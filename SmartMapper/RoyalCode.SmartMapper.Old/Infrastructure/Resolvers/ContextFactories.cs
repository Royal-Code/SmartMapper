using RoyalCode.SmartMapper.Infrastructure.Resolvers.Activations;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Constructors;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers;

/// <summary>
/// <para>
///     Static factories for create context instances.
/// </para>
/// </summary>
public static class ContextFactories
{
    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="AdapterContext"/> for the adapter resolution process.
    /// </para>
    /// </summary>
    /// <param name="request">The request for the adapter resolution.</param>
    /// <returns>A new instance of <see cref="AdapterContext"/>.</returns>
    public static AdapterContext CreateContext(this AdapterRequest request)
    {
        return new AdapterContext(request.Options, request.Configuration);
    }

    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="AdapterContext"/> for the activator resolution process.
    /// </para>
    /// </summary>
    /// <param name="request">The request for the activator resolution.</param>
    /// <returns>A new instance of <see cref="ActivationContext"/>.</returns>
    public static ActivationContext CreateContext(this ActivationRequest request)
    {
        return new ActivationContext(request.AdapterContext);
    }

    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="ConstructorContext"/> for the constructor resolution process.
    /// </para>
    /// </summary>
    /// <param name="request">The request for the constructor resolution.</param>
    /// <returns>A new instance of <see cref="ConstructorContext"/>.</returns>
    public static ConstructorContext CreateContext(this ConstructorRequest request) => new(request);
}
