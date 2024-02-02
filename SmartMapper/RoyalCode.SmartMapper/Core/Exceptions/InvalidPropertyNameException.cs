namespace RoyalCode.SmartMapper.Core.Exceptions;

/// <summary>
/// <para>
///     Thrown when none property is found in the type for a given name.
/// </para>
/// <para>
///     Thrown, also, when the name is null or whitespace.
/// </para>
/// </summary>
/// <remarks>
///     Initializes a new instance of the <see cref="InvalidPropertyNameException" /> class.
/// </remarks>
/// <param name="message">The message that describes the error.</param>
/// <param name="paramName">The name of the parameter that caused the exception.</param>
public class InvalidPropertyNameException(string? message, string? paramName) 
    : ArgumentException(message, paramName)
{ }