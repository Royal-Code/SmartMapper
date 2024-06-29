using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;
using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

namespace RoyalCode.SmartMapper.Mapping.Resolutions;

/// <summary>
/// A resolution for a property mapped to a property.
/// </summary>
public abstract class PropertyResolution : ResolutionBase
{
    /// <summary>
    /// Creates a failed resolution of <see cref="PropertyResolution"/>.
    /// </summary>
    /// <param name="failure">The failure of the resolution.</param>
    /// <returns>A failed resolution of <see cref="PropertyResolution"/>.</returns>
    public static PropertyResolution Fail(ResolutionFailure failure) => new FailurePropertyResolution(failure);
    
    /// <summary>
    /// Constructor for failed resolution of <see cref="PropertyResolution"/> .
    /// </summary>
    /// <param name="failure">The failure of the resolution.</param>
    protected PropertyResolution(ResolutionFailure failure)
    {
        Resolved = false;
        Failure = failure;
    }
    
    /// <summary>
    /// Constructor for successful resolution of <see cref="PropertyResolution"/> .
    /// </summary>
    /// <param name="availableSourceProperty">The available source property.</param>
    /// <param name="targetProperty">The target property.</param>
    protected PropertyResolution(AvailableSourceProperty availableSourceProperty, TargetProperty targetProperty)
    {
        Resolved = true;
        AvailableSourceProperty = availableSourceProperty;
        TargetProperty = targetProperty;
    }
    
    /// <summary>
    /// The available source property.
    /// </summary>
    public AvailableSourceProperty? AvailableSourceProperty { get; }

    /// <summary>
    /// The target parameter.
    /// </summary>
    public TargetProperty? TargetProperty { get; }

    /// <inheritdoc />
    public override void Completed()
    {
        TargetProperty?.ResolvedBy(this);
        AvailableSourceProperty?.Completed();
    }
    
    private sealed class FailurePropertyResolution : PropertyResolution
    {
        public FailurePropertyResolution(ResolutionFailure failure) : base(failure) { }
    }
}