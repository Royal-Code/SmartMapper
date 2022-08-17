using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Configurations.Adapters;

/// <summary>
/// <para>
///     Configures the parameters of a method adapted from the source type.
/// </para>
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
public interface IAdapterSourceToMethodParametersOptionsBuilder<TSource>
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
    IAdapterParameterStrategyBuilder<TProperty> Parameter<TProperty>(
        Expression<Func<TSource, TProperty>> propertySelector);
}