
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Adapters.Configurations;

/// <summary>
/// <para>
///     A builder to configurate the mapping for adapters.
/// </para>
/// </summary>
public interface IAdapterOptionsBuilder
{
    /// <summary>
    /// <para>
    ///     Configures the mapping of a source type to a destination type.
    /// </para>
    /// </summary>
    /// <param name="configure">Configuration action.</param>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TTarget">The destination type.</typeparam>
    /// <returns>The same instance for chained calls.</returns>
    IAdapterOptionsBuilder Configure<TSource, TTarget>(Action<IAdapterOptionsBuilder<TSource, TTarget>> configure);

    /// <summary>
    /// <para>
    ///     Gets the configuration for the specified source and destination types.
    /// </para>
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TTarget">The destination type.</typeparam>
    /// <returns>
    ///     The configuration for the specified source and destination types.
    /// </returns>
    IAdapterOptionsBuilder<TSource, TTarget> Configure<TSource, TTarget>();
}

/// <summary>
/// <para>
///     A builder to configurate the mapping for adapter of a source type to a destination type.
/// </para>
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
/// <typeparam name="TTarget">The destination type.</typeparam>
public interface IAdapterOptionsBuilder<TSource, TTarget>
{
    /// <summary>
    /// <para>
    ///     Configure the constructor that will be used to create the destination instance.
    /// </para>
    /// </summary>
    /// <returns>
    ///     A builder to configure the constructor mapping options.
    /// </returns>
    IConstructorOptionsBuilder<TSource> Constructor();

    /// <summary>
    /// <para>
    ///     Adapts the source type to a method of the destination type.
    /// </para>
    /// <para>
    ///     The properties of the source type are mapped to the parameters of the destination method.
    /// </para>
    /// </summary>
    /// <returns>
    ///     A builder to configure the method mapping options.
    /// </returns>
    ISourceToMethodOptionsBuilder<TSource, TTarget> MapToMethod();

    /// <summary>
    /// <para>
    ///     Adapts the source type to a method of the destination type.
    /// </para>
    /// <para>
    ///     The properties of the source type are mapped to the parameters of the destination method.
    /// </para>
    /// </summary>
    /// <param name="methodSelector">
    ///     A function to select the method of the destination type.
    /// </param>
    /// <returns>
    ///     A builder to configure the method mapping options.
    /// </returns>
    ISourceToMethodOptionsBuilder<TSource, TTarget> MapToMethod(Expression<Func<TTarget, Delegate>> methodSelector);

    /// <summary>
    /// <para>
    ///     Adapts the source type to a method of the destination type.
    /// </para>
    /// <para>
    ///     The properties of the source type are mapped to the parameters of the destination method.
    /// </para>
    /// </summary>
    /// <param name="methodName">
    ///     The name of the method of the destination type.
    /// </param>
    /// <returns>
    ///     A builder to configure the method mapping options.
    /// </returns>
    ISourceToMethodOptionsBuilder<TSource, TTarget> MapToMethod(string methodName);

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
    IPropertyOptionsBuilder<TSource, TTarget, TProperty> Map<TProperty>(Expression<Func<TSource, TProperty>> propertySelector);

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
    IPropertyOptionsBuilder<TSource, TTarget, TProperty> Map<TProperty>(string propertyName);
}