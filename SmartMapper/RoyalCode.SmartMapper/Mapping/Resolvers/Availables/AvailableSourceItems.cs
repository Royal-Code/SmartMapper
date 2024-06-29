using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using RoyalCode.SmartMapper.Mapping.Options;
using RoyalCode.SmartMapper.Mapping.Options.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

namespace RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

/// <summary>
/// Available source items for the mapping.
/// </summary>
public sealed partial class AvailableSourceItems
{
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
            if (p.SourceProperty.Options.ResolutionOptions is ParameterResolutionOptionsBase parameterResolutionOptions)
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
