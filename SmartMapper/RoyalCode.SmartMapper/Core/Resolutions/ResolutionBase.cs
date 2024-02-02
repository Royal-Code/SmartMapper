using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartMapper.Core.Resolutions;

/// <summary>
/// A base class for resolution results.
/// </summary>
public abstract class ResolutionBase
{
    /// <summary>
    /// Determine whether the resolution has been resolved.
    /// </summary>
    [MemberNotNullWhen(false, nameof(Failure))]
    public bool Resolved { get; init; }
    
    /// <summary>
    /// Contains the failure of the resolution, if any.
    /// </summary>
    public ResolutionFailure? Failure { get; init; }
}