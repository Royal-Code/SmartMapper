using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Resolvers.Available;
using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Mapping.Options;

namespace RoyalCode.SmartMapper.Adapters.Resolutions.Contexts;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

internal sealed class AdapterContext
{
    public static AdapterContext Create(AdapterOptions options)
    {
        // gets the source property options and creates the source items
        var sourceItems = SourceItem.Create(options.SourceType, options.SourceOptions);
        
        // creates the context for the adapter resolution
        return new AdapterContext
        {
            SourceItems = sourceItems,
            Options = options,
            AvailableTargetProperties = new AvailableTargetProperties(options.TargetType),
            AvailableTargetMethods = new AvailableTargetMethods(options.TargetType)
        };
    }

    private AdapterContext() { }

    /// <summary>
    /// The adapter options used to create the resolution.
    /// </summary>
    public AdapterOptions Options { get; private init; }

    /// <summary>
    /// Contains the options for the source properties of the mapping.
    /// </summary>
    public SourceOptions SourceOptions => Options.SourceOptions;

    /// <summary>
    /// Contains the options for the target members of the mapping.
    /// </summary>
    public TargetOptions TargetOptions => Options.TargetOptions;

    /// <summary>
    /// Contains the options for all properties of the source type.
    /// </summary>
    public IReadOnlyCollection<SourceItem> SourceItems { get; private init; }

    /// <summary>
    /// The available properties for the target type.
    /// </summary>
    public AvailableTargetProperties AvailableTargetProperties { get; private init; }
    
    /// <summary>
    /// The available methods for the target type.
    /// </summary>
    public AvailableTargetMethods AvailableTargetMethods { get; private init; }
    
    public AdapterResolution CreateResolution(MapperConfigurations configurations)
    {
        // event: resolution started. here the interceptor can be called in future versions

        // a struct to store the resolutions
        var resolutions = new Resolutions();

        // resolve the activator/constructor
        var activationContext = ActivationContext.Create(this);
        var activationResolution = activationContext.CreateResolution(configurations);
        if (activationResolution.Resolved)
            resolutions.ActivationResolution = activationResolution;
        else
            return new AdapterResolution(activationResolution.Failure);

        // resolve methods mapping
        var methodContext = SourceToMethodsContext.Create(this);
        MethodsResolutions methodResolution = methodContext.CreateResolution(configurations);
        if (methodResolution.Resolved)
            resolutions.MethodsResolutions = methodResolution;
        else
            return new AdapterResolution(methodResolution.Failure);

        // resolve properties mapping
        var propertiesContext = PropertiesContext.Create(this);
        var propertiesResolution = propertiesContext.CreateResolution(configurations);
        if (propertiesResolution.Resolved)
            resolutions.PropertiesResolutions = propertiesResolution;
        else
            return new AdapterResolution(propertiesResolution.Failure);

        return new AdapterResolution(
            resolutions.ActivationResolution,
            resolutions.MethodsResolutions,
            resolutions.PropertiesResolutions);
    }

    private struct Resolutions
    {
        public ActivationResolution? ActivationResolution { get; set; }
        
        public MethodsResolutions? MethodsResolutions { get; set; }
        
        public PropertiesResolution? PropertiesResolutions { get; set; }
    }
}
