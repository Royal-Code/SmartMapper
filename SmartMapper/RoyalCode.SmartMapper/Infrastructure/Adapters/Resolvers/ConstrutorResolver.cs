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

        parameterResolvers.ForEach(r => r.Resolve(ctorContext));
        
        throw new NotImplementedException();
    }
}