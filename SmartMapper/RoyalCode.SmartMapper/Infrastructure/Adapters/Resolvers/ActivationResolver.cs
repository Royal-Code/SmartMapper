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
                .ToList();



        // se não tiver resoluções com sucesso, registra o erro e encerra.
        if (construtorResolutions.All(c => !c.Resolved))
        {
            return new ActivationResolution()
            {
                Resolved = false,
                FailureMessages = Enumerable.Concat(
                    new string[] { $"None elegible constructor for adapt {resolutionContext.SourceType.Name} type to {resolutionContext.TargetType.Name} type." },
                    construtorResolutions.SelectMany(c => c.FailureMessages!))               
            };
        }

        // filtra as resoluções com sucesso e ordena ao estilo best constructor selector.
        var bestConstructorResolution = construtorResolutions
            .Where(c => c.Resolved)
            .OrderByDescending(c => c.Parameters.Length)
            .First();
        
        return new ActivationResolution(bestConstructorResolution.Constructor, bestConstructorResolution.ParameterResolution);
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