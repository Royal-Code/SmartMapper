
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Configurations.Adapters;

/// <summary>
/// <para>
///     A builder to configure the assignment strategy of the mapping.
/// </para>
/// </summary>
/// <typeparam name="TSourceProperty">The type of the source property.</typeparam>
/// <typeparam name="TTargetProperty">The type of the destination property.</typeparam>
/// <typeparam name="TOptionsBuilder">The options builder type.</typeparam>
public interface IAdapterPropertyStrategyBuilder<TSourceProperty, TTargetProperty, TOptionsBuilder>
{
    /// <summary>
    /// <para>
    ///     Configure the assignment strategy to cast the source value type to the destination value type.
    /// </para>
    /// </summary>
    /// <returns>
    ///     The same instance for chained calls.
    /// </returns>
    TOptionsBuilder CastValue();

    /// <summary>
    /// <para>
    ///     Configure the assignment strategy to convert the source value type to the destination value type.
    /// </para>
    /// </summary>
    /// <param name="converter">
    ///     The converter to use to convert the source value type to the destination value type.
    /// </param>
    /// <returns>
    ///     The same instance for chained calls.
    /// </returns>
    TOptionsBuilder UseConverter(Expression<Func<TSourceProperty, TTargetProperty>> converter);

    /// <summary>
    /// <para>
    ///     Configure the assignment strategy to adapt the source value type to the destination value type.
    /// </para>
    /// </summary>
    /// <returns>
    ///     The same instance for chained calls.
    /// </returns>
    TOptionsBuilder Adapt();

    /// <summary>
    /// <para>
    ///     Configure the assignment strategy to select the source value type to the destination value type.
    /// </para>
    /// </summary>
    /// <returns>
    ///     The same instance for chained calls.
    /// </returns>
    TOptionsBuilder Select();

    /// <summary>
    /// <para>
    ///     Configure the assignment strategy to process the source value with a service to retreive the destination value.
    /// </para>
    /// </summary>
    /// <param name="valueProcessor">
    ///     The service to use to process the source value.
    /// </param>
    /// <typeparam name="TService">
    ///     The service type.
    /// </typeparam>
    /// <returns>
    ///     The same instance for chained calls.
    /// </returns>
    TOptionsBuilder WithService<TService>(Expression<Func<TService, TSourceProperty, TTargetProperty>> valueProcessor);
}
