using RoyalCode.SmartSelector.Generators.Models.Descriptors;

namespace RoyalCode.SmartSelector.Generators.Models;

/// <summary>
/// Represent a type and its properties that can be matched to another type.
/// </summary>
internal ref struct MatchTypeInfo
{
    /// <summary>
    /// Creates a new instance of <see cref="MatchTypeInfo"/>.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="properties"></param>
    public MatchTypeInfo(TypeDescriptor type, IReadOnlyList<PropertyDescriptor> properties)
    {
        Type = type;
        Properties = properties;
    }

    /// <summary>
    /// The type descriptor of the current type.
    /// </summary>
    public TypeDescriptor Type { get; }

    /// <summary>
    /// The properties of the current type.
    /// </summary>
    public IReadOnlyList<PropertyDescriptor> Properties { get; }
}