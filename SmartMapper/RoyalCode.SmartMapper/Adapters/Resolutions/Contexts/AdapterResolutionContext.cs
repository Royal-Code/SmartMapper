using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Core.Extensions;
using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Core.Resolutions;

using RoyalCode.SmartMapper.Adapters.Resolvers;

namespace RoyalCode.SmartMapper.Adapters.Resolutions.Contexts;

internal class AdapterResolutionContext
{
    public static AdapterResolutionContext Create(AdapterOptions options)
    {
        var sourceType = options.SourceType;

        // gets the properties of the source that should be mapped
        var sourceProperties = sourceType.GetSourceProperties();

        // gets the source property options
        var propertyOptions = sourceProperties
            .Select(options.SourceOptions.GetPropertyOptions)
            .ToList();

        // creates the context for the adapter resolution
        return new AdapterResolutionContext
        {
            PropertyOptions = propertyOptions,
            SourceOptions = options.SourceOptions,
            TargetOptions = options.TargetOptions
        };
    }

    /// <summary>
    /// Contains the options for the source properties of the mapping.
    /// </summary>
    public SourceOptions SourceOptions { get; private init; }

    /// <summary>
    /// Contains the options for the target members of the mapping.
    /// </summary>
    public TargetOptions TargetOptions { get; private init; }

    /// <summary>
    /// Contains the options for all properties of the source type.
    /// </summary>
    public List<PropertyOptions> PropertyOptions { get; private init; }

    public AdapterResolution CreateResolution(MapperConfigurations configurations)
    {
        // event: resolution started. here the interceptor can be called in future versions

        var resolutions = new Resolutions();

        // get eligible target constructors
        var targetConstructor = EligibleConstructor.Create(TargetOptions.GetConstructorOptions());

        // if none constructor is eligible, generate a failure resolution
        if (targetConstructor.Count is 0)
        {
            return new AdapterResolution(new ResolutionFailure($"None elegible constructor for adapt {SourceOptions.SourceType.Name} type to {TargetOptions.TargetType.Name} type."));
        }

        // if has a single empty constructor, then create a resolution for the constructor
        if (EligibleConstructor.HasSingleEmptyConstructor(targetConstructor, out var eligible))
        {
            resolutions.ConstructorResolution = new([], eligible.Info);
        }
        else
        {
            // for each eligible constructor, try to resolve the constructor
        }

        throw new NotImplementedException();
    }

    private struct Resolutions
    {
        public ConstructorResolution? ConstructorResolution { get; set; }
    }
}
