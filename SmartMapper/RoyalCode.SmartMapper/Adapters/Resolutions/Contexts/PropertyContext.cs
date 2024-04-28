using RoyalCode.SmartMapper.Adapters.Resolvers.Available;
using RoyalCode.SmartMapper.Core.Configurations;

namespace RoyalCode.SmartMapper.Adapters.Resolutions.Contexts;

internal class PropertyContext
{
    public static PropertyContext Create(
        AdapterContext adapterContext,
        AvailableTargetProperties availableProperties,
        AvailableSourceProperty property)
    {
        return new PropertyContext()
        {
            AdapterContext = adapterContext,
            AvailableProperties = availableProperties,
            Property = property
        };
    }
    
    private PropertyContext() { }
    
    public AdapterContext AdapterContext { get; private init; }
    
    public AvailableTargetProperties AvailableProperties { get; private init; }
    
    public AvailableSourceProperty Property { get; private init; }

    public PropertyResolution CreateResolution(MapperConfigurations configurations)
    {
        // event: resolution started. here the interceptor can be called in future versions
        
        // 2.1 property can have a resolution option.
        // 2.1.1 check the resolution options e try to resolve the property.
        // 2.1.2 when property does not have a resolution option, try to resolve the property by name.
        
        // 2.2 try to resolve the property by name.
        // 2.2.1 try map the property by name to an available target method by name.
        // 2.2.2 try map the property by name to an available target property.
        
        throw new NotImplementedException();
    }
}