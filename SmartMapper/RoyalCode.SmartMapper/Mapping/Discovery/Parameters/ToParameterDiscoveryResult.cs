using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolutions;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Parameters;

/// <summary>
/// The result of the assignment discovery process.
/// </summary>
public readonly struct ToParameterDiscoveryResult
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
    public ParameterResolution? Resolution { get; init; }

    /// <summary>
    /// The failure of the resolution process.
    /// </summary>
    public ResolutionFailure? Failure { get; init; }
}
