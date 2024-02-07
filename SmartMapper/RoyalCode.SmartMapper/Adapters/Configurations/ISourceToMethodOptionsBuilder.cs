
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Adapters.Configurations;

/// <summary>
/// <para>
///     A builder to configurate the mapping for adapter of a source type to a method of a destination type.
/// </para>
/// <para>
///     By default, all properties will be mapped to the method, and will be resolved by the property/parameter name.    
/// </para>
/// <para>
///     It is possible to choose only some properties to be mapped to a method.
///     This option requires that the method is identified.
/// </para>
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
/// <typeparam name="TTarget">The destination type.</typeparam>
public interface ISourceToMethodOptionsBuilder<TSource, TTarget>
{
    /// <summary>
    /// <para>
    ///     In this option, some properties of the source type are mapped to parameters.
    /// </para>
    /// <para>
    ///     The order of the properties is the order of the parameters.
    /// </para>
    /// </summary>
    /// <param name="configureParameters">
    ///     A function to configure the parameters of the destination method.
    /// </param>
    void Parameters(Action<ISourceToMethodParametersOptionsBuilder<TSource>> configureParameters);

    /// <summary>
    /// <para>
    ///     This is the default option if the other is not performed.
    /// </para>
    /// <para>
    ///     All properties of the source type are mapped to a destination type method.
    /// </para>
    /// <para>
    ///     In it you can configure each property of the source type for a parameter.
    /// </para>
    /// </summary>
    /// <param name="configureProperties">
    ///     A function to configure the properties of the source type.
    /// </param>
    void AllProperties(Action<ISourceToMethodPropertiesOptionsBuilder<TSource>> configureProperties);

    /// <summary>
    /// <para>
    ///     Configure the name of the target method.
    /// </para>
    /// </summary>
    /// <param name="name">The name of method.</param>
    /// <returns>The same instance for chained calls.</returns>
    ISourceToMethodOptionsBuilder<TSource, TTarget> UseMethod(string name);

    /// <summary>
    /// <para>
    ///     Configure the target method using a expression selector.
    /// </para>
    /// </summary>
    /// <param name="methodSelector">An expression that select the target method.</param>
    /// <returns>The same instance for chained calls.</returns>
    ISourceToMethodOptionsBuilder<TSource, TTarget> UseMethod(Expression<Func<TTarget, Delegate>> methodSelector);
}
