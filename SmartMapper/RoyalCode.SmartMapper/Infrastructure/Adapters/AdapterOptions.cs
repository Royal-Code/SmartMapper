using RoyalCode.SmartMapper.Infrastructure.Options;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

/// <summary>
/// <para>
///     Contains all the options for a single mapping between a source and target type.
/// </para>
/// </summary>
public class AdapterOptions : OptionsBase
{
    private ICollection<AdapterSourceToMethodOptions>? sourceToMethodOptions;

    /// <summary>
    /// <para>
    ///     Gets the options for mapping a source type to a method.
    /// </para>
    /// </summary>
    /// <returns>
    ///     All options for mapping a source type to a method or an empty collection if no options have been set.
    /// </returns>
    public IEnumerable<AdapterSourceToMethodOptions> GetSourceToMethodOptions()
    {
        return sourceToMethodOptions ?? Enumerable.Empty<AdapterSourceToMethodOptions>();
    }

    /// <summary>
    /// <para>
    ///     Adds an option for mapping a source type to a method.
    /// </para>
    /// </summary>
    /// <param name="options">The options for mapping a source type to a method.</param>
    public void AddToMethod(AdapterSourceToMethodOptions options)
    {
        sourceToMethodOptions ??= new List<AdapterSourceToMethodOptions>();
        sourceToMethodOptions.Add(options);
    }
}