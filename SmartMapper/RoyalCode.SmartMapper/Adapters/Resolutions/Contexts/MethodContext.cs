
using RoyalCode.SmartMapper.Adapters.Resolvers.Avaliables;
using RoyalCode.SmartMapper.Core.Configurations;

namespace RoyalCode.SmartMapper.Adapters.Resolutions.Contexts;

internal sealed class MethodContext
{
    public static MethodContext Create(AdapterContext adapterContext)
    {
        return new MethodContext()
        {
            AdapterContext = adapterContext,
            AvailableMethods = new AvailableTargetMethods(adapterContext.Options.TargetType)
        };
    }

    private MethodContext() { }

    public AdapterContext AdapterContext { get; private init; }

    public AvailableTargetMethods AvailableMethods { get; private init; }

    public MethodsResolutions CreateResolution(MapperConfigurations configurations)
    {
        // event: resolution started. here the interceptor can be called in future versions
        
        // 1. get source to method options.
        // 1.1 if none is found, try to resolve the method with the source type.
        // 1.1.1 Using the name of the source type, try to discover the method by name.
        // 1.2 if none is found, return a empty resolution.
        
        // 2. for each source to method options, try to resolve the method (see 3).
        // 2.1 if one fails, and it is not the discovered method, return a failure.
        // 2.2 if the discovered method fails, return a empty resolution.
        
        // 3. resolve the method, resolving the parameters and the method itself.
        // 3.1 if all parameters are resolved, generate the resolution.
        // 3.2 if one parameter fails, generate a failure.
        
        throw new NotImplementedException();
    }
}
