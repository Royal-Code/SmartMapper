using RoyalCode.SmartMapper.Adapters.Resolutions;
using RoyalCode.SmartMapper.Core.Resolutions;
using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartMapper.Adapters.Discovery.SourceToMethods;

/// <summary>
/// The result of mapping a source properties to a target method.
/// </summary>
public sealed class SourceToMethodResult
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