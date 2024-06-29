namespace RoyalCode.SmartMapper.Mapping.Options.Resolutions;

/// <summary>
/// The resolution strategy when a source property is mapped to a target property.
/// </summary>
public enum ToPropertyResolutionStrategy
{
    /// <summary>
    /// The source property value will be assigned to the target property. This is the default strategy.
    /// </summary>
    AssignValue,

    /// <summary>
    /// The source property value will be used by another resolution,
    /// to assign the value to the target navigation property
    /// or call a method of a target property.
    /// </summary>
    Then, // TODO: restore the old strategies, like MemberAccess and MethodCall
}
