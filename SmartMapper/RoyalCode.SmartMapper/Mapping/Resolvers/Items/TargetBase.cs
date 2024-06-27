using RoyalCode.SmartMapper.Core.Resolutions;
using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartMapper.Mapping.Resolvers.Items;

/// <summary>
/// A base class for containers that holds target members information.
/// </summary>
public abstract class TargetBase
{
    /// <summary>
    /// The resolution of the target member, if resolved.
    /// </summary>
    public ResolutionBase? Resolution { get; private set; }

    /// <summary>
    /// Checks if the target member is available for resolution.
    /// </summary>
    [MemberNotNullWhen(false, nameof(Resolution))]
    public bool IsAvailable => Resolution is null;

    /// <summary>
    /// Checks if the target member is resolved.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Resolution))]
    public bool IsResolved => Resolution is not null;

    /// <summary>
    /// Sets the resolution of the target member.
    /// </summary>
    /// <param name="resolution">The resolution.</param>
    public void ResolvedBy(ResolutionBase resolution)
    {
        Resolution = resolution;
    }
}
