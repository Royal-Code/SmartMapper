using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

namespace RoyalCode.SmartMapper.Mapping.Resolvers;

/// <summary>
/// Represents the context for the constructor resolution process.
/// </summary>
internal sealed class ConstructorResolver
{
    public static ConstructorResolver Create(AdapterResolver adapterResolver, AvailableConstructor availableConstructor)
    {
        return new ConstructorResolver(adapterResolver, availableConstructor);
    }

    private ConstructorResolver(AdapterResolver adapterResolver, AvailableConstructor availableConstructor)
    {
        AdapterResolver = adapterResolver;
        AvailableConstructor = availableConstructor;
    }
    
    public AdapterResolver AdapterResolver { get; }

    public AvailableConstructor AvailableConstructor { get; }

    public ConstructorResolution CreateResolution(MapperConfigurations configurations)
    {
        // 1 - get the target parameters and the available source items for constructors.
        var targetParameters = AvailableConstructor.CreateTargetParameters();
        var availableSourceItems = AvailableSourceItems.CreateAvailableSourceItemsForConstructors(AdapterResolver.SourceItems);

        // 2 - create the parameter context for each target parameter.
        var parametersContext = targetParameters.Select(p => ParameterResolver.Create(p, availableSourceItems)).ToList();

        // 3 - resolve each parameter context.
        var parametersResolutions = parametersContext.Select(p => p.CreateResolution(configurations)).ToList();

        // 4 - check if all parameters are resolved.
        if (!parametersResolutions.TrueForAll(static p => p.Resolved))
        {
            // when not resolved, return a not resolved resolution.
            var failure = new ResolutionFailure(
                $"The constructor for type '{AdapterResolver.Options.TargetType.Name}' can't be resolved, " +
                $"some parameters don't have a resolution.");

            foreach (var p in parametersResolutions)
            {
                if (!p.Resolved)
                    failure.AddMessages(p.Failure.Messages);
            }

            return new ConstructorResolution(AvailableConstructor.Info, failure);
        }

        // 5 - check if all required source items are resolved.
        if (!availableSourceItems.AllRequiredItemsResolved)
        {
            // when not resolved, return a not resolved resolution.
            var failure = new ResolutionFailure(
                $"The constructor for type '{AdapterResolver.Options.TargetType.Name}' was resolved, " +
                $"but some properties mapped to the constructor are not resolved.");

            foreach (var required in availableSourceItems.RequiredSourceProperties)
            {
                if (!required.Resolved)
                    failure.AddMessage(required.GetFailureMessage());
            }

            return new ConstructorResolution(AvailableConstructor.Info, failure);
        }

        // 6 - create the resolution.
        return new ConstructorResolution(AvailableConstructor.Info, parametersResolutions);
    }
}
