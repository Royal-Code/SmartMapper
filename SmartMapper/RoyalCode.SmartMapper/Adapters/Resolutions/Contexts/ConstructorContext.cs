using RoyalCode.SmartMapper.Adapters.Resolvers;
using RoyalCode.SmartMapper.Adapters.Resolvers.Avaliables;
using RoyalCode.SmartMapper.Core.Configurations;

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
        foreach (var parameterContext in parametersContext)
        {
            var parameterResolution = parameterContext.CreateResolution(configurations);
        }
    }
}
