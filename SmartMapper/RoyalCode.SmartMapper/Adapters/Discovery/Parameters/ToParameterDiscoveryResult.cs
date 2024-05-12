using RoyalCode.SmartMapper.Adapters.Resolutions;
using RoyalCode.SmartMapper.Core.Resolutions;
using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartMapper.Adapters.Discovery.Parameters;

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
