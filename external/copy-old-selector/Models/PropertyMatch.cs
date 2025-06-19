using RoyalCode.SmartSelector.Generators.Models.Descriptors;

namespace RoyalCode.SmartSelector.Generators.Models;

/// <summary>
/// A result of matching a property from the origin type to a property in the target type.
/// </summary>
internal class PropertyMatch(PropertyDescriptor origin, PropertySelection? target, AssignDescriptor? assignDescriptor) : IEquatable<PropertyMatch>
{
    /// <summary>
    /// The origin property type descriptor. (DTO property)
    /// </summary>
    public PropertyDescriptor Origin { get; } = origin;

    /// <summary>
    /// The target property selection. (Entity property)
    /// </summary>
    public PropertySelection? Target { get; } = target;

    /// <summary>
    /// The assign descriptor that describes how to assign the origin property to the target property.
    /// </summary>
    public AssignDescriptor? AssignDescriptor { get; } = assignDescriptor;

    /// <summary>
    /// Determines if the target property selection is missing.
    /// </summary>
    public bool IsMissing => Target is null;

    /// <summary>
    /// Determine if the properties are compatible and has a valid assign descriptor.
    /// </summary>
    public bool CanAssign => AssignDescriptor is not null && !IsMissing;

    public bool Equals(PropertyMatch other)
    {
        if (other is null)
            return false;
        
        if (ReferenceEquals(this, other))
            return true;

        return Origin.Equals(other.Origin) &&
            Equals(Target, other.Target) &&
            Equals(AssignDescriptor, other.AssignDescriptor);

    }

    public override bool Equals(object? obj)
    {
        return obj is PropertyMatch other && Equals(other);
    }

    public override int GetHashCode()
    {
        int hashCode = -1013312977;
        hashCode = hashCode * -1521134295 + EqualityComparer<PropertyDescriptor>.Default.GetHashCode(Origin);
        hashCode = hashCode * -1521134295 + EqualityComparer<PropertySelection?>.Default.GetHashCode(Target);
        hashCode = hashCode * -1521134295 + EqualityComparer<AssignDescriptor?>.Default.GetHashCode(AssignDescriptor);
        return hashCode;
    }
}
