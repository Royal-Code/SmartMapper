namespace RoyalCode.SmartMapper.Exceptions;

public class ResolverNotFoundException : Exception
{
    public ResolverNotFoundException(Type resolverType)
        : base($"The resolver type '{resolverType.FullName}' is not set")
    { }
}