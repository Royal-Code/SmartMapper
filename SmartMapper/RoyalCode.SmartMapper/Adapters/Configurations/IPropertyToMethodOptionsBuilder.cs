
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Adapters.Configurations;

/// <summary>
/// <para>
///     A builder to configure the mapping of a source property to a target method.
/// </para>
/// </summary>
/// <typeparam name="TTarget">The destination type.</typeparam>
/// <typeparam name="TSourceProperty">The source property type.</typeparam>
public interface IPropertyToMethodOptionsBuilder<TTarget, TSourceProperty>
{
    /// <summary>
    /// <para>
    ///     In this configuration, internal properties of the type of the source property are mapped to parameters.
    /// </para>
    /// </summary>
    /// <param name="configureParameters"></param>
    /// <returns></returns>
    void Parameters(Action<IPropertyToParametersOptionsBuilder<TSourceProperty>> configureParameters);

    /// <summary>
    /// <para>
    ///     In this configuration, the source property is mapped as a method parameter, 
    ///     where the method should have a single parameter.
    /// </para>
    /// </summary>
    /// <param name="configureProperty"></param>
    void ToParameter(Action<IToParameterOptionsBuilder<TSourceProperty>>? configureProperty = null);

    /// <summary>
    /// <para>
    ///     Configure the name of the target method.
    /// </para>
    /// </summary>
    /// <param name="name">The name of method.</param>
    /// <returns>The same instance for chained calls.</returns>
    IPropertyToMethodOptionsBuilder<TTarget, TSourceProperty> UseMethod(string name);

    /// <summary>
    /// <para>
    ///     Configure the target method using a expression selector.
    /// </para>
    /// </summary>
    /// <param name="methodSelector">An expression that select the target method.</param>
    /// <returns>The same instance for chained calls.</returns>
    IPropertyToMethodOptionsBuilder<TTarget, TSourceProperty> UseMethod(
        Expression<Func<TTarget, Delegate>> methodSelector);
}
