using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

/// <summary>
/// <para>
///     Esta resolução tenta resolver um construtor, parâmetro por parâmetro.
/// </para>
/// </summary>
public class ConstrutorResolver
{
    private readonly ConstructorInfo constructorInfo;
    private readonly List<ConstructorParameterResolver> parameterResolvers;

    public ConstrutorResolver(ConstructorInfo constructorInfo)
    {
        this.constructorInfo = constructorInfo;

        parameterResolvers = constructorInfo.GetParameters()
            .Select(p => new ConstructorParameterResolver(p))
            .ToList();
    }

    public ConstrutorResolution Resolve(AdapterResolutionContext resolutionContext)
    {
        var properties = resolutionContext.GetPropertiesByStatus(
            ResolutionStatus.Undefined,
            ResolutionStatus.MappedToConstructor,
            ResolutionStatus.MappedToConstructorParameter);

        var ctorContext = new ConstructorResolutionContext(properties, resolutionContext);

        var parametersResults = parameterResolvers.Select(r => r.Resolve(ctorContext));

        // Plano B, mapear props para os parâmetros()

        var paramsContext = new ParametersResolutionContext(parameterResolvers);
        foreach (var property in properties)
        {
            if (property.Options.ResolutionStatus == ResolutionStatus.Undefined)
            {

            }
            else if (property.Options.ResolutionStatus == ResolutionStatus.MappedToConstructor)
            {

            }
            else if (property.Options.ResolutionStatus == ResolutionStatus.MappedToConstructorParameter)
            {

            }
        }

        throw new NotImplementedException();
    }
}

public class ParametersResolutionContext
{
    private readonly List<ConstructorParameterResolver> constructorParameterResolvers;

    public ParametersResolutionContext(List<ConstructorParameterResolver> constructorParameterResolvers)
    {
        this.constructorParameterResolvers = constructorParameterResolvers;
    }
}