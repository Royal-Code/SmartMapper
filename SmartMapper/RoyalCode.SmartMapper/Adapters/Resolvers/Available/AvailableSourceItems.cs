using RoyalCode.SmartMapper.Adapters.Options.Resolutions;
using RoyalCode.SmartMapper.Adapters.Resolutions;
using RoyalCode.SmartMapper.Mapping.Options;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace RoyalCode.SmartMapper.Adapters.Resolvers.Available;

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
    ///     as parameters of a constructor.
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
            else if (sourceItem.IsMappedToConstructorParameter)
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

    /// <summary>
    /// Create a new instance of <see cref="AvailableSourceItems"/> with the source items available for the mapping
    /// as parameters of a method.
    /// </summary>
    /// <param name="methodInfo">The method info to map the source items.</param>
    /// <param name="sourceItems">The source items of a source type.</param>
    /// <param name="parent">Parent source property, if available.</param>
    /// <returns>
    ///     A new instance of <see cref="AvailableSourceItems"/> with the source items available for the mapping
    ///     as parameters of a method.
    /// </returns>
    public static AvailableSourceItems CreateAvailableSourceItemsForSourceToMethod(
        MethodInfo methodInfo,
        IEnumerable<SourceItem> sourceItems, AvailableSourceProperty? parent = null)
    {
        AvailableSourceItems availableSourceItems = new();

        foreach (var sourceItem in sourceItems.Where(item => item.IsAvailable))
        {
            var availableSourceProperty = new AvailableSourceProperty(sourceItem, parent);

            switch (sourceItem)
            {
                case
                    {
                        IsMappedToMethod: true,
                        Options.ResolutionOptions: ToMethodResolutionOptions toMethodResolutionOptions
                    }
                    when toMethodResolutionOptions.CanAcceptMethod(methodInfo):
                    
                    availableSourceItems.requiredSourceProperties.Add(availableSourceProperty);
                    availableSourceItems.AddInner(
                        CreateInnerAvailableSourceItemsForMethods(
                            methodInfo,
                            sourceItem,
                            availableSourceProperty));
                    break;

                case
                    {
                        IsMappedToMethodParameter: true,
                        Options.ResolutionOptions: ToMethodParameterResolutionOptions resolutionOptions
                    }
                    when resolutionOptions.CanAcceptMethod(methodInfo):
                    
                    availableSourceItems.availableSourceProperties.Add(availableSourceProperty);
                    availableSourceItems.requiredSourceProperties.Add(availableSourceProperty);
                    break;

                case
                    {
                        IsNotMapped: true
                    }:
                    
                    availableSourceItems.availableSourceProperties.Add(availableSourceProperty);
                    break;

                case
                    {
                        IsMappedToTarget: true
                    }:
                    
                    availableSourceItems.AddInner(
                        CreateInnerAvailableSourceItemsForMethods(
                            methodInfo,
                            sourceItem,
                            availableSourceProperty));
                    break;
            }
        }

        return availableSourceItems;
    }

    /// <summary>
    /// <para>
    ///     Create a new instance of <see cref="AvailableSourceItems"/> with the source items available for the mapping
    ///     as parameters of a method based on the to method parameters collection.
    /// </para>
    /// <para>
    ///     When on of the to method parameters is missing, it will return null.
    /// </para>
    /// </summary>
    /// <param name="toMethodParameters">A collection of to method parameters.</param>
    /// <param name="sourceItems">The source items of a source type.</param>
    /// <param name="parent">Parent source property, if available.</param>
    /// <returns>
    ///     A new instance of <see cref="AvailableSourceItems"/> with the source items available for the mapping
    ///     as parameters of a method in the sequence of the to-method parameters,
    ///     or null if one of the to-method parameters is missing.
    /// </returns>
    public static AvailableSourceItems? CreateAvailableSourceItemsForSourceToMethod(
        IReadOnlyCollection<MethodParameterOptions> toMethodParameters,
        IReadOnlyCollection<SourceItem> sourceItems, AvailableSourceProperty? parent = null)
    {
        AvailableSourceItems availableSourceItems = new();
        
        foreach (var parameter in toMethodParameters)
        {
            var sourceItem = sourceItems.Where(item => item.IsAvailable)
                .FirstOrDefault(item => item.Options.Property == parameter.SourceProperty);

            if (sourceItem?.Options.ResolutionOptions is not ToMethodParameterResolutionOptions toMethodParameterResolutionOptions)
                return null;
            
            if (toMethodParameterResolutionOptions.ToMethodParameterOptions != parameter)
                return null;
            
            var availableSourceProperty = new AvailableSourceProperty(sourceItem, parent);
            
            availableSourceItems.availableSourceProperties.Add(availableSourceProperty);
            availableSourceItems.requiredSourceProperties.Add(availableSourceProperty);
        }
        
        return availableSourceItems;
    }

    private static AvailableSourceItems CreateInnerAvailableSourceItemsForMethods(
        MethodInfo methodInfo, SourceItem sourceItem, AvailableSourceProperty parent)
    {
        var propertyOptions = sourceItem.Options;
        var innerSourceOptions = propertyOptions.GetInnerPropertiesSourceOptions()
            ?? new SourceOptions(propertyOptions.Property.PropertyType);
        var innerItems = SourceItem.Create(propertyOptions.Property.PropertyType, innerSourceOptions);
        var innerAvailableProperties = CreateAvailableSourceItemsForSourceToMethod(methodInfo, innerItems, parent);

        return innerAvailableProperties;
    }

    public static AvailableSourceItems CreateAvailableSourceItemsForMapProperties(
        IEnumerable<SourceItem> sourceItems, AvailableSourceProperty? parent = null)
    {
        AvailableSourceItems availableSourceItems = new();

        foreach (var sourceItem in sourceItems.Where(item => item.IsAvailable))
        {
            // all available source properties are required for map as properties
            // because all properties must be resolved
            var availableSourceProperty = new AvailableSourceProperty(sourceItem, parent);
            availableSourceItems.requiredSourceProperties.Add(availableSourceProperty);
            
            if (sourceItem.IsNotMapped || sourceItem.IsMappedToProperty)
            {
                availableSourceItems.availableSourceProperties.Add(availableSourceProperty);
            }
            else if (sourceItem.IsMappedToTarget)
            {
                var innerAvailableItems = CreateAvailableSourceItemsForMapProperties(sourceItem, availableSourceProperty);
                availableSourceItems.AddInner(innerAvailableItems);
            }
        }
        
        return availableSourceItems;
    }
    
    private static AvailableSourceItems CreateAvailableSourceItemsForMapProperties(
        SourceItem sourceItem, AvailableSourceProperty parent)
    {
        var propertyOptions = sourceItem.Options;
        var innerSourceOptions = propertyOptions.GetInnerPropertiesSourceOptions()
                                 ?? new SourceOptions(propertyOptions.Property.PropertyType);
        var innerItems = SourceItem.Create(propertyOptions.Property.PropertyType, innerSourceOptions);
        var innerAvailableProperties = CreateAvailableSourceItemsForMapProperties(innerItems, parent);

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

    private void AddInner(AvailableSourceItems innerAvailableSourceItems)
    {
        availableSourceProperties.AddRange(innerAvailableSourceItems.AvailableSourceProperties);
        requiredSourceProperties.AddRange(innerAvailableSourceItems.RequiredSourceProperties);
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
