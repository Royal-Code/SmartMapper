using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Core;
using RoyalCode.SmartMapper.Infrastructure.Selectors.Options;

namespace RoyalCode.SmartMapper.Infrastructure.Configurations;

/// <summary>
/// <para>
///     Contains all options and configurations for adapters.
/// </para>
/// </summary>
public interface IAdaptersOptions
{
    AdapterOptions GetOptions(MapKey key);
}

public interface ISelectorsOptions
{
    SelectorOptions GetOptions(MapKey key);
}