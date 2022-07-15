
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Configurations.Adapters;

/// <summary>
/// <para>
///     A builder to configure the mapping of inner properties of a source property to a target constructor parameters.
/// </para>
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
/// <typeparam name="TSourceProperty">The source property type.</typeparam>
public interface IConstructorPropertyParametersOptionsBuilder<TSource, TSourceProperty>
{
    /// <summary>
    /// <para>
    ///     Maps a property of the source type to a constructor parameter in the target type.
    /// </para>
    /// </summary>
    /// <typeparam name="TProperty">The source property type.</typeparam>
    /// <param name="propertySelector">
    ///     The property selection expression.
    /// </param>
    /// <param name="parameterName">
    ///     The name of the constructor parameter.
    /// </param>
    /// <returns>
    ///     The builder to configure the property to constructor mapping.
    /// </returns>
    IConstructorParameterPropertyOptionsBuilder<TSource, TSourceProperty, TProperty> Parameter<TProperty>(
        Expression<Func<TSourceProperty, TProperty>> propertySelector,
        string? parameterName = null);
}
