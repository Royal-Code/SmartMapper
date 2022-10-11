using RoyalCode.SmartMapper.Extensions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;
using RoyalCode.SmartMapper.Infrastructure.Core;
using RoyalCode.SmartMapper.Infrastructure.Discovery;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

/// <summary>
/// <para>
///     Esta resolução tenta resolver um construtor, parâmetro por parâmetro.
/// </para>
/// </summary>
public class ConstrutorResolver
{
    public ConstrutorResolution Resolve(ConstructorContext context)
    {
        var targetParameters = context.Constructor.GetParameters()
            .Select(p => new TargetParameter(p))
            .ToList();
        
        var sourceProperties = context.ResolutionContext.GetPropertiesByStatus(
            ResolutionStatus.Undefined,
            ResolutionStatus.MappedToConstructor,
            ResolutionStatus.MappedToConstructorParameter).ToList();

        var availableProperties = new List<AvailableSourceProperty>();
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
                        return new SourceProperty(info, preConfigured, option ?? new PropertyOptions(info));
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
        

        var parameterResolver = context.ResolutionContext.Configuration.GetResolver<ConstructorParameterResolver>();
        var ctorContext = new ConstructorResolutionContext(availableProperties, targetParameters, context.ResolutionContext);

        foreach (var parameter in targetParameters)
        {
            var parameterContext = new ConstrutorParameterContext(ctorContext, parameter);
            if (parameterResolver.TryResolve(parameterContext, out var resolution))
            {
                parameter.ResolvedBy(resolution);
                sourceProperties.Remove(resolution.SourceProperty);
            }
        }

        if (targetParameters.Any(p => p.HasFailure))
        {
            // gerar erro
        }

        if (targetParameters.TrueForAll(p => !p.Unresolved))
        {
            // gerar sucesso.
        }

        var discovery = context.ResolutionContext.Configuration.GetDiscovery<ConstructorParameterDiscovery>();
        
        var discoveryContext = new ConstructorParameterDiscoveryContext(sourceProperties, 
            targetParameters.Where(p => p.Unresolved).ToList(),
            context.ResolutionContext.Configuration);
        
        var discoverd = discovery.Discover(discoveryContext);
        
        throw new NotImplementedException();
    }
}

public class AvailableSourceProperty
{
    public AvailableSourceProperty(SourceProperty sourceProperty, InnerSourcePropertiesGroup? group = null)
    {
        SourceProperty = sourceProperty;
        Group = group;
        group?.Add(this);
    }

    public SourceProperty SourceProperty { get; }

    public InnerSourcePropertiesGroup? Group { get; }
    
    public TargetParameter? TargetParameter { get; private set; }

    public bool IsResolved { get; private set; }
    
    public void ResolvedBy(TargetParameter targetParameter)
    {
        TargetParameter = targetParameter;
        IsResolved = true;
    }
}

public class InnerSourcePropertiesGroup
{
    private readonly List<AvailableSourceProperty> properties = new();

    public InnerSourcePropertiesGroup(SourceProperty sourceProperty)
    {
        SourceProperty = sourceProperty;
    }

    public SourceProperty SourceProperty { get; }
    
    public bool IsResolved => properties.All(p => p.IsResolved);
    
    public void Add(AvailableSourceProperty property) => properties.Add(property);
}
