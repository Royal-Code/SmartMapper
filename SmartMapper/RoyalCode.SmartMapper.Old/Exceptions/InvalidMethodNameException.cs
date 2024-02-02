namespace RoyalCode.SmartMapper.Exceptions;

/// <summary>
/// <para>
///     Thrown when none method is found in the destination type for a given name.
/// </para>
/// <para>
///     Thrown, also, when the name is null or whitespace.
/// </para>
/// </summary>
public class InvalidMethodNameException : ArgumentException
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="InvalidMethodNameException" /> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="paramName">The name of the parameter that caused the exception.</param>
    public InvalidMethodNameException(string? message, string? paramName) : base(message, paramName) { }
}