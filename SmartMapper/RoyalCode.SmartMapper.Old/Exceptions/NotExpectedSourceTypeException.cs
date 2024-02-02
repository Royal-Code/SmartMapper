namespace RoyalCode.SmartMapper.Exceptions;

/// <summary>
/// <para>
///     Thrown when a mapping operation expects a source type, but the type is another.
/// </para>
/// </summary>
public class NotExpectedSourceTypeException : ArgumentException
{
    public NotExpectedSourceTypeException(string paramName, Type expectedType, Type sourceType) 
        : base($"Expected type {expectedType}, got {sourceType}", paramName)
    { }
}