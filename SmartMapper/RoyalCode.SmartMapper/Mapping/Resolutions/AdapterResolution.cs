using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Mapping.Resolutions;

/// <summary>
/// Resolution for an adapter.
/// </summary>
public class AdapterResolution : ResolutionBase
{
    /// <summary>
    /// <para>
    ///     Create a new <see cref="AdapterResolution"/>.
    /// </para>
    /// </summary>
    /// <param name="activationResolution"></param>
    /// <param name="sourceToMethodsResolutions"></param>
    /// <param name="propertiesResolutions"></param>
    public AdapterResolution(
        ActivationResolution activationResolution,
        SourceToMethodsResolutions sourceToMethodsResolutions,
        PropertiesResolution propertiesResolutions)
    {
        Resolved = true;
        ActivationResolution = activationResolution;
        SourceToMethodsResolutions = sourceToMethodsResolutions;
        PropertiesResolutions = propertiesResolutions;
    }

    /// <summary>
    /// Failure resolution.
    /// </summary>
    /// <param name="failure">The failure of the resolution.</param>
    public AdapterResolution(ResolutionFailure failure)
    {
        Resolved = false;
        Failure = failure;
    }
    
    /// <summary>
    /// Check if the resolution has a failure.
    /// </summary>
    [MemberNotNullWhen(false, nameof(ActivationResolution), nameof(SourceToMethodsResolutions), nameof(PropertiesResolutions))]
    public bool HasFailure([NotNullWhen(true)] out ResolutionFailure? failure)
    {
        failure = Failure;
        return !Resolved;
    }
    
    /// <summary>
    /// The activation resolution.
    /// </summary>
    public ActivationResolution? ActivationResolution { get; }
    
    /// <summary>
    /// The source to methods resolutions.
    /// </summary>
    public SourceToMethodsResolutions? SourceToMethodsResolutions { get; }
    
    /// <summary>
    /// The properties resolutions.
    /// </summary>
    public PropertiesResolution? PropertiesResolutions { get; }
    
    /// <inheritdoc />
    public override void Completed() { }
}

