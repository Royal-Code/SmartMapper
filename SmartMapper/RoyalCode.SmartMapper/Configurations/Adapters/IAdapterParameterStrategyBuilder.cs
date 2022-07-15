using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Configurations.Adapters;

/// <summary>
/// <para>
///     Options Builder to configure the assignment strategy from a property of the source type 
///     that is mapped to a parameter of a method on the target type.
/// </para>
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
/// <typeparam name="TTarget">The destination type.</typeparam>
/// <typeparam name="TProperty">The source property type</typeparam>
public interface IAdapterParamterStrategyBuilder<TSource, TTarget, TProperty>
{
    /// <summary>
    /// <para>
    ///     Configure the assignment strategy to cast the source value type to the destination value type.
    /// </para>
    /// </summary>
    /// <returns>
    ///     The same instance for chained calls.
    /// </returns>
    IAdapterParamterStrategyBuilder<TSource, TTarget, TProperty> CastValue();

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
    IAdapterParamterStrategyBuilder<TSource, TTarget, TProperty> UseConverter<TParameter>(
        Expression<Func<TProperty, TParameter>> converter);

    /// <summary>
    /// <para>
    ///     Configure the assignment strategy to adapt the source value type to the destination value type.
    /// </para>
    /// </summary>
    /// <returns>
    ///     The same instance for chained calls.
    /// </returns>
    IAdapterParamterStrategyBuilder<TSource, TTarget, TProperty> Adapt();

    /// <summary>
    /// <para>
    ///     Configure the assignment strategy to select the source value type to the destination value type.
    /// </para>
    /// </summary>
    /// <returns>
    ///     The same instance for chained calls.
    /// </returns>
    IAdapterParamterStrategyBuilder<TSource, TTarget, TProperty> Select();

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
    IAdapterParamterStrategyBuilder<TSource, TTarget, TProperty> WithService<TService, TParameter>(
        Expression<Func<TService, TProperty, TParameter>> valueProcessor);
}
