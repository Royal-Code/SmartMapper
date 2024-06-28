using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Options;
using RoyalCode.SmartMapper.Mapping.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;
using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

namespace RoyalCode.SmartMapper.Mapping.Resolvers;

/// <summary>
/// Represents the context for the source to method resolution process.
/// </summary>
internal sealed class SourceToMethodResolver
{
    public static SourceToMethodResolver Create(
        IReadOnlyCollection<SourceProperty> sourceItems,
        SourceToMethodOptions options,
        AvailableTargetMethods availableTargetMethods)
    {
        return new SourceToMethodResolver(sourceItems, options, availableTargetMethods);
    }

    private SourceToMethodResolver(
        IReadOnlyCollection<SourceProperty> sourceItems,
        SourceToMethodOptions options,
        AvailableTargetMethods availableTargetMethods)
    {
        SourceItems = sourceItems;
        Options = options;
        AvailableMethods = availableTargetMethods;
    }

    public IReadOnlyCollection<SourceProperty> SourceItems { get; }
    
    public SourceToMethodOptions Options { get; }

    public AvailableTargetMethods AvailableMethods { get; }

    public SourceToMethodResolution CreateResolution(MapperConfigurations configurations)
    {
        // event: resolution started. here the interceptor can be called in future versions

        // 1. Filter the available methods using the options.
        
        var availableMethods = AvailableMethods.ListAvailableMethods();

        if (Options.MethodOptions.Method is not null)
        {
            availableMethods = availableMethods.Where(a => a.Method.MethodInfo == Options.MethodOptions.Method);
        }
        else if (Options.MethodOptions.MethodName is not null)
        {
            availableMethods = availableMethods.Where(a => a.Method.MethodInfo.Name == Options.MethodOptions.MethodName);
        }

        IReadOnlyCollection<MethodParameterOptions>? sourceParameters = null;
        if (Options.IsSelectedParameters(out var parametersOptions))
        {
            sourceParameters = parametersOptions.GetAllParameterSequence();
            availableMethods = availableMethods.Where(a => a.Method.Parameters.Count == parametersOptions.CountParameterSequence());
        }

        // 2.1 for each available method, try to resolve the method.
        foreach (var availableMethod in availableMethods)
        {
            // 3.1 get the available source items for the method.
            var availableSourceItems = sourceParameters is null
                ? AvailableSourceItems.CreateAvailableSourceItemsForSourceToMethod(availableMethod.Method.MethodInfo, SourceItems)
                : AvailableSourceItems.CreateAvailableSourceItemsForSourceToMethod(sourceParameters, SourceItems);
            
            // 3.2 if not satisfied, continue to the next method.
            if (availableSourceItems is null)
                continue;
            
            // 3.3 get target parameters.
            var availableParameters = availableMethod.CreateAvailableParameters();
            
            // 3.4 - create the parameter context for each target parameter.
            var parameterResolvers = availableParameters.Select(p => ParameterResolver.Create(p, availableSourceItems)).ToList();
            
            // 3.5 - resolve each parameter context.
            var parameterResolutions = parameterResolvers.Select(p => p.CreateResolution(configurations)).ToList();
            
            // 3.6 - check if all parameters are resolved.
            if (!parameterResolutions.TrueForAll(static p => p.Resolved))
                continue;
            
            // 3.7 - check if all required source items are resolved.
            if (!availableSourceItems.AllRequiredItemsResolved)
                continue;
            
            // 3.8 - create the resolution.
            var resolution = new SourceToMethodResolution(availableMethod.Method, parameterResolutions);
            resolution.Completed();
            return resolution;
        }

        // 2.2 if none available method satisfies the source to method options, return a failure. 
        var failure = new ResolutionFailure(
            $"The source to method resolution failed, no method was found that satisfies the source to method options {Options}.");

        return new SourceToMethodResolution(failure);
    }
}
