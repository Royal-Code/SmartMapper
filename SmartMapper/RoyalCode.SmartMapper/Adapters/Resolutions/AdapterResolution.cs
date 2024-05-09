
using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Resolutions;

public class AdapterResolution : ResolutionBase
{
    /// <summary>
    /// <para>
    ///     Create a new <see cref="AdapterResolution"/>.
    /// </para>
    /// </summary>
    /// <param name="activationResolution"></param>
    /// <param name="methodsResolutions"></param>
    /// <param name="propertiesResolutions"></param>
    public AdapterResolution(
        ActivationResolution activationResolution,
        MethodsResolutions methodsResolutions,
        PropertiesResolution propertiesResolutions)
    {
        Resolved = true;
        ActivationResolution = activationResolution;
        MethodsResolutions = methodsResolutions;
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
    
    public ActivationResolution? ActivationResolution { get; }
        
    public MethodsResolutions? MethodsResolutions { get; }
        
    public PropertiesResolution? PropertiesResolutions { get; }
}

