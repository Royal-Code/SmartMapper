using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

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
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    internal ActivationResolution GetResolution()
    {
        if (!HasEligibleConstructors)
        {
            return new ActivationResolution()
            {
                Resolved = false,
                FailureMessages = new[] { $"None elegible constructor for adapt {AdapterContext.Options.SourceType.Name} type to {AdapterContext.Options.TargetType.Name} type." }
            };
        }

        
    }
}