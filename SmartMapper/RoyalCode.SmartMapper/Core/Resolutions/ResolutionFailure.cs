
using RoyalCode.SmartMapper.Core.Exceptions;

namespace RoyalCode.SmartMapper.Core.Resolutions;

/// <summary>
/// Represents a failure in a resolution.
/// </summary>
public sealed class ResolutionFailure
{
    private readonly ICollection<string> messages = [];

    /// <summary>
    /// Create a new instance of <see cref="ResolutionFailure"/>.
    /// </summary>
    public ResolutionFailure() { }

    /// <summary>
    /// Create a new instance of <see cref="ResolutionFailure"/> with a message.
    /// </summary>
    /// <param name="message">A failure message.</param>
    public ResolutionFailure(string message)
    {
        messages.Add(message);
    }

    /// <summary>
    /// Add a message to the failure.
    /// </summary>
    /// <param name="message">The message to add.</param>
    public void AddMessage(string message)
    {
        messages.Add(message);
    }

    /// <summary>
    /// Creates an exception from the failure.
    /// </summary>
    /// <returns>A new exception with the messages of the failure.</returns>
    public ResolutionException CreateException()
    {
        return new(string.Join(Environment.NewLine, messages));
    }
}
