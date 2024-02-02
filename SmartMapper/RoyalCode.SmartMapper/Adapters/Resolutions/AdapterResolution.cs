
using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Resolutions;

public class AdapterResolution : ResolutionBase
{
    // success resolution, not implemented yet
    public AdapterResolution()
    {

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
}

