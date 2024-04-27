using RoyalCode.SmartMapper.Adapters.Resolvers.Available;
using RoyalCode.SmartMapper.Core.Configurations;

namespace RoyalCode.SmartMapper.Adapters.Resolutions.Contexts;

internal class PropertiesContext
{
    public static PropertiesContext Create(AdapterContext adapterContext)
    {
        return new PropertiesContext()
        {
            AdapterContext = adapterContext,
            AvailableProperties = new AvailableTargetProperties(adapterContext.Options.TargetType),
            AvailableMethods = new AvailableTargetMethods(adapterContext.Options.TargetType)
        };
    }
    
    private PropertiesContext() { }
    
    public AdapterContext AdapterContext { get; private init; }
    
    public AvailableTargetProperties AvailableProperties { get; private init; }
    
    public AvailableTargetMethods AvailableMethods { get; private init; }

    public PropertiesResolution CreateResolution(MapperConfigurations configurations)
    {
        // event: resolution started. here the interceptor can be called in future versions
        
        // 1. get source available properties to map.
        var availableProperties = AvailableSourceItems
            .CreateAvailableSourceItemsForMapProperties(AdapterContext.SourceItems);
        
        // 2. for each property, try to resolve the property.
        foreach (var property in availableProperties.AvailableSourceProperties)
        {
            
        }
        
        // 2.1 property can have a resolution option.
        // 2.1.1 check the resolution options e try to resolve the property.
        // 2.1.2 when property does not have a resolution option, try to resolve the property by name.
        
        // 2.2 try to resolve the property by name.
        // 2.2.1 try map the property by name to an available target method by name.
        // 2.2.2 try map the property by name to an available target property.
        
        throw new System.NotImplementedException();
    }
}