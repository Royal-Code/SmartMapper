namespace RoyalCode.SmartMapper.Mapping.Options;

/// <summary>
/// <para>
///     Contains all the options for a single mapping between a source and target type.
/// </para>
/// </summary>
public sealed class MappingOptions
{
    /// <summary>
    /// Constructs a new instance of <see cref="MappingOptions"/> for adapter category.
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TTarget">The target type.</typeparam>
    /// <returns>A new instance of <see cref="MappingOptions"/>.</returns>
    public static MappingOptions AdapterFor<TSource, TTarget>() => new(MappingCategory.Adapter, typeof(TSource), typeof(TTarget));

    /// <summary>
    /// Constructs a new instance of <see cref="MappingOptions"/> for mapper category.
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TTarget">The target type.</typeparam>
    /// <returns>A new instance of <see cref="MappingOptions"/>.</returns>
    public static MappingOptions MapperFor<TSource, TTarget>() => new(MappingCategory.Mapper, typeof(TSource), typeof(TTarget));

    /// <summary>
    /// Creates a new instance of <see cref="MappingOptions"/>.
    /// </summary>
    /// <param name="category">The category.</param>
    /// <param name="sourceType">The source type.</param>
    /// <param name="targetType">The target type.</param>
    public MappingOptions(MappingCategory category, Type sourceType, Type targetType)
    {
        Category = category;
        SourceType = sourceType;
        TargetType = targetType;
        SourceOptions = new SourceOptions(category, sourceType);
        TargetOptions = new TargetOptions(category, targetType);
    }

    internal MappingOptions(SourceOptions sourceOptions, TargetOptions targetOptions)
    {
        SourceType = sourceOptions.SourceType;
        TargetType = targetOptions.TargetType;
        SourceOptions = sourceOptions;
        TargetOptions = targetOptions;
    }

    /// <summary>
    /// The mapping category, adapter or mapper.
    /// </summary>
    public MappingCategory Category { get; }

    /// <summary>
    /// The source type of the mapping.
    /// </summary>
    public Type SourceType { get; }

    /// <summary>
    /// The target type of the mapping.
    /// </summary>
    public Type TargetType { get; }

    /// <summary>
    /// Contains the options for the source properties of the mapping.
    /// </summary>
    public SourceOptions SourceOptions { get; }

    /// <summary>
    /// Contains the options for the target members of the mapping.
    /// </summary>
    public TargetOptions TargetOptions { get; }
}