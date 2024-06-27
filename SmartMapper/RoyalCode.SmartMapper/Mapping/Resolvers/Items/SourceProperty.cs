using RoyalCode.SmartMapper.Core.Extensions;
using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Options;
using RoyalCode.SmartMapper.Mapping.Options.Resolutions;

namespace RoyalCode.SmartMapper.Mapping.Resolvers.Items;

/// <summary>
/// Represent a source property of the mapping and the current resolution state.
/// </summary>
public sealed class SourceProperty
{
    /// <summary>
    /// Creates a new list of <see cref="SourceProperty"/> from a type.
    /// </summary>
    /// <param name="type">The type of the source.</param>
    /// <param name="options">The options for the source properties.</param>
    /// <returns>A new list of <see cref="SourceProperty"/> with new instances of the source items.</returns>
    public static List<SourceProperty> Create(Type type, SourceOptions options)
    {
        // gets the properties of the source that should be mapped
        var sourceProperties = type.GetSourceProperties();

        // gets the source property options
        var items = sourceProperties
            .Select(p => new SourceProperty(options.GetPropertyOptions(p)))
            .ToList();

        return items;
    }

    /// <summary>
    /// Creates a new instance of <see cref="SourceProperty"/>.
    /// </summary>
    /// <param name="options"></param>
    public SourceProperty(PropertyOptions options)
    {
        Options = options;
    }

    /// <summary>
    /// Options for the source property.
    /// </summary>
    public PropertyOptions Options { get; }

    /// <summary>
    /// The resolution of the property, if resolved.
    /// </summary>
    public ResolutionBase? Resolution { get; private set; }

    /// <summary>
    /// Checks if the property is available for resolution.
    /// </summary>
    public bool IsAvailable => Resolution is null;

    /// <summary>
    /// Sets the resolution of the property.
    /// </summary>
    /// <param name="resolution">The resolution.</param>
    public void ResolvedBy(ResolutionBase resolution)
    {
        Resolution = resolution;
    }

    /// <summary>
    /// Check if the property is mapped to a constructor.
    /// </summary>
    public bool IsMappedToConstructor => Options.ResolutionOptions?.Status == ResolutionStatus.MappedToConstructor;

    /// <summary>
    /// Check if the property is mapped to a constructor parameter.
    /// </summary>
    public bool IsMappedToConstructorParameter => Options.ResolutionOptions?.Status == ResolutionStatus.MappedToConstructorParameter;

    /// <summary>
    /// Check if the property is mapped to a method.
    /// </summary>
    public bool IsMappedToMethod => Options.ResolutionOptions?.Status == ResolutionStatus.MappedToMethod;

    /// <summary>
    /// Check if the property is mapped to a method parameter.
    /// </summary>
    public bool IsMappedToMethodParameter => Options.ResolutionOptions?.Status == ResolutionStatus.MappedToMethodParameter;

    /// <summary>
    /// Check if the property has inner properties mapped to the target.
    /// </summary>
    public bool IsMappedToTarget => Options.ResolutionOptions?.Status == ResolutionStatus.MappedToTarget;

    /// <summary>
    /// Check if the property is mapped to a property.
    /// </summary>
    public bool IsMappedToProperty => Options.ResolutionOptions?.Status == ResolutionStatus.MappedToProperty;

    /// <summary>
    /// Check if the property is not mapped manually.
    /// </summary>
    public bool IsNotMapped => Options.ResolutionOptions is null || Options.ResolutionOptions?.Status == ResolutionStatus.Undefined;
}
