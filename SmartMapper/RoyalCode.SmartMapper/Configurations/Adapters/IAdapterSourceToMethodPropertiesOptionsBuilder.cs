using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Configurations.Adapters;

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
public interface IAdapterSourceToMethodPropertiesOptionsBuilder<TSource>
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
    IAdapterParameterStrategyBuilder<TSource, TProperty> Parameter<TProperty>(
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
}
