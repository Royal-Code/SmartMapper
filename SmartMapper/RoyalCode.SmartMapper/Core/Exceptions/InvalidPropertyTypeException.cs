namespace RoyalCode.SmartMapper.Core.Exceptions;

/// <summary>
/// <para>
///     Thrown when the property is found in the type for a given name but the informed type is not compatible with the property.
/// </para>
/// </summary>
/// <remarks>
///     Initializes a new instance of the <see cref="InvalidPropertyTypeException" /> class.
/// </remarks>
/// <param name="message">The message that describes the error.</param>
/// <param name="paramName">The name of the parameter that caused the exception.</param>
public class InvalidPropertyTypeException(string? message, string? paramName)
    : ArgumentException(message, paramName)
{ }