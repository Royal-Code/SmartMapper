
using RoyalCode.SmartMapper.Adapters.Discovery.SourceToMethods;
using RoyalCode.SmartMapper.Adapters.Options;
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
        if (!AdapterContext.SourceOptions.TryGetSourceToMethodOptions(out var sourceToMethodOptions))
        {
            // 1.1 if none is found, try to resolve the method with the source type.

            var discoveryRequest = new SourceToMethodRequest(
                configurations,
                AdapterContext.SourceOptions.SourceType,
                AdapterContext.SourceItems,
                AvailableMethods.ListAvailableMethods());

            // 1.1.1 Using the name of the source type, try to discover the method by name.
            var discoveryResult = configurations.Discovery.SourceToMethod.Discover(discoveryRequest);

            // 1.2 if none is found, return a empty resolution.
            return discoveryResult.IsResolved
                ? new MethodsResolutions([discoveryResult.Resolution])
                : new MethodsResolutions([]);
        }

        // 2. for each source to method options, try to resolve the method.
        List<SourceToMethodResolution>? resolutions = null;
        foreach(var stmOption in sourceToMethodOptions)
        {
            var sourceToMethodContext = SourceToMethodContext.Create(stmOption, AvailableMethods);
            var resolution = sourceToMethodContext.CreateResolution(configurations);

            if (resolution.Resolved)
            {
                resolutions ??= [];
                resolutions.Add(resolution);
            }
            else
            {
                // 2.1 if one fails, and it is not the discovered method, return a failure.
                return new MethodsResolutions(resolution.Failure);
            }
        }

        return new(resolutions ?? []);
    }

}
