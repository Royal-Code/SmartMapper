using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;
using RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

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
    }

    public bool TryGetParameterOptionsByName(string name, [NotNullWhen(true)] out ToConstructorParameterOptions? options)
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

}