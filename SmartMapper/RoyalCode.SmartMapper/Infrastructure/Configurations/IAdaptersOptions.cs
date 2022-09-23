using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Configurations;

/// <summary>
/// <para>
///     Contains all options and configurations for adapters.
/// </para>
/// </summary>
public interface IAdaptersOptions
{
    // TODO: Rever se continuará existindo a interface, visto que é referênciado pelo Mappings e agora é infra.
    AdapterOptions GetOptions(MapKey key);
}