using RoyalCode.SmartMapper.Adapters.Resolvers.Available;
using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Resolutions.Contexts;

internal class PropertiesContext
{
    public static PropertiesContext Create(AdapterContext adapterContext)
    {
        return new PropertiesContext()
        {
            AdapterContext = adapterContext,
            AvailableProperties = new AvailableTargetProperties(adapterContext.Options.TargetType),
        };
    }
    
    private PropertiesContext() { }
    
    public AdapterContext AdapterContext { get; private init; }
    
    public AvailableTargetProperties AvailableProperties { get; private init; }

    public PropertiesResolution CreateResolution(MapperConfigurations configurations)
    {
        // event: resolution started. here the interceptor can be called in future versions
        
        // 1. get source available properties to map.
        var availableProperties = AvailableSourceItems
            .CreateAvailableSourceItemsForMapProperties(AdapterContext.SourceItems);

        // 2. for each property, create a context and try to resolve the property.
        var resolutions = availableProperties.AvailableSourceProperties
            .Select(property => PropertyContext.Create(AdapterContext, AvailableProperties, property))
            .Select(propertyContext => propertyContext.CreateResolution(configurations))
            .ToList();

        var errors = resolutions.Where(r => !r.Resolved).Select(r => r.Failure).ToList();
        
        // when there are no errors, return a successful resolution.
        if (errors.Count is 0) 
            return new PropertiesResolution(resolutions);
        
        // when has errors, return a failure resolution.
        var failure = new ResolutionFailure();
        failure.AddMessage($"Failed to resolve properties from {AdapterContext.Options.SourceType} to {AdapterContext.Options.TargetType}.");
        failure.AddMessages(errors.SelectMany(f => f.Messages));
        return new PropertiesResolution(failure);

    }
}