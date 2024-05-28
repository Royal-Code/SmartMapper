
using System.Linq.Expressions;
using RoyalCode.SmartMapper.Mapping.Builders;

namespace RoyalCode.SmartMapper.Adapters.Configurations;

/// <summary>
/// <para>
///     A builder to configure the mapping of the internal properties of a source property 
///     to parameters of a target method or constructor.
/// </para>
/// </summary>
/// <typeparam name="TSourceProperty">The source property.</typeparam>
public interface IPropertyToParametersOptionsBuilder<TSourceProperty>
{
    /// <summary>
    /// <para>
    ///     Maps an internal property of the type of the source property to a parameter of method or constructor.
    /// </para>
    /// </summary>
    /// <typeparam name="TProperty">The source property type.</typeparam>
    /// <param name="propertySelector">
    ///     The property selection expression.
    /// </param>
    /// <param name="parameterName">
    ///     The name of the parameter.
    /// </param>
    /// <returns>
    ///     The builder to configure the property to parameter method mapping.
    /// </returns>
    IParameterBuilder<TProperty> Parameter<TProperty>(
        Expression<Func<TSourceProperty, TProperty>> propertySelector,
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
    void Ignore<TProperty>(Expression<Func<TSourceProperty, TProperty>> propertySelector);
}
