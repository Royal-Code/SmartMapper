using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

/// <summary>
/// <para>
///     Resolver para descobrir e criar a resolução de construtor.
/// </para>
/// </summary>
public class ActivationResolver
{
    public ActivationResolution Resolve(AdapterResolutionContext resolutionContext)
    {
        var ctorElegible = GetElegibleConstructors(resolutionContext.GetConstructorOptions());

        // deve ser validado se existe algum ctor elegivel.
        
        var construtorResolutions = ctorElegible.Select(c => new ConstrutorResolver(c))
            .Select(r => r.Resolve(resolutionContext))
            .ToList();
        
        // se não tiver resoluções com sucesso, registra o erro e encerra.
        
        // filtra as resoluções com sucesso e ordena ao estilo best constructor selector.
        
        // agora deve ser validada as resoluções, se as opções pré-configuradas está atendendo parametros do ctor resolvido?
        
        // elimina os ctor inválidos.
        
        // caso não sobre nenhum, registra o erro de configuração.
        
        // por fim, pega o melhor construtor e gera um sucesso.
        // o resolver do adaptador deverá pegar a resolução do ctor e aplicar os valores corretos nas opções.
        
        throw new NotImplementedException();
    }
    
    internal ConstructorInfo[] GetElegibleConstructors(ConstructorOptions options)
    {
        if (options.ParameterTypes is not null)
        {
            var constructor = options.TargetType.GetTypeInfo().GetConstructor(
                BindingFlags.Instance | BindingFlags.Public,
                null,
                options.ParameterTypes!,
                null);
            
            return constructor is null
                ? Array.Empty<ConstructorInfo>()
                : new[] { constructor };
        }
        
        var constructors = options.TargetType.GetTypeInfo().DeclaredConstructors;
        if (options.NumberOfParameters.HasValue)
            constructors = constructors.Where(c => c.GetParameters().Length == options.NumberOfParameters.Value);

        return constructors.ToArray();
    }
}