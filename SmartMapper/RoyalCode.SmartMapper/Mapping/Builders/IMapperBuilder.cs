namespace RoyalCode.SmartMapper.Mapping.Builders;

/// <summary>
/// <para>
///     A builder to configurate the mapping for mapper of a source type to a target type.
/// </para>
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
/// <typeparam name="TTarget">The target type.</typeparam>
public interface IMapperBuilder<TSource, TTarget> : IMappingBuilder<TSource, TTarget> { }
