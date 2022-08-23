using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Configurations.Adapters;

/// <summary>
/// <para>
///     Contains all options and configurations for adapters.
/// </para>
/// </summary>
public interface IAdaptersOptions
{
    // TODO: Rever namespaces, está consumindo e retornado do Infrastructure.
    AdapterOptions GetOptions(MapKey key);
}