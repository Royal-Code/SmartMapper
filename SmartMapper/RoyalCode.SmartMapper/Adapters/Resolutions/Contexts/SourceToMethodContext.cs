
using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Resolvers.Available;
using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Resolutions.Contexts;

/// <summary>
/// Represents the context for the source to method resolution process.
/// </summary>
internal sealed class SourceToMethodContext
{
    public static SourceToMethodContext Create(
        IEnumerable<SourceItem> sourceItems,
        SourceToMethodOptions options,
        AvailableTargetMethods availableTargetMethods)
    {
        return new SourceToMethodContext()
        {
            SourceItems = sourceItems,
            Options = options,
            AvailableMethods = availableTargetMethods
        };
    }

    public IEnumerable<SourceItem> SourceItems { get; private init; }
    
    public SourceToMethodOptions Options { get; private init; }

    public AvailableTargetMethods AvailableMethods { get; private init; }

    public SourceToMethodResolution CreateResolution(MapperConfigurations configurations)
    {
        // event: resolution started. here the interceptor can be called in future versions

        // 1. Filter the available methods using the options.
        
        var availableMethods = AvailableMethods.ListAvailableMethods();

        if (Options.MethodOptions.Method is not null)
        {
            availableMethods = availableMethods.Where(a => a.Info == Options.MethodOptions.Method);
        }
        else if (Options.MethodOptions.MethodName is not null)
        {
            availableMethods = availableMethods.Where(a => a.Info.Name == Options.MethodOptions.MethodName);
        }

        IEnumerable<ToMethodParameterOptions>? sourceParameters = null;
        if (Options.Strategy == SourceToMethodStrategy.SelectedParameters)
        {
            sourceParameters = Options.GetAllParameterSequence();
            availableMethods = availableMethods.Where(a => a.Info.GetParameters().Length == Options.CountParameterSequence());
        }

        // 2.1 for each available method, try to resolve the method.
        foreach (var availableMethod in availableMethods)
        {
            // TODO: Reavaliar AvailableSourceItems, tem opções de resolução para source to method e property to method. É necessário separar.
            
            // 3.1 get the available source items for the method.
            var availableSourceItems = sourceParameters is null
                ? AvailableSourceItems.CreateAvailableSourceItemsForMethods(availableMethod.Info, SourceItems)
                : AvailableSourceItems.CreateAvailableSourceItemsForMethods(sourceParameters, SourceItems);
            
            // 3.2 if not satisfied, continue to the next method.
            if (availableSourceItems is null)
                continue;
            
            // 3.3 get target parameters.
            var targetParameters = availableMethod.CreateTargetParameters();
            
            // 3.4 - create the parameter context for each target parameter.
            var parametersContext = targetParameters.Select(p => ParameterContext.Create(p, availableSourceItems)).ToList();
            
            // 3.5 - resolve each parameter context.
            var parametersResolutions = parametersContext.Select(p => p.CreateResolution(configurations)).ToList();
            
            // 3.6 - check if all parameters are resolved.
            if (!parametersResolutions.TrueForAll(static p => p.Resolved))
                continue;
            
            // 3.7 - check if all required source items are resolved.
            if (!availableSourceItems.AllRequiredItemsResolved)
                continue;
            
            // 3.8 - create the resolution.
            return new SourceToMethodResolution(availableMethod.Info, parametersResolutions);
        }

        // 2.2 if none available method satisfies the source to method options, return a failure. 
        var failure = new ResolutionFailure(
            $"The source to method resolution failed, no method was found that satisfies the source to method options {Options}.");

        return new SourceToMethodResolution(failure);
    }
}
