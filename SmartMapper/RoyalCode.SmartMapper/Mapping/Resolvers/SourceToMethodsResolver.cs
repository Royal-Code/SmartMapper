using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Mapping.Discovery.SourceToMethods;
using RoyalCode.SmartMapper.Mapping.Resolutions;

namespace RoyalCode.SmartMapper.Mapping.Resolvers;

/// <summary>
/// Represents the context for the source to methods resolution.
/// </summary>
internal sealed class SourceToMethodsResolver
{
    public static SourceToMethodsResolver Create(AdapterResolver adapterResolver)
    {
        return new SourceToMethodsResolver(adapterResolver);
    }

    private SourceToMethodsResolver(AdapterResolver adapterResolver)
    {
        AdapterResolver = adapterResolver;
    }

    public AdapterResolver AdapterResolver { get; }

    public SourceToMethodsResolutions CreateResolution(MapperConfigurations configurations)
    {
        // event: resolution started. here the interceptor can be called in future versions

        var availableMethods = AdapterResolver.AvailableTargetMethods;
        
        // 1. get source to method options.
        if (!AdapterResolver.SourceOptions.TryGetSourceToMethodOptions(out var sourceToMethodOptions))
        {
            // 1.1 if none is found, try to resolve the method with the source type.

            var discoveryRequest = new SourceToMethodRequest(
                configurations,
                AdapterResolver.SourceOptions.SourceType,
                AdapterResolver.SourceItems,
                availableMethods.ListAvailableMethods());

            // 1.1.1 Using the name of the source type, try to discover the method by name.
            var discoveryResult = configurations.Discovery.SourceToMethod.Discover(discoveryRequest);

            // 1.2 if none is found, return an empty resolution.
            return discoveryResult.IsResolved
                ? new SourceToMethodsResolutions([discoveryResult.Resolution])
                : new SourceToMethodsResolutions([]);
        }

        // 2. for each source to method options, try to resolve the method.
        List<SourceToMethodResolution>? resolutions = null;
        foreach(var stmOption in sourceToMethodOptions)
        {
            var sourceToMethodContext = SourceToMethodResolver.Create(AdapterResolver.SourceItems, stmOption, availableMethods);
            var resolution = sourceToMethodContext.CreateResolution(configurations);

            if (resolution.Resolved)
            {
                resolutions ??= [];
                resolutions.Add(resolution);
            }
            else
            {
                // 2.1 if one fails, and it is not the discovered method, return a failure.
                return new SourceToMethodsResolutions(resolution.Failure);
            }
        }

        return new(resolutions ?? []);
    }

}
