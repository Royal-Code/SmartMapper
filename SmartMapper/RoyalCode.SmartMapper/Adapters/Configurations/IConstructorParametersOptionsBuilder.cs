using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Adapters.Configurations;

/// <summary>
/// <para>
///     An option builder to configure the parameters of the target type constructor.
/// </para>
/// <para>
///     The goal here is to define which properties will be part of the constructor.
/// </para>
/// <para>
///     The constructor will be selected by a best constructor selector algorithm.
///     If the constructor has parameters defined,
///     these parameters will have to be in the constructor where the algorithm will take them into account.
/// </para>
/// <para>
///     The algorithm will always try to map the properties to the constructor parameters based on the name.
/// </para>
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
public interface IConstructorParametersOptionsBuilder<TSource>
{
    /// <summary>
    /// <para>
    ///     Maps a property of the source type to a parameter in the constructor target type.
    /// </para>
    /// </summary>
    /// <typeparam name="TProperty">The source property type.</typeparam>
    /// <param name="propertySelector">
    ///     An expression to select the property of the source type.
    /// </param>
    /// <param name="parameterName">
    ///     Optional name of the constructor parameter.
    /// </param>
    /// <returns>
    ///     A builder to configure the parameter strategy options.
    /// </returns>
    IToParameterOptionsBuilder<TProperty> Parameter<TProperty>(
        Expression<Func<TSource, TProperty>> propertySelector,
        string? parameterName = null);

    /// <summary>
    /// <para>
    ///     Map the inner properties of the source property to the constructor parameters of the target type.
    /// </para>
    /// </summary>
    /// <typeparam name="TInnerProperty">The source property type.</typeparam>
    /// <param name="propertySelector">An expression to select the property of the source type.</param>
    /// <returns>
    ///     A builder to configure the inner properties and parameters strategy options.
    /// </returns>
    IConstructorParametersOptionsBuilder<TInnerProperty> InnerProperties<TInnerProperty>(
               Expression<Func<TSource, TInnerProperty>> propertySelector);

    /// <summary>
    /// <para>
    ///     Map the inner properties of the source property to the constructor parameters of the target type.
    /// </para>
    /// </summary>
    /// <typeparam name="TInnerProperty">The source property type.</typeparam>
    /// <param name="propertySelector">An expression to select the property of the source type.</param>
    /// <param name="configureInnerProperties">An action to configure the inner properties and parameters strategy options.</param>
    void InnerProperties<TInnerProperty>(
        Expression<Func<TSource, TInnerProperty>> propertySelector,
        Action<IConstructorParametersOptionsBuilder<TInnerProperty>> configureInnerProperties);
}