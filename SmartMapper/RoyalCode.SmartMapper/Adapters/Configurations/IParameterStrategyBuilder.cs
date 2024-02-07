
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Adapters.Configurations;

/// <summary>
/// <para>
///     Options Builder to configure the assignment strategy from a property of the source type 
///     that is mapped to a parameter of a method on the target type.
/// </para>
/// </summary>
/// <typeparam name="TProperty">The source property type</typeparam>
public interface IParameterStrategyBuilder<TProperty>
{
    /// <summary>
    /// <para>
    ///     Configure the assignment strategy to cast the source value type to the destination value type.
    /// </para>
    /// </summary>
    /// <returns>
    ///     The same instance for chained calls.
    /// </returns>
    IParameterStrategyBuilder<TProperty> CastValue();

    /// <summary>
    /// <para>
    ///     Configure the assignment strategy to convert the source value type to the destination value type.
    /// </para>
    /// </summary>
    /// <typeparam name="TParameter">
    ///     The method parameter type.
    /// </typeparam>
    /// <param name="converter">
    ///     The converter to use to convert the source value type to the destination value type.
    /// </param>
    /// <returns>
    ///     The same instance for chained calls.
    /// </returns>
    IParameterStrategyBuilder<TProperty> UseConverter<TParameter>(
        Expression<Func<TProperty, TParameter>> converter);

    /// <summary>
    /// <para>
    ///     Configure the assignment strategy to adapt the source value type to the destination value type.
    /// </para>
    /// </summary>
    /// <returns>
    ///     The same instance for chained calls.
    /// </returns>
    IParameterStrategyBuilder<TProperty> Adapt();

    /// <summary>
    /// <para>
    ///     Configure the assignment strategy to select the source value type to the destination value type.
    /// </para>
    /// </summary>
    /// <returns>
    ///     The same instance for chained calls.
    /// </returns>
    IParameterStrategyBuilder<TProperty> Select();

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
    /// <typeparam name="TParameter">
    ///     The method parameter type.
    /// </typeparam>
    /// <returns>
    ///     The same instance for chained calls.
    /// </returns>
    IParameterStrategyBuilder<TProperty> WithService<TService, TParameter>(
        Expression<Func<TService, TProperty, TParameter>> valueProcessor);
}

