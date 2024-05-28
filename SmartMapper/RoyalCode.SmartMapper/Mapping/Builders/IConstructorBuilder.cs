using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Mapping.Builders;

/// <summary>
/// <para>
///     A builder to configurate the mapping of the constructor of the target type.
/// </para>
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
public interface IConstructorBuilder<TSource>
{
    /// <summary>
    /// <para>
    ///     Configure the parameters of the constructor of the target type.
    /// </para>
    /// <para>
    ///     This configuration requires the same number of parameters as the constructor of the target type.
    /// </para>
    /// </summary>
    /// <param name="configureParameters"></param>
    void Parameters(Action<IConstructorParametersBuilder<TSource>> configureParameters);

    /// <summary>
    /// <para>
    ///     Map many properties of the source type to the constructor parameters of the target type.
    /// </para>
    /// <para>
    ///     To advanced mapping, use the <see cref="Parameters"/> method.
    /// </para>
    /// </summary>
    /// <param name="propertySelectors">
    ///     The properties of the source type.
    /// </param>
    void Map(params Expression<Func<TSource, object>>[] propertySelectors);

    /// <summary>
    /// <para>
    ///     Adds information about the constructor of the target type.
    ///     Set the number of parameters that the constructor of the target type has.
    /// </para>
    /// </summary>
    /// <param name="numberOfParameters">The number of parameters used to select the constructor.</param>
    void WithParameters(int numberOfParameters);

    /// <summary>
    /// <para>
    ///     Adds information about the constructor of the target type.
    ///     Set the paramters types that the constructor of the target type has.
    /// </para>
    /// </summary>
    /// <param name="parameterTypes">The parameters types.</param>
    void WithParameters(params Type[] parameterTypes);
}
