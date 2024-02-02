
using RoyalCode.SmartMapper.Core.Configurations;

namespace RoyalCode.SmartMapper;

/// <summary>
/// Default implementation of <see cref="IAdapter"/>.
/// </summary>
public sealed class DefaultAdapter : IAdapter
{
    private readonly MapperConfigurations configurations;

    /// <summary>
    /// Create a new instance of <see cref="DefaultAdapter"/> with the given configurations.
    /// </summary>
    /// <param name="configurations">
    ///     The mapper configurations, including the adapters, selectors, and other components for creating the mappings.
    /// </param>
    public DefaultAdapter(MapperConfigurations configurations)
    {
        this.configurations = configurations;
    }

    /// <inheritdoc />
    public TTarget Map<TSource, TTarget>(TSource from)
    {
        return configurations.GetAdapter<TSource, TTarget>()(from);
    }

    /// <inheritdoc />
    public TTarget Map<TTarget>(object source, Type? type = null)
    {
        throw new NotImplementedException();
    }
}
