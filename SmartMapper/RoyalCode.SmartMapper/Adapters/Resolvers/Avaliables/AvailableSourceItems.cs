using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;
using RoyalCode.SmartMapper.Adapters.Resolutions;
using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartMapper.Adapters.Resolvers.Avaliables;

/// <summary>
/// Available source items for the mapping.
/// </summary>
public sealed class AvailableSourceItems
{
    /// <summary>
    /// Create a new instance of <see cref="AvailableSourceItems"/> with the source items available for the mapping
    /// as parameters of a constructor.
    /// </summary>
    /// <param name="sourceItems">The source items of a source type.</param>
    /// <param name="parent">Parent source property, if available.</param>
    /// <returns>
    ///     A new instance of <see cref="AvailableSourceItems"/> with the source items available for the mapping
    /// </returns>
    public static AvailableSourceItems CreateAvailableSourceItemsForConstructors(
        IEnumerable<SourceItem> sourceItems, AvailableSourceProperty? parent = null)
    {
        AvailableSourceItems availableSourceItems = new();

        foreach (var sourceItem in sourceItems.Where(item => item.IsAvailable))
        {
            var availableSourceProperty = new AvailableSourceProperty(sourceItem, parent);

            if (sourceItem.IsMappedToConstructor)
            {
                availableSourceItems.requiredSourceProperties.Add(availableSourceProperty);

                var innerAvailableItems = CreateInnerAvailableSourceItemsForConstructors(sourceItem, availableSourceProperty);
                availableSourceItems.AddInner(innerAvailableItems);
            }
            else if (sourceItem.IsMappedToTarget)
            {
                var innerAvailableItems = CreateInnerAvailableSourceItemsForConstructors(sourceItem, availableSourceProperty);
                availableSourceItems.AddInner(innerAvailableItems);
            }
            else if (sourceItem.IsMappedToMethodParameter)
            {
                availableSourceItems.availableSourceProperties.Add(availableSourceProperty);
                availableSourceItems.requiredSourceProperties.Add(availableSourceProperty);
            }
            else if (sourceItem.IsNotMapped)
            {
                availableSourceItems.availableSourceProperties.Add(new(sourceItem, parent));
            }
        }

        return availableSourceItems;
    }

    private static AvailableSourceItems CreateInnerAvailableSourceItemsForConstructors(
        SourceItem sourceItem, AvailableSourceProperty parent)
    {
        var propertyOptions = sourceItem.Options;
        var innerSourceOptions = propertyOptions.GetInnerPropertiesSourceOptions()
            ?? new SourceOptions(propertyOptions.Property.PropertyType);
        var innerItems = SourceItem.Create(propertyOptions.Property.PropertyType, innerSourceOptions);
        var innerAvailableProperties = CreateAvailableSourceItemsForConstructors(innerItems, parent);

        return innerAvailableProperties;
    }

    private readonly List<AvailableSourceProperty> availableSourceProperties = [];
    private readonly List<AvailableSourceProperty> requiredSourceProperties = [];

    /// <summary>
    /// All the available source properties, including the inner properties.
    /// </summary>
    public IEnumerable<AvailableSourceProperty> AvailableSourceProperties => availableSourceProperties;

    /// <summary>
    /// The required source properties for the mapping. All the required properties must be resolved.
    /// </summary>
    public IEnumerable<AvailableSourceProperty> RequiredSourceProperties => requiredSourceProperties;

    /// <summary>
    /// Check if all the required source properties are resolved.
    /// </summary>
    public bool AllRequiredItemsResolved => RequiredSourceProperties.All(p => p.Resolved);

    private void AddInner(AvailableSourceItems innerAvaliableItems)
    {
        availableSourceProperties.AddRange(innerAvaliableItems.AvailableSourceProperties);
        requiredSourceProperties.AddRange(innerAvaliableItems.RequiredSourceProperties);
    }

    internal bool TryGetAvailableSourcePropertyToParameter(string parameterName,
        [NotNullWhen(true)] out AvailableSourceProperty? availableSourceProperty,
        [NotNullWhen(true)] out ParameterResolutionOptionsBase? toParameterResolutionOptions)
    {
        foreach(var p in AvailableSourceProperties.Where(p => !p.Resolved))
        {
            availableSourceProperty = p;
            if (p.SourceItem.Options.ResolutionOptions is ParameterResolutionOptionsBase parameterResolutionOptions)
            {
                toParameterResolutionOptions = parameterResolutionOptions;

                var toParameterOptions = parameterResolutionOptions.ToParameterOptions;
                if (toParameterOptions.Parameter is not null)
                    return toParameterOptions.Parameter.Name == parameterName;
                if (toParameterOptions.ParameterName is not null)
                    return toParameterOptions.ParameterName == parameterName;
            }
        }

        availableSourceProperty = null;
        toParameterResolutionOptions = null;
        return false;
    }
}
