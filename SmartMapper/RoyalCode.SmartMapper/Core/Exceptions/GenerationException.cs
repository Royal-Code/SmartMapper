
namespace RoyalCode.SmartMapper.Core.Exceptions;

/// <summary>
/// Exception thrown when an expression cannot be generated.
/// </summary>
/// <param name="message">The message of the exception.</param>
public sealed class GenerationException(string message) : Exception(message) { }
