
namespace RoyalCode.SmartMapper.Adapters.Options;

/// <summary>
/// <para>
///     Contains all the options for a single mapping between a source and target type.
/// </para>
/// </summary>
public sealed class AdapterOptions
{
    /// <summary>
    /// Constructs a new instance of <see cref="AdapterOptions"/>.
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TTarget">The target type.</typeparam>
    /// <returns>A new instance of <see cref="AdapterOptions"/>.</returns>
    public static AdapterOptions For<TSource, TTarget>() => new(typeof(TSource), typeof(TTarget));
    
    /// <summary>
    /// Creates a new instance of <see cref="AdapterOptions"/>.
    /// </summary>
    /// <param name="sourceType">The source type.</param>
    /// <param name="targetType">The target type.</param>
    public AdapterOptions(Type sourceType, Type targetType)
    {
        SourceType = sourceType;
        TargetType = targetType;
        SourceOptions = new SourceOptions(sourceType);
        TargetOptions = new TargetOptions(targetType);
    }

    internal AdapterOptions(SourceOptions sourceOptions, TargetOptions targetOptions)
    {
        SourceType = sourceOptions.SourceType;
        TargetType = targetOptions.TargetType;
        SourceOptions = sourceOptions;
        TargetOptions = targetOptions;
    }

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