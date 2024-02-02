using RoyalCode.SmartMapper.Adapters.Resolvers;
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

    }
}
