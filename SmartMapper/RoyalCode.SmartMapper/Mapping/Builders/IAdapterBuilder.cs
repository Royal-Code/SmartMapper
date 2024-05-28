namespace RoyalCode.SmartMapper.Mapping.Builders;

/// <summary>
/// <para>
///     A builder to configurate the mapping for adapter of a source type to a target type.
/// </para>
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
/// <typeparam name="TTarget">The target type.</typeparam>
public interface IAdapterBuilder<TSource, TTarget> : IMappingBuilder<TSource, TTarget>
{
    /// <summary>
    /// <para>
    ///     Configure the constructor that will be used to create the target instance.
    /// </para>
    /// </summary>
    /// <returns>
    ///     A builder to configure the constructor mapping options.
    /// </returns>
    IConstructorBuilder<TSource> Constructor();
}