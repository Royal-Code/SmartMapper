
namespace RoyalCode.SmartMapper.Core.Exceptions;

/// <summary>
/// An exception that is thrown when a resolution for an adapter, selector, or mapper could not be done.
/// </summary>
/// <param name="message">
///     The message of the exception.
/// </param>
public sealed class ResolutionException(string message) : Exception(message) { }
