using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.Adapters;

// TODO: Transformar em record

/// <summary>
/// <para>
///     Request to resolve the adapter type mapping between the source type and the destination type.
/// </para>
/// </summary>
public class AdapterRequest
{
    /// <summary>
    /// <para>
    ///     The source and destination types.
    /// </para>
    /// </summary>
    public MapKey Key { get; }

    /// <summary>
    /// <para>
    ///     The adapter options retrieved from the Configuration.
    /// </para>
    /// </summary>
    public AdapterOptions Options => Configuration.Mappings.AdaptersOptions.GetOptions(Key);

    /// <summary>
    /// <para>
    ///     The configuration used to resolve the adapter.
    /// </para>
    /// </summary>
    public ResolutionConfiguration Configuration { get; }

    /// <summary>
    /// Creates a new instance of <see cref="AdapterRequest"/>.
    /// </summary>
    /// <param name="key">The source and destination types.</param>
    /// <param name="configuration">The configuration used to resolve the adapter.</param>
    public AdapterRequest(
        MapKey key,
        ResolutionConfiguration configuration)
    {
        Key = key;
        Configuration = configuration;
    }
}