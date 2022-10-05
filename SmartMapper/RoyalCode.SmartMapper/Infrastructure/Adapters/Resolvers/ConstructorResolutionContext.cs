using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;
using RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Core;
using RoyalCode.SmartMapper.Infrastructure.Discovery;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

// TODO: Remover este contexto, não é mais usado, passar operações para ConstrutorParameterContext
public class ConstructorResolutionContext
{
    private readonly AdapterResolutionContext adapterResolutionContext;
    private readonly IEnumerable<SourceProperty> properties;
    private readonly ConstructorOptions constructorOptions;
    public ConstructorResolutionContext(
        IEnumerable<SourceProperty> properties,
        AdapterResolutionContext adapterResolutionContext)
    {
        this.properties = properties;
        this.adapterResolutionContext = adapterResolutionContext;
        constructorOptions = adapterResolutionContext.GetConstructorOptions();
        Configuration = adapterResolutionContext.Configuration;
    }

    public ResolutionConfiguration Configuration { get; }

    public Type SourceType => adapterResolutionContext.SourceType;

    public Type TargetType => adapterResolutionContext.TargetType;

    public bool TryGetParameterOptionsByName(string name,
        [NotNullWhen(true)] out ToConstructorParameterOptions? options)
    {
        return constructorOptions.TryGetParameterOptions(name, out options);
    }
    
    public SourceProperty GetSourceProperty(PropertyInfo propertyInfo)
    {
        var sourceProperty = properties.FirstOrDefault(p => p.PropertyInfo == propertyInfo);
        if (sourceProperty is null)
            throw new InvalidOperationException($"The property '{propertyInfo.Name}' is not a valid source property");

        return sourceProperty;
    }

    public AssignmentStrategyResolver GetAssignmentStrategyResolver()
        => adapterResolutionContext.GetAssignmentStrategyResolver();

    public ConstructorParameterDiscoveryContext CreateDiscoveryContext(IEnumerable<TargetParameter> parameters)
        => new ConstructorParameterDiscoveryContext(properties, parameters, Configuration);
}