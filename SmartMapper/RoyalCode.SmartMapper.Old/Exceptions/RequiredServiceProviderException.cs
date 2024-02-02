namespace RoyalCode.SmartMapper.Exceptions;

/// <summary>
/// <para>
///     Thrown when a adapter operation requires a service provider and none was provided.
/// </para>
/// </summary>
public class RequiredServiceProviderException : ArgumentNullException
{
    public RequiredServiceProviderException(string? paramName, Type sourceType, Type targetType) 
        : base(paramName, $"Service provider is required for adapt type {sourceType} to {targetType}")
    { }
}