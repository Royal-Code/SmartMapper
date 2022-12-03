using RoyalCode.SmartMapper.Infrastructure.Resolvers.Adapters;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.Activations;

/// <summary>
/// Context for the activator resolution process.
/// </summary>
public class ActivationContext
{
    private readonly EligibleConstructor[] eligibleConstructors;

    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="ActivationContext"/> for the activator resolution process.
    /// </para>
    /// </summary>
    /// <param name="adapterContext">The context of the adapter resolution process.</param>
    public ActivationContext(AdapterContext adapterContext)
    {
        eligibleConstructors = adapterContext.CreateEligibleConstructors();
        AdapterContext = adapterContext;
    }

    /// <summary>
    /// The context of the adapter resolution process.
    /// </summary>
    public AdapterContext AdapterContext { get; }

    /// <summary>
    /// Get all eligible constructors for the target type.
    /// </summary>
    public IEnumerable<EligibleConstructor> Constructors => eligibleConstructors;

    /// <summary>
    /// Check if has any eligible constructor.
    /// </summary>
    public bool HasEligibleConstructors => eligibleConstructors.Length > 0;

    /// <summary>
    /// Get the best reolved eligible constructor for the target type.
    /// </summary>
    /// <returns>The best resolved eligible constructor.</returns>
    public EligibleConstructor? GetBestConstructor()
    {
        if (eligibleConstructors.Length == 0)
            return null;

        // filtra as resoluções com sucesso e ordena ao estilo best constructor selector.
        return eligibleConstructors
            .Where(c => c.Resolution.Resolved)
            .OrderByDescending(c => c.Resolution.Parameters.Length)
            .FirstOrDefault();
    }

    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="ActivationResolution" /> with the result of the computation.
    /// </para>
    /// </summary>
    /// <returns>A new instance of <see cref="ActivationResolution" />.</returns>
    internal ActivationResolution GetResolution()
    {
        // must be validated if any eligible ctor exists.
        if (!HasEligibleConstructors)
        {
            return new ActivationResolution()
            {
                Resolved = false,
                FailureMessages = new[] { $"None elegible constructor for adapt {AdapterContext.Options.SourceType.Name} type to {AdapterContext.Options.TargetType.Name} type." }
            };
        }

        // check if has any resolved constructor.
        var bestConstructor = GetBestConstructor();
        if (bestConstructor is null)
        {
            return new ActivationResolution()
            {
                Resolved = false,
                FailureMessages = new[]
                {
                    $"None elegible constructor for adapt {AdapterContext.Options.SourceType.Name} type to {AdapterContext.Options.TargetType.Name} type."
                }
            };
        }

        return new ActivationResolution(bestConstructor.Resolution);
    }
}