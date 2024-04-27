using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Adapters.Configurations;

/// <summary>
/// <para>
///     A builder to configure the mapping of the internal properties of a source property.
/// </para>
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
/// <typeparam name="TTarget">The destination type.</typeparam>
/// <typeparam name="TSourceProperty">The source property type.</typeparam>
public interface IInnerPropertiesOptionsBuilder<TSource, TTarget, TSourceProperty>
{
    /// <summary>
    /// <para>
    ///     Configure the mapping of a property from the source property type.
    /// </para>
    /// </summary>
    /// <param name="propertySelector">
    ///     An expression to select the property of the source property type.
    /// </param>
    /// <typeparam name="TProperty">
    ///     The type of the property of the source property type.
    /// </typeparam>
    /// <returns>
    ///     A builder to configure the property mapping options.
    /// </returns>
    IPropertyOptionsBuilder<TSourceProperty, TTarget, TProperty> Map<TProperty>(Expression<Func<TSourceProperty, TProperty>> propertySelector);

    /// <summary>
    /// <para>
    ///     Configure the mapping of a property from the source property type.
    /// </para>
    /// </summary>
    /// <param name="propertyName">
    ///     The name of the property of the source property type.
    /// </param>
    /// <typeparam name="TProperty">
    ///     The type of the property of the source property type.
    /// </typeparam>
    /// <returns>
    ///     A builder to configure the property mapping options.
    /// </returns>
    IPropertyOptionsBuilder<TSourceProperty, TTarget, TProperty> Map<TProperty>(string propertyName);
    
    /// <summary>
    /// <para>
    ///     Configure a property to be ignored.
    /// </para>
    /// </summary>
    /// <param name="propertySelector">
    ///     An expression to select the property of the source property type to be ignored from mapping.
    /// </param>
    /// <typeparam name="TProperty">
    ///     The type of the property of the source property type.
    /// </typeparam>
    void Ignore<TProperty>(Expression<Func<TSource, TProperty>> propertySelector);
    
    /// <summary>
    /// <para>
    ///     Configure a property to be ignored.
    /// </para>
    /// </summary>
    /// <param name="propertyName">
    ///     The name of the property of the source property type to be ignored from mapping.
    /// </param>
    void Ignore(string propertyName);
}