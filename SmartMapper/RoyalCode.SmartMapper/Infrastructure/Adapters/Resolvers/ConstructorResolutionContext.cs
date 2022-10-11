using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;
using RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Core;
using RoyalCode.SmartMapper.Infrastructure.Discovery;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

public class ConstructorResolutionContext
{
    private readonly AdapterResolutionContext resolutionContext;
    private readonly IEnumerable<AvailableSourceProperty> properties;
    private readonly IEnumerable<TargetParameter> parameters;
    private readonly ConstructorOptions constructorOptions;
    
    public ConstructorResolutionContext(
        IEnumerable<AvailableSourceProperty> properties,
        IEnumerable<TargetParameter> parameters,
        AdapterResolutionContext resolutionContext)
    {
        this.properties = properties;
        this.parameters = parameters;
        this.resolutionContext = resolutionContext;
        constructorOptions = resolutionContext.GetConstructorOptions();
        Configuration = resolutionContext.Configuration;
    }

    public ResolutionConfiguration Configuration { get; }

    public Type SourceType => resolutionContext.SourceType;

    public Type TargetType => resolutionContext.TargetType;

    public bool TryGetParameterOptionsByName(string name,
        [NotNullWhen(true)] out ToConstructorParameterOptions? options)
    {
        return constructorOptions.TryGetParameterOptions(name, out options);
    }
    
    public SourceProperty GetSourceProperty(PropertyInfo propertyInfo)
    {
        var property = properties.FirstOrDefault(p => p.SourceProperty.PropertyInfo == propertyInfo);
        if (property is null)
            throw new InvalidOperationException($"The property '{propertyInfo.Name}' is not a valid source property");

        return property.SourceProperty;
    }
}

public enum ConstructorResolutionPropertyKind
{
    
}

public class ConstructorResolutionProperty
{
    public SourceProperty SourceProperty { get; set; }
    
    
}