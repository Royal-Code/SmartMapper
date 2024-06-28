using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Options;
using RoyalCode.SmartMapper.Mapping.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

namespace RoyalCode.SmartMapper.Mapping.Resolvers;

internal sealed class ActivationResolver
{
    public static ActivationResolver Create(AdapterResolver adapterResolver)
    {
        return new ActivationResolver(adapterResolver);
    }

    private ActivationResolver(AdapterResolver adapterResolver)
    {
        AdapterResolver = adapterResolver;
    }

    /// <summary>
    /// The adapter context that creates this activation context.
    /// </summary>
    public AdapterResolver AdapterResolver { get; }

    /// <summary>
    /// The adapter options to create the resolution.
    /// </summary>
    public MappingOptions Options => AdapterResolver.Options;

    public ActivationResolution CreateResolution(MapperConfigurations configurations)
    {
        // event: activation resolution started. here the interceptor can be called in future versions

        // 1 - get eligible target constructors
        var options = Options.TargetOptions.GetConstructorOptions();
        var eligibleConstructors = TargetConstructor.Create(options.TargetType, options.ParameterTypes, options.NumberOfParameters);

        // if none constructor is eligible, generate a failure resolution
        if (eligibleConstructors.Count is 0)
        {
            return new(new ResolutionFailure($"None constructor is available for adapt {Options.SourceType.Name} type to {Options.TargetType.Name} type."));
        }

        // 2 - check if it has a single empty constructor, then create a resolution for the constructor
        if (TargetConstructor.HasSingleEmptyConstructor(eligibleConstructors, out var eligible))
        {
            return new(new ConstructorResolution(eligible, []));
        }

        // a list of failures
        List<ResolutionFailure>? failures = null;

        // 3 - for each eligible constructor, try to resolve the constructor
        foreach (var ctor in eligibleConstructors)
        {
            var ctorContext = ConstructorResolver.Create(AdapterResolver, ctor);
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
