namespace RoyalCode.SmartMapper.Core.Exceptions;

/// <summary>
/// <para>
///     Thrown when none method is found in the destination type for a given name.
/// </para>
/// <para>
///     Thrown, also, when the name is null or whitespace.
/// </para>
/// </summary>
/// <remarks>
///     Initializes a new instance of the <see cref="InvalidMethodNameException" /> class.
/// </remarks>
/// <param name="message">The message that describes the error.</param>
/// <param name="paramName">The name of the parameter that caused the exception.</param>
public sealed class InvalidMethodNameException(string? message, string? paramName) 
    : ArgumentException(message, paramName)
{ }