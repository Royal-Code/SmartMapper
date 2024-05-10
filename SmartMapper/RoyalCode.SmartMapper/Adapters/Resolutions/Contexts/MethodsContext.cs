
using RoyalCode.SmartMapper.Adapters.Discovery.SourceToMethods;
using RoyalCode.SmartMapper.Adapters.Resolvers.Available;
using RoyalCode.SmartMapper.Core.Configurations;

namespace RoyalCode.SmartMapper.Adapters.Resolutions.Contexts;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

/// <summary>
/// Represents the context for the source to methods resolution.
/// </summary>
internal sealed class MethodsContext
{
    public static MethodsContext Create(AdapterContext adapterContext)
    {
        return new MethodsContext()
        {
            AdapterContext = adapterContext
        };
    }

    private MethodsContext() { }

    public AdapterContext AdapterContext { get; private init; }

    public MethodsResolutions CreateResolution(MapperConfigurations configurations)
    {
        // event: resolution started. here the interceptor can be called in future versions

        var availableMethods = AdapterContext.AvailableTargetMethods;
        
        // 1. get source to method options.
        if (!AdapterContext.SourceOptions.TryGetSourceToMethodOptions(out var sourceToMethodOptions))
        {
            // 1.1 if none is found, try to resolve the method with the source type.

            var discoveryRequest = new SourceToMethodRequest(
                configurations,
                AdapterContext.SourceOptions.SourceType,
                AdapterContext.SourceItems,
                availableMethods.ListAvailableMethods());

            // 1.1.1 Using the name of the source type, try to discover the method by name.
            var discoveryResult = configurations.Discovery.SourceToMethod.Discover(discoveryRequest);

            // 1.2 if none is found, return an empty resolution.
            return discoveryResult.IsResolved
                ? new MethodsResolutions([discoveryResult.Resolution])
                : new MethodsResolutions([]);
        }

        // 2. for each source to method options, try to resolve the method.
        List<SourceToMethodResolution>? resolutions = null;
        foreach(var stmOption in sourceToMethodOptions)
        {
            var sourceToMethodContext = SourceToMethodContext.Create(AdapterContext.SourceItems, stmOption, availableMethods);
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
