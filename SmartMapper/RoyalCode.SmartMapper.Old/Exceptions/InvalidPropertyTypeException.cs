namespace RoyalCode.SmartMapper.Exceptions;

/// <summary>
/// <para>
///     Thrown when the property is found in the type for a given name but the informed type is not compatible with the property.
/// </para>
/// </summary>
public class InvalidPropertyTypeException : ArgumentException
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="InvalidPropertyTypeException" /> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="paramName">The name of the parameter that caused the exception.</param>
    public InvalidPropertyTypeException(string? message, string? paramName) : base(message, paramName) { }
}