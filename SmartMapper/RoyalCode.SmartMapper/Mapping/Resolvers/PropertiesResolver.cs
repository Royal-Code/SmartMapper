using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

namespace RoyalCode.SmartMapper.Mapping.Resolvers;

internal class PropertiesResolver
{
    public static PropertiesResolver Create(AdapterResolver adapterResolver)
    {
        return new PropertiesResolver(adapterResolver);
    }

    private PropertiesResolver(AdapterResolver adapterResolver)
    {
        AdapterResolver = adapterResolver;
    }
    
    public AdapterResolver AdapterResolver { get; }

    public PropertiesResolution CreateResolution(MapperConfigurations configurations)
    {
        // event: resolution started. here the interceptor can be called in future versions
        
        // 1. get source available properties to map.
        var availableProperties = AvailableSourceItems
            .CreateAvailableSourceItemsForMapProperties(AdapterResolver.SourceProperties);

        // 2. for each property, create a context and try to resolve the property.
        var resolutions = availableProperties.AvailableSourceProperties
            .Select(availableSourceProperty => PropertyResolver.Create(AdapterResolver, availableSourceProperty))
            .Select(propertyResolution => propertyResolution.CreateResolution(configurations))
            .ToList();

        var errors = resolutions.Where(r => !r.Resolved).Select(r => r.Failure!).ToList();
        
        // when there are no errors, return a successful resolution.
        if (errors.Count is 0) 
            return new PropertiesResolution(resolutions);
        
        // when has errors, return a failure resolution.
        var failure = new ResolutionFailure();
        failure.AddMessage($"Failed to resolve properties from {AdapterResolver.Options.SourceType} to {AdapterResolver.Options.TargetType}.");
        failure.AddMessages(errors.SelectMany(f => f.Messages));
        return new PropertiesResolution(failure);

    }
}