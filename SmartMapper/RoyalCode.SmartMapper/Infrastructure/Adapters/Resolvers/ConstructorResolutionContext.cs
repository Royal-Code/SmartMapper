using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Discovery;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

public class ConstructorResolutionContext
{
    private readonly AdapterResolutionContext resolutionContext;
    private readonly IEnumerable<AvailableSourceProperty> properties;
    private readonly IEnumerable<InnerSourcePropertiesGroup> groups;
    private readonly IEnumerable<TargetParameter> parameters;
    private readonly ConstructorOptions constructorOptions;
    
    public ConstructorResolutionContext(
        IEnumerable<AvailableSourceProperty> properties,
        IEnumerable<InnerSourcePropertiesGroup> groups,
        IEnumerable<TargetParameter> parameters,
        AdapterResolutionContext resolutionContext)
    {
        this.properties = properties;
        this.groups = groups;
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
    
    public bool TryGetAvailableSourceProperty(PropertyInfo propertyInfo, 
        [NotNullWhen(true)] out AvailableSourceProperty? property,
        [NotNullWhen(false)] out string? failureReason)
    {
        failureReason = null;
        property = properties.FirstOrDefault(p => p.SourceProperty.PropertyInfo == propertyInfo);
        
        if (property is null)
            failureReason = $"The property '{propertyInfo.Name}' is not a valid source property";
        
        if (property.IsResolved)
        {
            failureReason = $"The property '{propertyInfo.Name}' was resolved before";
            property = null;
        }

        return property is not null;
    }

    public void Resolved(TargetParameter parameter, ParameterResolution resolution)
    {
        parameter.ResolvedBy(resolution);
        resolution.AvailableSourceProperty.ResolvedBy(parameter);
    }

    public void Resolved(ConstructorParameterMatch match)
    {
        var resolution = new ParameterResolution()
        {
            Resolved = true,
            Parameter = match.Parameter,
            AssignmentResolution = match.AssignmentResolution,
            AvailableSourceProperty = match.Property
        };

        Resolved(match.Parameter, resolution);
    }

    public bool HasFailure => parameters.Any(p => p.HasFailure);

    public bool IsParametersResolved => parameters.All(p => !p.Unresolved);

    public bool IsSuccessfullyResolved => IsParametersResolved && groups.All(g => g.IsResolved) && !HasFailure;
    
    public ConstrutorResolution GetResolution()
    {
        // TODO: processar a resolução. O objeto de retorno ainda não tem informações.
        
        
        throw new NotImplementedException();
    }
}

public enum ConstructorResolutionPropertyKind
{
    
}

public class ConstructorResolutionProperty
{
    public SourceProperty SourceProperty { get; set; }
    
    
}