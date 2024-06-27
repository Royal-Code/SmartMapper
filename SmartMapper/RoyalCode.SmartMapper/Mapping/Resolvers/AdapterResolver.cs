using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Mapping.Options;
using RoyalCode.SmartMapper.Mapping.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;
using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

namespace RoyalCode.SmartMapper.Mapping.Resolvers;

internal sealed class AdapterResolver
{
    public static AdapterResolver Create(MappingOptions options)
    {
        if (options.Category != MappingCategory.Adapter)
            throw  new ArgumentException("The mapping options must be for an adapter.", nameof(options));
        
        // gets the source property options and creates the source items
        var sourceProperties = SourceProperty.Create(options.SourceType, options.SourceOptions);
        
        // creates the context for the adapter resolution
        return new AdapterResolver(options, sourceProperties);
    }

    private AdapterResolver(MappingOptions options, IReadOnlyCollection<SourceProperty> sourceItems)
    {
        Options = options;
        SourceItems = sourceItems;
        AvailableTargetProperties = new AvailableTargetProperties(options.TargetType);
        AvailableTargetMethods = new AvailableTargetMethods(options.TargetType);
    }

    /// <summary>
    /// The adapter options used to create the resolution.
    /// </summary>
    public MappingOptions Options { get; private init; }

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
    public IReadOnlyCollection<SourceProperty> SourceItems { get; private init; }

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
        var activationContext = ActivationResolver.Create(this);
        var activationResolution = activationContext.CreateResolution(configurations);
        if (activationResolution.Resolved)
            resolutions.ActivationResolution = activationResolution;
        else
            return new AdapterResolution(activationResolution.Failure);

        // resolve methods mapping
        var methodContext = SourceToMethodsResolver.Create(this);
        SourceToMethodsResolutions sourceToMethodResolution = methodContext.CreateResolution(configurations);
        if (sourceToMethodResolution.Resolved)
            resolutions.MethodsResolutions = sourceToMethodResolution;
        else
            return new AdapterResolution(sourceToMethodResolution.Failure);

        // resolve properties mapping
        var propertiesContext = PropertiesResolver.Create(this);
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
        
        public SourceToMethodsResolutions? MethodsResolutions { get; set; }
        
        public PropertiesResolution? PropertiesResolutions { get; set; }
    }
}
