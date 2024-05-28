using System.Linq.Expressions;
using RoyalCode.SmartMapper.Mapping.Builders;

namespace RoyalCode.SmartMapper.Adapters.Configurations;

/// <summary>
/// <para>
///     Configures the parameters of a method adapted from the source type.
/// </para>
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
public interface ISourceToMethodParametersOptionsBuilder<TSource>
{
    /// <summary>
    /// <para>
    ///     Maps a property of the source type to a parameter in the target type.
    /// </para>
    /// <para>
    ///     The calling order of this method during parameter setting will define the position of the parameter.
    /// </para>
    /// </summary>
    /// <typeparam name="TProperty">The source property type.</typeparam>
    /// <param name="propertySelector">
    ///     An expression to select the property of the source type.
    /// </param>
    /// <returns>
    ///     A builder to configure the parameter mapping options.
    /// </returns>
    IParameterBuilder<TProperty> Parameter<TProperty>(Expression<Func<TSource, TProperty>> propertySelector);

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
