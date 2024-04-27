using RoyalCode.SmartMapper.Adapters.Resolvers;
using RoyalCode.SmartMapper.Adapters.Resolvers.Available;
using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Resolutions.Contexts;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

/// <summary>
/// Represents the context for the constructor resolution process.
/// </summary>
internal sealed class ConstructorContext
{
    public static ConstructorContext Create(AdapterContext adapterContext, EligibleConstructor eligibleConstructor)
    {
        return new ConstructorContext()
        {
            AdapterContext = adapterContext,
            EligibleConstructor = eligibleConstructor
        };
    }

    public AdapterContext AdapterContext { get; private init; }

    public EligibleConstructor EligibleConstructor { get; private init; }

    public ConstructorResolution CreateResolution(MapperConfigurations configurations)
    {
        // 1 - get the target parameters and the available source items for constructors.
        var targetParameters = EligibleConstructor.CreateTargetParameters();
        var availableSourceItems = AvailableSourceItems.CreateAvailableSourceItemsForConstructors(AdapterContext.SourceItems);

        // 2 - create the parameter context for each target parameter.
        var parametersContext = targetParameters.Select(p => ParameterContext.Create(p, availableSourceItems)).ToList();

        // 3 - resolve each parameter context.
        var parametersResolutions = parametersContext.Select(p => p.CreateResolution(configurations)).ToList();

        // 4 - check if all parameters are resolved.
        if (!parametersResolutions.TrueForAll(static p => p.Resolved))
        {
            // when not resolved, return a not resolved resolution.
            var failure = new ResolutionFailure(
                $"The constructor for type '{AdapterContext.Options.TargetType.Name}' can't be resolved, " +
                $"some parameters don't have a resolution.");

            foreach (var p in parametersResolutions)
            {
                if (!p.Resolved)
                    failure.AddMessages(p.Failure.Messages);
            }

            return new ConstructorResolution(EligibleConstructor.Info, failure);
        }

        // 5 - check if all required source items are resolved.
        if (!availableSourceItems.AllRequiredItemsResolved)
        {
            // when not resolved, return a not resolved resolution.
            var failure = new ResolutionFailure(
                $"The constructor for type '{AdapterContext.Options.TargetType.Name}' was resolved, " +
                $"but some properties mapped to the constructor are not resolved.");

            foreach (var required in availableSourceItems.RequiredSourceProperties)
            {
                if (!required.Resolved)
                    failure.AddMessage(required.GetFailureMessage());
            }

            return new ConstructorResolution(EligibleConstructor.Info, failure);
        }

        // 6 - create the resolution.
        return new ConstructorResolution(EligibleConstructor.Info, parametersResolutions);
    }
}
