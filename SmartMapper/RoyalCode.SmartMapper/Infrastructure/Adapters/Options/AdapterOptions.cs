
namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

/// <summary>
/// <para>
///     Contains all the options for a single mapping between a source and target type.
/// </para>
/// </summary>
public class AdapterOptions
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