using RoyalCode.SmartMapper.Extensions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Core;
using RoyalCode.SmartMapper.Infrastructure.Discovery;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.Constructors;

/// <summary>
/// <para>
///     This resolver attempts to resolve a constructor, parameter by parameter.
/// </para>
/// </summary>
public class ConstructorResolver
{
    /// <summary>
    /// <para>
    ///     Resolves a constructor of a target class, used by adapters.
    /// </para>
    /// <para>
    ///     If the resolution is successful, the target class can be instantiated.
    /// </para>
    /// <para>
    ///     If the source properties do not match the constructor parameters, the resolution will fail.
    /// </para>
    /// </summary>
    /// <param name="request">The request for the constructor resolution.</param>
    /// <returns>The resolution.</returns>
    public ConstructorResolution Resolve(ConstructorRequest request)
    {
        // part 1 - get parameters and source properties

        var targetParameters = context.Constructor.GetParameters()
            .Select(p => new TargetParameter(p))
            .ToList();

        var sourceProperties = context.ResolutionContext.GetPropertiesByStatus(
            ResolutionStatus.Undefined,
            ResolutionStatus.MappedToConstructor,
            ResolutionStatus.MappedToConstructorParameter).ToList();

        // part 2 - create context and contextual properties

        var availableProperties = new List<AvailableSourceProperty>();
        var groups = new List<InnerSourcePropertiesGroup>();
        foreach (var sourceProperty in sourceProperties)
        {
            if (sourceProperty.Options.ResolutionStatus == ResolutionStatus.MappedToConstructor)
            {
                var group = new InnerSourcePropertiesGroup(sourceProperty);
                var resolution = sourceProperty.Options.GetToConstructorOptionsResolution();
                resolution.SourceOptions.SourceType.GetReadableProperties()
                    .Select(info =>
                    {
                        var preConfigured = resolution.SourceOptions.TryGetPropertyOptions(info.Name, out var option);
                        var child = new SourceProperty(info, preConfigured, option ?? new PropertyOptions(info));
                        sourceProperty.Hierarchy.AddChild(child);
                        return child;
                    })
                    .Where(s => s.Options.ResolutionStatus != ResolutionStatus.Ignored)
                    .Select(s => new AvailableSourceProperty(s, group))
                    .ForEach(availableProperties.Add);
            }
            else
            {
                availableProperties.Add(new(sourceProperty));
            }
        }

        var ctorContext = new ConstructorResolutionContext(
            availableProperties,
            groups,
            targetParameters,
            context.ResolutionContext,
            context.Constructor);

        // Part 3 - Resolved pré-configured properties.

        var parameterResolver = context.ResolutionContext.Configuration.GetResolver<ConstructorParameterResolver>();
        foreach (var parameter in targetParameters)
        {
            var parameterContext = new ConstrutorParameterContext(ctorContext, parameter);
            if (parameterResolver.TryResolve(parameterContext, out var resolution))
            {
                ctorContext.Resolved(parameter, resolution);
            }
        }

        if (ctorContext.HasFailure || ctorContext.IsParametersResolved)
        {
            return ctorContext.GetResolution();
        }

        // Part 4 - Discovery mapping from source properties to target constructor parameters.

        var discovery = context.ResolutionContext.Configuration.GetDiscovery<ConstructorParameterDiscovery>();

        var discoveryContext = new ConstructorParameterDiscoveryContext(
            availableProperties.Where(p => !p.IsResolved).ToList(),
            targetParameters.Where(p => p.Unresolved).ToList(),
            context.ResolutionContext.Configuration);

        var result = discovery.Discover(discoveryContext);

        foreach (var match in result.Matches)
        {
            ctorContext.Resolved(match);
        }

        // produce a result e return it.

        return ctorContext.GetResolution();
    }
}