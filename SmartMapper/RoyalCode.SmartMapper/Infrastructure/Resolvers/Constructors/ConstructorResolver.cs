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
        // part 1 - create a context for the resolution
        var context = request.CreateContext();
        
        // Part 2 - Resolved pré-configured properties.

        var parameterResolver = request.Configuration.GetResolver<ConstructorParameterResolver>();
        foreach (var parameter in context.TargetParameters)
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