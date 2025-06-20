namespace RoyalCode.SmartCommands.Generators.Models.Descriptors;

public sealed class ServiceTypeDescriptor : IEquatable<ServiceTypeDescriptor>
{
    public ServiceTypeDescriptor(TypeDescriptor interfaceType, TypeDescriptor handlerType)
    {
        InterfaceType = interfaceType;
        HandlerType = handlerType;
    }

    public TypeDescriptor InterfaceType { get; }

    public TypeDescriptor HandlerType { get; }

    public bool Equals(ServiceTypeDescriptor? other)
    {
        return other is not null &&
            Equals(InterfaceType, other.InterfaceType) &&
            Equals(HandlerType, other.HandlerType);
    }

    public override bool Equals(object? obj)
    {
        return obj is ServiceTypeDescriptor descriptor && Equals(descriptor);
    }

    public override int GetHashCode()
    {
        int hashCode = 1245597379;
        hashCode = hashCode * -1521134295 + InterfaceType.GetHashCode();
        hashCode = hashCode * -1521134295 + HandlerType.GetHashCode();
        return hashCode;
    }
}
