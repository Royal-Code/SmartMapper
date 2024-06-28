using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;
using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

namespace RoyalCode.SmartMapper.Mapping.Resolvers;

/// <summary>
/// Represents the context for the constructor resolution process.
/// </summary>
internal sealed class ConstructorResolver
{
    public static ConstructorResolver Create(AdapterResolver adapterResolver, TargetConstructor constructor)
    {
        return new ConstructorResolver(adapterResolver, constructor);
    }

    private ConstructorResolver(AdapterResolver adapterResolver, TargetConstructor constructor)
    {
        AdapterResolver = adapterResolver;
        Constructor = constructor;
    }
    
    public AdapterResolver AdapterResolver { get; }

    public TargetConstructor Constructor { get; }

    public ConstructorResolution CreateResolution(MapperConfigurations configurations)
    {
        // 1 - get the target parameters and the available source items for constructors.
        var availableParameters = AvailableParameter.Create(Constructor);
        var availableSourceItems = AvailableSourceItems.CreateAvailableSourceItemsForConstructors(AdapterResolver.SourceProperties);

        // 2 - create the parameter context for each target parameter.
        var parametersResolver = availableParameters.Select(p => ParameterResolver.Create(p, availableSourceItems)).ToList();

        // 3 - resolve each parameter context.
        var parametersResolutions = parametersResolver.Select(p => p.CreateResolution(configurations)).ToList();

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

            return new ConstructorResolution(Constructor, failure);
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

            return new ConstructorResolution(Constructor, failure);
        }

        // 6 - create the resolution.
        return new ConstructorResolution(Constructor, parametersResolutions);
    }
}
