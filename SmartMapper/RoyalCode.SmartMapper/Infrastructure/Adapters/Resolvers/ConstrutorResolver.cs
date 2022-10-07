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
        
        var properties = context.ResolutionContext.GetPropertiesByStatus(
            ResolutionStatus.Undefined,
            ResolutionStatus.MappedToConstructor,
            ResolutionStatus.MappedToConstructorParameter).ToList();

        var parameterResolver = context.ResolutionContext.Configuration.GetResolver<ConstructorParameterResolver>();
        var ctorContext = new ConstructorResolutionContext(properties, targetParameters, context.ResolutionContext);

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

        var discovery = context.ResolutionContext.Configuration.GetDiscovery<ConstructorParameterDiscovery>();
        
        var discoveryContext = new ConstructorParameterDiscoveryContext(properties, 
            targetParameters.Where(p => p.Unresolved).ToList(),
            resolutionContext.Configuration);
        
        var discoverd = discovery.Discover(discoveryContext);
        
        
        throw new NotImplementedException();
    }
}

public record ConstrutorParameterContext(ConstructorResolutionContext ConstructorContext, TargetParameter Parameter);