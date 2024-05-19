using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Resolutions;

/// <summary>
/// All method resolutions of the adapter.
/// </summary>
public sealed class MethodsResolutions : ResolutionBase
{
    /// <summary>
    /// Creates a new instance of <see cref="MethodsResolutions"/> with the given <see cref="ResolutionFailure"/>.
    /// </summary>
    /// <param name="failure">The failure of the resolution.</param>
    public MethodsResolutions(ResolutionFailure failure)
    {
        Resolved = false;
        Failure = failure;
        Resolutions = [];
    }
    
    /// <summary>
    /// Creates a new instance of <see cref="MethodsResolutions"/> with the given <see cref="SourceToMethodResolution"/>.
    /// </summary>
    /// <param name="resolutions">The resolutions of the methods.</param>
    public MethodsResolutions(IEnumerable<SourceToMethodResolution> resolutions)
    {
        Resolved = true;
        Failure = null;
        Resolutions = resolutions;
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
}