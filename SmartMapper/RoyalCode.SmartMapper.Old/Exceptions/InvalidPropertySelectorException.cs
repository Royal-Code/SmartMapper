namespace RoyalCode.SmartMapper.Exceptions;

/// <summary>
/// Exception thrown when the expression for select a property is invalid.
/// </summary>
public class InvalidPropertySelectorException : ArgumentException
{
    public InvalidPropertySelectorException(string propertyName)
        :base("Expression must be a member access expression that is a property.", propertyName)
    { }
}