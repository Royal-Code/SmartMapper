using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Core.Extensions;
using RoyalCode.SmartMapper.Core.Configurations;

namespace RoyalCode.SmartMapper.Adapters.Resolutions.Contexts;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

internal sealed class AdapterContext
{
    public static AdapterContext Create(AdapterOptions options)
    {
        var sourceType = options.SourceType;

        // gets the properties of the source that should be mapped
        var sourceProperties = sourceType.GetSourceProperties();

        // gets the source property options
        var propertyOptions = sourceProperties
            .Select(options.SourceOptions.GetPropertyOptions)
            .ToList();

        // creates the context for the adapter resolution
        return new AdapterContext
        {
            PropertyOptions = propertyOptions,
            Options = options
        };
    }

    private AdapterContext() { }

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
    public List<PropertyOptions> PropertyOptions { get; private init; }

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



        // resolve properties mapping



        throw new NotImplementedException();
    }

    private struct Resolutions
    {
        public ActivationResolution? ActivationResolution { get; set; }
    }
}
