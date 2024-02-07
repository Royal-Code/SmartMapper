
namespace RoyalCode.SmartMapper.Adapters.Options;

/// <summary>
/// <para>
///     Contains all the options for a single mapping between a source and target type.
/// </para>
/// </summary>
public sealed class AdapterOptions
{
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

    /////// <summary>
    /////// Creates a new configuration to map properties of the source type to a target method.
    /////// </summary>
    /////// <returns>A new instance of the <see cref="SourceToMethodOptions"/> class.</returns>
    ////public SourceToMethodOptions CreateSourceToMethodOptions()
    ////{
    ////    var methodOptions = new MethodOptions(TargetType);
    ////    TargetOptions.AddToMethod(methodOptions);
    ////    var sourceToMethodOptions = new SourceToMethodOptions(methodOptions);
    ////    SourceOptions.AddSourceToMethod(sourceToMethodOptions);
        
    ////    return sourceToMethodOptions;
    ////}

    /////// <summary>
    /////// Gets all options of source to method.
    /////// </summary>
    /////// <returns>
    ///////     All configured options of source to method or empty.
    /////// </returns>
    ////public IEnumerable<SourceToMethodOptions> GetSourceToMethodOptions() => SourceOptions.GetSourceToMethodOptions();
}