
namespace RoyalCode.SmartCommands.Generators.Models.Descriptors;

public sealed class IdPropertyBoundToEntityParameter : IEquatable<IdPropertyBoundToEntityParameter>
{
    public IdPropertyBoundToEntityParameter(ParameterDescriptor parameter, PropertyDescriptor property)
    {
        Parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
        Property = property ?? throw new ArgumentNullException(nameof(property));
    }

    public ParameterDescriptor Parameter { get; }

    public PropertyDescriptor Property { get; }

    public bool Equals(IdPropertyBoundToEntityParameter? other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return Equals(Parameter, other.Parameter) &&
            Equals(Property, other.Property);
    }

    public override bool Equals(object? obj)
    {
        return obj is IdPropertyBoundToEntityParameter other && Equals(other);
    }

    public override int GetHashCode()
    {
        int hashCode = 495548696;
        hashCode = hashCode * -1521134295 + Parameter.GetHashCode();
        hashCode = hashCode * -1521134295 + Property.GetHashCode();
        return hashCode;
    }
}
