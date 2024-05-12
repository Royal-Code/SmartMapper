
using RoyalCode.SmartMapper.Core.Exceptions;

namespace RoyalCode.SmartMapper.Core.Resolutions;

/// <summary>
/// Represents a failure in a resolution.
/// </summary>
public sealed class ResolutionFailure
{
    private readonly List<string> messages = [];

    /// <summary>
    /// Create a new instance of <see cref="ResolutionFailure"/>.
    /// </summary>
    public ResolutionFailure() { }

    /// <summary>
    /// Get the messages of the failure.
    /// </summary>
    public IEnumerable<string> Messages => messages;

    /// <summary>
    /// Create a new instance of <see cref="ResolutionFailure"/> with a message.
    /// </summary>
    /// <param name="message">A failure message.</param>
    public ResolutionFailure(string message)
    {
        messages.Add(message);
    }
    
    /// <summary>
    /// Create a new instance of <see cref="ResolutionFailure"/> with a message and the messages from another failure.
    /// </summary>
    /// <param name="message">A failure message.</param>
    /// <param name="failure">The failure to get the messages.</param>
    public ResolutionFailure(string message, ResolutionFailure failure)
    {
        messages.Add(message);
        messages.AddRange(failure.Messages);
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
    /// Add messages to the failure.
    /// </summary>
    /// <param name="messages">A collection of messages to add.</param>
    public void AddMessages(IEnumerable<string> messages)
    {
        this.messages.AddRange(messages);
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
