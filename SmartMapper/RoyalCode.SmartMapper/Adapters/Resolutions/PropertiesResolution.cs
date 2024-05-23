using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Resolutions;

public class PropertiesResolution : ResolutionBase
{
    public PropertiesResolution(ResolutionFailure failure)
    {
        Failure = failure;
        Resolved = false;
        PropertyResolutions = [];
    }
    
    public PropertiesResolution(IEnumerable<PropertyResolution> propertyResolutions)
    {
        PropertyResolutions = propertyResolutions;
        Resolved = true;
    }
    
    public IEnumerable<PropertyResolution> PropertyResolutions { get; }
    
    /// <inheritdoc />
    public override void Completed() { }
}