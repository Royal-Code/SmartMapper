using RoyalCode.SmartMapper.Infrastructure.Resolvers.Activations;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Constructors;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers;

/// <summary>
/// <para>
///     Static factories for create requests instances.
/// </para>
/// </summary>
public static class RequestsFactories
{
    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="ActivationRequest"/> for the adapter resolution process.
    /// </para>
    /// </summary>
    /// <param name="context">The context of the adapter resolution process.</param>
    /// <returns>A new instance of <see cref="ActivationRequest"/>.</returns>
    public static ActivationRequest CreateActivationRequest(this AdapterContext context)
    {
        return new ActivationRequest(context);
    }

    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="ConstructorRequest"/> for the constructor resolution process.
    /// </para>
    /// </summary>
    /// <param name="constructor">The constructor to be resolved.</param>
    /// <param name="context">The context of the adapter resolution process.</param>
    /// <returns>A new instance of <see cref="ConstructorRequest"/>.</returns>
    public static ConstructorRequest CreateConstructorRequest(this EligibleConstructor constructor, ActivationContext context)
    {
        return new ConstructorRequest(context, constructor);
    }
}
