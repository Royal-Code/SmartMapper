using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Discovery;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

public class ConstructorResolutionContext
{
    private readonly AdapterContext resolutionContext;
    private readonly IEnumerable<AvailableSourceProperty> properties;
    private readonly IEnumerable<InnerSourcePropertiesGroup> groups;
    private readonly IEnumerable<TargetParameter> parameters;
    private readonly ConstructorOptions constructorOptions;

    public ConstructorResolutionContext(
        IEnumerable<AvailableSourceProperty> properties,
        IEnumerable<InnerSourcePropertiesGroup> groups,
        IEnumerable<TargetParameter> parameters,
        AdapterContext resolutionContext,
        ConstructorInfo constructorInfo)
    {
        this.properties = properties;
        this.groups = groups;
        this.parameters = parameters;
        this.resolutionContext = resolutionContext;
        constructorOptions = resolutionContext.GetConstructorOptions();
        Configuration = resolutionContext.Configuration;
        ConstructorInfo = constructorInfo;
    }

    public ResolutionConfiguration Configuration { get; }

    public Type SourceType => resolutionContext.SourceType;

    public Type TargetType => resolutionContext.TargetType;

    public ConstructorInfo ConstructorInfo { get; }

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
        var resolution = new ParameterResolution(match.Property)
        {
            Resolved = true,
            Parameter = match.Parameter,
            AssignmentResolution = match.AssignmentResolution,
        };

        Resolved(match.Parameter, resolution);
    }

    public bool HasFailure => parameters.Any(p => p.HasFailure);

    public bool IsParametersResolved => parameters.All(p => !p.Unresolved);

    public bool IsSuccessfullyResolved => IsParametersResolved && groups.All(g => g.IsResolved) && !HasFailure;
    
    public ConstrutorResolution GetResolution()
    {
        if (HasFailure)
        {
            return new ConstrutorResolution(
                ConstructorInfo,
                parameters.Where(p => p.HasFailure).SelectMany(p => p.Resolution!.FailureMessages!));
        }

        if (!IsSuccessfullyResolved)
        {
            // pegar os parâmetros não resolvidos e gerar uma mensagem.
            var unresolvedParametersMessages = parameters.Where(p => p.Unresolved)
                .Select(p => "The parameter '" + p.ParameterInfo.Name + "' is not resolved");

            // pegar os grupos não resolvidos, e gerar uma mensagem para cada, incluindo os nomes das
            // propriedades internas não resolvidas.
            var unresolvedGroupsMessages = groups.Where(g => !g.IsResolved)
                .Select(g => g.GetFailureMessage());

            return new ConstrutorResolution(
                ConstructorInfo,
                unresolvedParametersMessages.Concat(unresolvedGroupsMessages));
        }

        // processar sucesso.
        var resolutions = parameters.Select(p => p.Resolution!).ToList();

        return new ConstrutorResolution(resolutions, ConstructorInfo);
    }
}
