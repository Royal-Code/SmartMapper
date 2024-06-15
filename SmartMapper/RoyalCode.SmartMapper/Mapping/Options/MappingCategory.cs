namespace RoyalCode.SmartMapper.Mapping.Options;

/// <summary>
/// Defines the mapping category, which can be adapter or mapper.
/// </summary>
public enum MappingCategory
{
    /// <summary>
    /// Adapter is used to map source properties to target constructors, methods and properties.
    /// </summary>
    Adapter,

    /// <summary>
    /// Mapper is used to map properties from the source to an instance of the target,
    /// which can be methods or properties.
    /// </summary>
    Mapper,
    
    /// <summary>
    /// Used for mapping inner properties.
    /// </summary>
    Inner,
}
