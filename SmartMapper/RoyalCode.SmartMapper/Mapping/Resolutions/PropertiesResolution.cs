using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Mapping.Resolutions;

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
    
    /// <summary>
    /// Check if the resolution has a failure.
    /// </summary>
    [MemberNotNullWhen(false, nameof(PropertyResolutions))]
    public bool HasFailure([NotNullWhen(true)] out ResolutionFailure? failure)
    {
        failure = Failure;
        return !Resolved;
    }
    
    public IEnumerable<PropertyResolution> PropertyResolutions { get; }
    
    /// <inheritdoc />
    public override void Completed() { }
}