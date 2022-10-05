using System.Reflection;
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
    private readonly ConstructorInfo constructorInfo;
    private readonly List<TargetParameter> targetParameters;

    public ConstrutorResolver(ConstructorInfo constructorInfo)
    {
        this.constructorInfo = constructorInfo;

        targetParameters = constructorInfo.GetParameters()
            .Select(p => new TargetParameter(p))
            .ToList();
    }

    public ConstrutorResolution Resolve(AdapterResolutionContext resolutionContext)
    {
        var properties = resolutionContext.GetPropertiesByStatus(
            ResolutionStatus.Undefined,
            ResolutionStatus.MappedToConstructor,
            ResolutionStatus.MappedToConstructorParameter)
            .ToList();

        var parameterResolver = resolutionContext.Configuration.GetResolver<ConstructorParameterResolver>();
        var ctorContext = new ConstructorResolutionContext(properties, resolutionContext);

        foreach (var parameter in targetParameters)
        {
            var parameterContext = new ConstrutorParameterContext(ctorContext, parameter);
            if (parameterResolver.TryResolve(parameterContext, out var resolution))
            {
                parameter.ResolvedBy(resolution);
                properties.Remove(resolution.SourceProperty);
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

        var discovery = resolutionContext.Configuration.GetDiscovery<ConstructorParameterDiscovery>();
        
        var discoveryContext = new ConstructorParameterDiscoveryContext(properties, 
            targetParameters.Where(p => p.Unresolved).ToList(),
            resolutionContext.Configuration);
        
        var discoverd = discovery.Discover(discoveryContext);
        
        
        throw new NotImplementedException();
    }
}

public record ConstrutorParameterContext(ConstructorResolutionContext ConstructorContext, TargetParameter Parameter);