using RoyalCode.SmartMapper.Infrastructure.Resolvers.Constructors;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.Activations;

/// <summary>
/// <para>
///     Resolver para descobrir e criar a resolução de construtor.
/// </para>
/// </summary>
public class ActivationResolver
{
    /// <summary>
    /// Resolve the activator for the given <paramref name="request"/>.
    /// </summary>
    /// <param name="request">The request for the activator resolution.</param>
    /// <returns>The resolution for the activator.</returns>
    public ActivationResolution Resolve(ActivationRequest request)
    {
        var context = request.CreateContext();

        var ctorResolver = request.Configuration.GetResolver<ConstructorResolver>();
        foreach (var ctor in context.Constructors)
        {
            var ctorRequest = ctor.CreateConstructorRequest(context);
            var resolution = ctorResolver.Resolve(ctorRequest);
            ctor.Resolution = resolution;
        }

        return context.GetResolution();
    }
}