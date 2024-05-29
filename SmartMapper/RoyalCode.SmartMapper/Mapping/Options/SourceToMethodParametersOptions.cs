using System.Collections.ObjectModel;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;

namespace RoyalCode.SmartMapper.Mapping.Options;

/// <summary>
/// Mapping options between source properties and the parameters of a target method.
/// </summary>
public sealed class SourceToMethodParametersOptions
{
    private readonly MethodOptions methodOptions;
    private IList<MethodParameterOptions>? parametersSequence;

    /// <summary>
    /// Creates a new instance of <see cref="SourceToMethodParametersOptions"/>.
    /// </summary>
    /// <param name="methodOptions">Method options.</param>
    public SourceToMethodParametersOptions(MethodOptions methodOptions)
    {
        this.methodOptions = methodOptions;
    }

    /// <summary>
    /// Adds the property to parameter options to the selected property to parameter sequence.
    /// </summary>
    /// <param name="options">The property to parameter options.</param>
    public void AddParameterSequence(MethodParameterOptions options)
    {
        parametersSequence ??= [];
        parametersSequence.Add(options);
    }

    /// <summary>
    /// Add a source property to a parameter in sequence.
    /// </summary>
    /// <param name="options">The source property options.</param>
    public ToMethodParameterResolutionOptions AddPropertyToParameterSequence(PropertyOptions options)
    {
        var parameterOptions = methodOptions.GetParameterOptions(options.Property);

        // when created the resolution options, the options are resolved by the resolution.
        var resolution = ToMethodParameterResolutionOptions.Resolves(options, parameterOptions);

        AddParameterSequence(parameterOptions);

        return resolution;
    }

    /// <summary>
    /// <para>
    ///     Gets the selected property to parameter sequence.
    /// </para>
    /// </summary>
    /// <returns>The selected property to parameter sequence.</returns>
    public IReadOnlyCollection<MethodParameterOptions> GetAllParameterSequence()
    {
        return new ReadOnlyCollection<MethodParameterOptions>(parametersSequence ?? []);
    }

    /// <summary>
    /// <para>
    ///     Get the count of the selected property to parameter sequence.
    /// </para>
    /// </summary>
    /// <returns>The count of the selected property to parameter sequence.</returns>
    public int CountParameterSequence()
    {
        return parametersSequence?.Count ?? 0;
    }
}
