
using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Resolutions;

public class AdapterResolution : ResolutionBase
{
    // success resolution, not implemented yet
    public AdapterResolution(ActivationResolution activationResolution, MethodsResolutions methodsResolutions, PropertiesResolution propertiesResolutions)
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

