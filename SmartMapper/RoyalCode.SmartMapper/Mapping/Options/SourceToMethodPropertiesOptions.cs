
namespace RoyalCode.SmartMapper.Mapping.Options;

/// <summary>
/// Options containing configuration to map all properties of the source type to a method of the target type.
/// </summary>
public sealed class SourceToMethodPropertiesOptions
{
    /// <summary>
    /// Creates new <see cref="SourceToMethodPropertiesOptions"/>.
    /// </summary>
    /// <param name="methodOptions"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public SourceToMethodPropertiesOptions(MethodOptions methodOptions)
    {
        MethodOptions = methodOptions ?? throw new ArgumentNullException(nameof(methodOptions));
    }

    /// <summary>
    /// The method options.
    /// </summary>
    public MethodOptions MethodOptions { get; }
}
