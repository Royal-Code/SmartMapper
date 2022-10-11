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
    private readonly IEnumerable<SourceProperty> properties;
    private readonly IEnumerable<TargetParameter> parameters;
    private readonly ConstructorOptions constructorOptions;
    
    public ConstructorResolutionContext(
        IEnumerable<SourceProperty> properties,
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
        var sourceProperty = properties.FirstOrDefault(p => p.PropertyInfo == propertyInfo);
        if (sourceProperty is null)
            throw new InvalidOperationException($"The property '{propertyInfo.Name}' is not a valid source property");

        return sourceProperty;
    }
}

public enum ConstructorResolutionPropertyKind
{
    
}

public class ConstructorResolutionProperty
{
    public SourceProperty SourceProperty { get; set; }
    
    
}