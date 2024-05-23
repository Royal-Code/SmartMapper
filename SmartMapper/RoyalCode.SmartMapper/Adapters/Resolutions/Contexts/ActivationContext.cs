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

    /// <summary>
    /// The adapter context that creates this activation context.
    /// </summary>
    public AdapterContext AdapterContext { get; private init; }

    /// <summary>
    /// The adapter options to create the resolution.
    /// </summary>
    public AdapterOptions Options => AdapterContext.Options;

    /// <summary>
    /// The elegible constructors.
    /// </summary>
    public ICollection<EligibleConstructor> EligibleConstructors { get; private init; }

    public ActivationResolution CreateResolution(MapperConfigurations configurations)
    {
        // event: activation resolution started. here the interceptor can be called in future versions

        // 1 - get eligible target constructors
        var targetConstructor = EligibleConstructor.Create(Options.TargetOptions.GetConstructorOptions());

        // if none constructor is eligible, generate a failure resolution
        if (targetConstructor.Count is 0)
        {
            return new(new ResolutionFailure($"None eligible constructor for adapt {Options.SourceType.Name} type to {Options.TargetType.Name} type."));
        }

        // 2 - check if it has a single empty constructor, then create a resolution for the constructor
        if (targetConstructor.Count is 1 && EligibleConstructor.HasSingleEmptyConstructor(targetConstructor, out var eligible))
        {
            return new(new ConstructorResolution(eligible.Info, []));
        }

        // a list of failures
        List<ResolutionFailure>? failures = null;

        // 3 - for each eligible constructor, try to resolve the constructor
        foreach (var ctor in targetConstructor)
        {
            var ctorContext = ConstructorContext.Create(AdapterContext, ctor);
            var ctorResolution = ctorContext.CreateResolution(configurations);
            
            // if resolved, return the resolution
            if (ctorResolution.Resolved)
            {
                var activationResolution = new ActivationResolution(ctorResolution);
                activationResolution.Completed();
                return activationResolution;
            }
            
            // otherwise, add the failure to the list
            failures ??= [];
            failures.Add(ctorResolution.Failure);
        }

        // if none constructor is resolved, generate a failure resolution
        var failure = new ResolutionFailure(
            $"None constructor for adapt {Options.SourceType.Name} type " +
            $"to {Options.TargetType.Name} type was resolved.");

        foreach (var f in failures!)
            failure.AddMessages(f.Messages);

        return new(failure);
    }
}
