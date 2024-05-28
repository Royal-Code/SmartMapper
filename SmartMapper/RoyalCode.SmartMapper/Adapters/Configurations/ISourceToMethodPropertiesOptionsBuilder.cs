using System.Linq.Expressions;
using RoyalCode.SmartMapper.Mapping.Builders;

namespace RoyalCode.SmartMapper.Adapters.Configurations;

/// <summary>
/// <para>
///     Builder to configure the source properties mappings as parameters of a target method.
/// </para>
/// <para>
///     It is possible to map each property of the source to a parameter of the target method,
///     defining the parameter name and assignment strategy.
/// </para>
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
public interface ISourceToMethodPropertiesOptionsBuilder<TSource>
{
    /// <summary>
    /// <para>
    ///     Maps a property of the source type to a parameter in the target type.
    /// </para>
    /// </summary>
    /// <typeparam name="TProperty">The source property type.</typeparam>
    /// <param name="propertySelector">
    ///     An expression to select the property of the source type.
    /// </param>
    /// <param name="parameterName">
    ///     Optional name of the method parameter.
    /// </param>
    /// <returns>
    ///     A builder to configure the parameter strategy options.
    /// </returns>
    IParameterBuilder<TProperty> Parameter<TProperty>(
        Expression<Func<TSource, TProperty>> propertySelector,
        string? parameterName = null);

    /// <summary>
    /// <para>
    ///     Ignore the selected property.
    /// </para>
    /// </summary>
    /// <typeparam name="TProperty">The source property type.</typeparam>
    /// <param name="propertySelector">
    ///     An expression to select the property of the source type.
    /// </param>
    void Ignore<TProperty>(Expression<Func<TSource, TProperty>> propertySelector);

    /// <summary>
    /// <para>
    ///     Map the inner properties of the source property to the method parameters of the target type.
    /// </para>
    /// </summary>
    /// <typeparam name="TInnerProperty">The source property type.</typeparam>
    /// <param name="propertySelector">An expression to select the property of the source type.</param>
    /// <returns>
    ///     A builder to configure the inner properties and parameters strategy options.
    /// </returns>
    ISourceToMethodPropertiesOptionsBuilder<TInnerProperty> InnerProperties<TInnerProperty>(
               Expression<Func<TSource, TInnerProperty>> propertySelector);

    /// <summary>
    /// <para>
    ///     Map the inner properties of the source property to the method parameters of the target type.
    /// </para>
    /// </summary>
    /// <typeparam name="TInnerProperty">The source property type.</typeparam>
    /// <param name="propertySelector">An expression to select the property of the source type.</param>
    /// <param name="configureInnerProperties">An action to configure the inner properties and parameters strategy options.</param>
    void InnerProperties<TInnerProperty>(
        Expression<Func<TSource, TInnerProperty>> propertySelector,
        Action<ISourceToMethodPropertiesOptionsBuilder<TInnerProperty>> configureInnerProperties);
}
