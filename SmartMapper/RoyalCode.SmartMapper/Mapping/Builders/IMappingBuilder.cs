using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Mapping.Builders;

/// <summary>
/// <para>
///     A builder to configure the mapping for adapters.
/// </para>
/// </summary>
public interface IMappingBuilder
{
    /// <summary>
    /// <para>
    ///     Configures the mapping of a source type to a destination type.
    /// </para>
    /// </summary>
    /// <param name="configure">Configuration action.</param>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TTarget">The target type.</typeparam>
    /// <returns>The same instance for chained calls.</returns>
    IMappingBuilder Adapter<TSource, TTarget>(Action<IAdapterBuilder<TSource, TTarget>> configure);

    /// <summary>
    /// <para>
    ///     Gets the configuration for the specified source and destination types.
    /// </para>
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TTarget">The target type.</typeparam>
    /// <returns>
    ///     The configuration for the specified source and destination types.
    /// </returns>
    IAdapterBuilder<TSource, TTarget> Adapter<TSource, TTarget>();

    /// <summary>
    /// <para>
    ///     Configures the mapping of a source type to a destination type.
    /// </para>
    /// </summary>
    /// <param name="configure">Configuration action.</param>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TTarget">The target type.</typeparam>
    /// <returns>The same instance for chained calls.</returns>
    IMappingBuilder Mapper<TSource, TTarget>(Action<IMapperBuilder<TSource, TTarget>> configure);

    /// <summary>
    /// <para>
    ///     Gets the configuration for the specified source and destination types.
    /// </para>
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TTarget">The target type.</typeparam>
    /// <returns>
    ///     The configuration for the specified source and destination types.
    /// </returns>
    IMapperBuilder<TSource, TTarget> Mapper<TSource, TTarget>();
}

/// <summary>
/// <para>
///     A builder to configurate the mapping of a source type to a target type.
/// </para>
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
/// <typeparam name="TTarget">The target type.</typeparam>
public interface IMappingBuilder<TSource, TTarget>
{
    /// <summary>
    /// <para>
    ///     Maps the source type properties to a method of the target type.
    /// </para>
    /// <para>
    ///     The properties of the source type are mapped to the parameters of the target method.
    /// </para>
    /// </summary>
    /// <returns>
    ///     A builder to configure the method mapping options.
    /// </returns>
    ISourceToMethodBuilder<TSource, TTarget> MapToMethod();

    /// <summary>
    /// <para>
    ///     Maps the source type properties to a method of the target type.
    /// </para>
    /// <para>
    ///     The properties of the source type are mapped to the parameters of the target method.
    /// </para>
    /// </summary>
    /// <param name="methodSelector">
    ///     A function to select the method of the target type.
    /// </param>
    /// <returns>
    ///     A builder to configure the method mapping options.
    /// </returns>
    ISourceToMethodBuilder<TSource, TTarget> MapToMethod(Expression<Func<TTarget, Delegate>> methodSelector);

    /// <summary>
    /// <para>
    ///     Maps the source type properties to a method of the target type.
    /// </para>
    /// <para>
    ///     The properties of the source type are mapped to the parameters of the target method.
    /// </para>
    /// </summary>
    /// <param name="methodName">
    ///     The name of the method of the target type.
    /// </param>
    /// <returns>
    ///     A builder to configure the method mapping options.
    /// </returns>
    ISourceToMethodBuilder<TSource, TTarget> MapToMethod(string methodName);

    /// <summary>
    /// <para>
    ///     Configure the mapping of a property from the source type.
    /// </para>
    /// </summary>
    /// <param name="propertySelector">
    ///     An expression to select the property of the source type.
    /// </param>
    /// <typeparam name="TProperty">
    ///     The type of the property of the source type.
    /// </typeparam>
    /// <returns>
    ///     A builder to configure the property mapping options.
    /// </returns>
    IPropertyBuilder<TSource, TTarget, TProperty> Map<TProperty>(Expression<Func<TSource, TProperty>> propertySelector);

    /// <summary>
    /// <para>
    ///     Configure the mapping of a property from the source type.
    /// </para>
    /// </summary>
    /// <param name="propertyName">
    ///     The name of the property of the source type.
    /// </param>
    /// <typeparam name="TProperty">
    ///     The type of the property of the source type.
    /// </typeparam>
    /// <returns>
    ///     A builder to configure the property mapping options.
    /// </returns>
    IPropertyBuilder<TSource, TTarget, TProperty> Map<TProperty>(string propertyName);

    /// <summary>
    /// <para>
    ///     Configure a property to be ignored.
    /// </para>
    /// </summary>
    /// <param name="propertySelector">
    ///     An expression to select the property of the source type to be ignored from mapping.
    /// </param>
    /// <typeparam name="TProperty">
    ///     The type of the property of the source type.
    /// </typeparam>
    void Ignore<TProperty>(Expression<Func<TSource, TProperty>> propertySelector);

    /// <summary>
    /// <para>
    ///     Configure a property to be ignored.
    /// </para>
    /// </summary>
    /// <param name="propertyName">
    ///     The name of the property of the source type to be ignored from mapping.
    /// </param>
    void Ignore(string propertyName);
}
