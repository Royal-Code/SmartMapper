using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using RoyalCode.SmartMapper.Extensions;
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

        // must be validated if any eligible ctor exists.
        if (ctorElegible.Length is 0)
        {
            return new ActivationResolution()
            {
                Resolved = false,
                FailureMessages = new[] { $"None elegible constructor for adapt {resolutionContext.SourceType.Name} type to {resolutionContext.TargetType.Name} type." }
            };
        }

        var ctorResolver = resolutionContext.Configuration.GetResolver<ConstructorResolver>();

        var construtorResolutions = ctorElegible.Select(c => new ConstructorContext(resolutionContext, c))
                .Select(ctorResolver.Resolve)
                .ToSpanMarshal();

        

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