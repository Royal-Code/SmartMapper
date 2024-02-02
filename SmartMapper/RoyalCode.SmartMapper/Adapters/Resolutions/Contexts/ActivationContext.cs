using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Resolvers;
using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Resolutions.Contexts;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

internal sealed class ActivationContext
{
    public static ActivationContext Create(AdapterContext adapterContext)
    {
        return new ActivationContext()
        {
            AdapterContext = adapterContext,
            EligibleConstructors = EligibleConstructor.Create(adapterContext.TargetOptions.GetConstructorOptions())
        };
    }

    private ActivationContext() { }

    public AdapterContext AdapterContext { get; private init; }

    public AdapterOptions Options => AdapterContext.Options;

    public ICollection<EligibleConstructor> EligibleConstructors { get; private init; }

    public ActivationResolution CreateResolution(MapperConfigurations configurations)
    {
        // event: activation resolution started. here the interceptor can be called in future versions

        // get eligible target constructors
        var targetConstructor = EligibleConstructor.Create(Options.TargetOptions.GetConstructorOptions());

        // if none constructor is eligible, generate a failure resolution
        if (targetConstructor.Count is 0)
        {
            return new(new ResolutionFailure($"None elegible constructor for adapt {Options.SourceType.Name} type to {Options.TargetType.Name} type."));
        }

        // if has a single empty constructor, then create a resolution for the constructor
        if (EligibleConstructor.HasSingleEmptyConstructor(targetConstructor, out var eligible))
        {
            return new(new ConstructorResolution([], eligible.Info));
        }
        else
        {
            // for each eligible constructor, try to resolve the constructor
            foreach (var ctor in targetConstructor)
            {
                var ctorContext = ConstructorContext.Create(AdapterContext, ctor);
                var ctorResolution = ctorContext.CreateResolution(configurations);
                if (ctorResolution.Resolved)
                {
                    return new(ctorResolution);
                }
            }
        }

        // if none constructor is resolved, generate a failure resolution
        return new(new ResolutionFailure($"None constructor for adapt {Options.SourceType.Name} type to {Options.TargetType.Name} type was resolved."));
    }
}
