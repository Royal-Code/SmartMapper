using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Mapping.Resolutions;

/// <summary>
/// All method resolutions of the adapter.
/// </summary>
public sealed class SourceToMethodsResolutions : ResolutionBase
{
    /// <summary>
    /// Creates a new instance of <see cref="SourceToMethodsResolutions"/> with the given <see cref="ResolutionFailure"/>.
    /// </summary>
    /// <param name="failure">The failure of the resolution.</param>
    public SourceToMethodsResolutions(ResolutionFailure failure)
    {
        Resolved = false;
        Failure = failure;
        Resolutions = [];
    }
    
    /// <summary>
    /// Creates a new instance of <see cref="SourceToMethodsResolutions"/> with the given <see cref="SourceToMethodResolution"/>.
    /// </summary>
    /// <param name="resolutions">The resolutions of the methods.</param>
    public SourceToMethodsResolutions(IEnumerable<SourceToMethodResolution> resolutions)
    {
        Resolved = true;
        Failure = null;
        Resolutions = resolutions;
    }
    
    /// <summary>
    /// Check if the resolution has a failure.
    /// </summary>
    [MemberNotNullWhen(false, nameof(Resolutions))]
    public bool HasFailure([NotNullWhen(true)] out ResolutionFailure? failure)
    {
        failure = Failure;
        return !Resolved;
    }
    
    /// <summary>
    /// <para>
    ///     Contains all the method resolutions of the adapter.
    /// </para>
    /// <para>
    ///     It will be empty if a failure occurs.
    /// </para>
    /// </summary>
    public IEnumerable<SourceToMethodResolution> Resolutions { get; }

    /// <inheritdoc />
    public override void Completed() { }
}