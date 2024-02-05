using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;
using RoyalCode.SmartMapper.Adapters.Resolutions;
using RoyalCode.SmartMapper.Core.Resolutions;
using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartMapper.Adapters.Resolvers.Avaliables;

/// <summary>
/// 
/// </summary>
public class AvailableSourceItems
{
    public static AvailableSourceItems CreateAvailableSourceItemsForConstructors(
        IEnumerable<SourceItem> sourceItems, AvailableSourceProperty? parent = null)
    {
        AvailableSourceItems availableSourceItems = new();

        foreach (var sourceItem in sourceItems.Where(item => item.IsAvailable))
        {
            var availableSourceProperty = new AvailableSourceProperty(sourceItem, parent);

            if (sourceItem.IsMappedToConstructor)
            {
                availableSourceItems.groupedSourceProperties.Add(availableSourceProperty);
                availableSourceItems.requiredSourceProperties.Add(availableSourceProperty);

                var innerAvailableItems = CreateInnerAvailableSourceItemsForConstructors(sourceItem, availableSourceProperty);
                availableSourceItems.AddInner(innerAvailableItems);
            }
            else if (sourceItem.IsMappedToTarget)
            {
                availableSourceItems.groupedSourceProperties.Add(availableSourceProperty);

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
    private readonly List<AvailableSourceProperty> groupedSourceProperties = [];

    /// <summary>
    /// All the available source properties, including the inner properties.
    /// </summary>
    public IEnumerable<AvailableSourceProperty> AvailableSourceProperties => availableSourceProperties;

    /// <summary>
    /// The required source properties for the mapping. All the required properties must be resolved.
    /// </summary>
    public IEnumerable<AvailableSourceProperty> RequiredSourceProperties => requiredSourceProperties;

    /// <summary>
    /// Properties that have inner properties.
    /// </summary>
    public IEnumerable<AvailableSourceProperty> GroupedSourceProperties => groupedSourceProperties;

    private void AddInner(AvailableSourceItems innerAvaliableItems)
    {
        availableSourceProperties.AddRange(innerAvaliableItems.AvailableSourceProperties);
        requiredSourceProperties.AddRange(innerAvaliableItems.RequiredSourceProperties);
    }

    internal bool TryGetAvailableSourcePropertyToParameter(string parameterName,
        [NotNullWhen(true)] out AvailableSourceProperty? availableSourceProperty,
        [NotNullWhen(true)] out ParameterResolutionOptionsBase? toParameterResolutionOptions)
    {
        foreach(var p in AvailableSourceProperties)
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

/// <summary>
/// Property that is available for mapping.
/// </summary>
public class AvailableSourceProperty
{
    /// <summary>
    /// Creates new instance of <see cref="AvailableSourceProperty"/>.
    /// </summary>
    /// <param name="sourceItem">The source item.</param>
    /// <param name="parentySourceProperty">The parent source property, if available.</param>
    public AvailableSourceProperty(SourceItem sourceItem, AvailableSourceProperty? parentySourceProperty = null)
    {
        SourceItem = sourceItem;
        Parent = parentySourceProperty;

        parentySourceProperty?.AddInnerProperty(this);
    }

    /// <summary>
    /// The parent source property, if available.
    /// </summary>
    public AvailableSourceProperty? Parent { get; }

    /// <summary>
    /// if the property is resolved.
    /// </summary>
    public bool Resolved { get; private set; }

    /// <summary>
    /// The source item.
    /// </summary>
    public SourceItem SourceItem { get; }

    /// <summary>
    /// The property options.
    /// </summary>
    public PropertyOptions Options => SourceItem.Options;

    /// <summary>
    /// The resolution options, if available.
    /// </summary>
    public ResolutionOptionsBase? ResolutionOptions => SourceItem.Options.ResolutionOptions;

    /// <summary>
    /// The inner properties, if available.
    /// </summary>
    public AvaliableInnerSourceProperties? InnerProperties { get; private set; }

    /// <summary>
    /// The resolution of the property, if resolved.
    /// </summary>
    public ResolutionBase? Resolution { get; private set; }

    /// <summary>
    /// Assign the resolution of the property.
    /// </summary>
    /// <param name="resolution"></param>
    public void ResolvedBy(ParameterResolution resolution)
    {
        Resolution = resolution;
        Resolved = true;
    }

    internal object GetPropertyPathString()
    {
        var prefix = Parent?.GetPropertyPathString();
        return prefix is null ? Options.Property.Name : $"{prefix}.{Options.Property.Name}";
    }

    private void AddInnerProperty(AvailableSourceProperty child)
    {
        InnerProperties ??= new AvaliableInnerSourceProperties(this);
        InnerProperties.Add(child);
    }
}