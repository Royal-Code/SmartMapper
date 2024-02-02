namespace RoyalCode.SmartMapper.Exceptions;

public class DiscoveryNotFoundException : Exception
{
    public DiscoveryNotFoundException(Type discoveryType)
        : base($"The discovery type '{discoveryType.FullName}' is not set")
    { }
}