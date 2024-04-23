using RoyalCode.SmartMapper.Adapters.Resolvers.Avaliables;
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
        throw new NotImplementedException();
    }
}