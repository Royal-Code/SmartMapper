
namespace RoyalCode.SmartMapper.Adapters.Configurations;

/// <summary>
/// <para>
///     A builder to continue the mapping of a source property to a target property.
/// </para>
/// </summary>
/// <typeparam name="TSourceProperty">The source property type.</typeparam>
/// <typeparam name="TTargetProperty">The destination property type.</typeparam>
/// <typeparam name="TNextProperty">The next destination property type.</typeparam>
public interface IPropertyThenToOptionsBuilder<TSourceProperty, TTargetProperty, TNextProperty>
    : IPropertyStrategyBuilder<TSourceProperty, TNextProperty, IPropertyThenToOptionsBuilder<TSourceProperty, TTargetProperty, TNextProperty>>
{
    /// <summary>
    /// <para>
    ///     Continues the mapping of the source property to an internal property of the target property.
    /// </para>
    /// </summary>
    /// <returns>
    ///     The builder to configure the property to property mapping.
    /// </returns>
    IPropertyThenOptionsBuilder<TSourceProperty, TNextProperty> Then();
}

