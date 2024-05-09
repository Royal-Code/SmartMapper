using RoyalCode.SmartMapper.Adapters.Resolutions;
using RoyalCode.SmartMapper.Core.Resolutions;
using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartMapper.Adapters.Discovery.PropertyToMethods;

public readonly struct PropertyToMethodResult
{
    /// <summary>
    /// Determines if the mapping of a source properties to a target method was resolved.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Resolution))]
    [MemberNotNullWhen(false, nameof(Failure))]
    public bool IsResolved { get; init; }

    /// <summary>
    /// The resolution.
    /// </summary>
    public PropertyResolution? Resolution { get; init; }

    /// <summary>
    /// The failure of the resolution process.
    /// </summary>
    public ResolutionFailure? Failure { get; init; }
}
