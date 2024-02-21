
namespace RoyalCode.SmartMapper.Adapters.Resolutions.Contexts;

internal class MethodContext
{
    public static MethodContext Create(AdapterContext adapterContext)
    {
        return new MethodContext()
        {
            AdapterContext = adapterContext,
            
        };
    }

    private MethodContext() { }

    public AdapterContext AdapterContext { get; private init; }

    
}
