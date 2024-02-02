namespace RoyalCode.SmartMapper.Core.Exceptions;

/// <summary>
///     Thrown when the method selector is not valid, it must select a destination method.
/// </summary>
public sealed class InvalidMethodDelegateException(string propertyName) 
    : ArgumentException("The method selector is invalid, it must select a destination method.", propertyName)
{ }