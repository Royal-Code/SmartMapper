using RoyalCode.SmartMapper.Core.Resolutions;
using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartMapper.Core.Discovery.Assignment;

/// <summary>
/// The result of the assignment discovery process.
/// </summary>
public readonly struct AssignmentDiscoveryResult
{
    /// <summary>
    /// Determines if the assignment strategy was resolved.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Resolution))]
    [MemberNotNullWhen(false, nameof(Failure))]
    public bool IsResolved { get; init; }

    /// <summary>
    /// The resolution of the assignment strategy.
    /// </summary>
    public AssignmentStrategyResolution? Resolution { get; init; }

    /// <summary>
    /// The failure of the resolution process.
    /// </summary>
    public ResolutionFailure? Failure { get; init; }
}
