namespace RoyalCode.SmartMapper.Core.Exceptions;

/// <summary>
/// Exception thrown when the expression for select a property is invalid.
/// </summary>
public class InvalidPropertySelectorException(string propertyName) 
    : ArgumentException("Expression must be a member access expression that is a property.", propertyName)
{ }