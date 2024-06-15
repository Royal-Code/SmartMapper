using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolutions;

namespace RoyalCode.SmartMapper.Mapping.Discovery.SourceToMethods;

/// <summary>
/// The result of mapping a source properties to a target method.
/// </summary>
public readonly struct SourceToMethodResult
{
    /// <summary>
    /// Determines if the mapping of a source properties to a target method was resolved.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Resolution))]
    [MemberNotNullWhen(false, nameof(Failure))]
    public bool IsResolved { get; }

    /// <summary>
    /// The resolution.
    /// </summary>
    public SourceToMethodResolution? Resolution { get; }

    /// <summary>
    /// The failure of the resolution process.
    /// </summary>
    public ResolutionFailure? Failure { get; }
}