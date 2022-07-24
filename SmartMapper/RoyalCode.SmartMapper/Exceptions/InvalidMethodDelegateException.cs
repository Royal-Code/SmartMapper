namespace RoyalCode.SmartMapper.Exceptions;

/// <summary>
///     Thrown when the method selector is not valid, it must select a destination method.
/// </summary>
public class InvalidMethodDelegateException : ArgumentException
{
    public InvalidMethodDelegateException(string propertyName)
        :base("The method selector is invalid, it must select a destination method.", propertyName)
    { }
}