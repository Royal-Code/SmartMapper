﻿
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Options;

/// <summary>
/// Options containing configuration for the mapping of a source type to a destination type method.
/// </summary>
public sealed class SourceToMethodOptions
{
    private SourceToMethodStrategy strategy;
    private ICollection<ToMethodParameterOptions>? parametersSequence;

    /// <summary>
    /// Creates a new <see cref="SourceToMethodOptions"/> instance with the specified adapter options
    /// and the method options.
    /// </summary>
    /// <param name="methodOptions">The method options.</param>
    public SourceToMethodOptions(MethodOptions methodOptions)
    {
        MethodOptions = methodOptions;
    }

    /// <summary>
    /// The method options.
    /// </summary>
    public MethodOptions MethodOptions { get; }

    /// <summary>
    /// The strategy used to mapping the properties of the source to the target method.
    /// </summary>
    public SourceToMethodStrategy Strategy
    {
        get => strategy;
        internal set
        {
            if (strategy != SourceToMethodStrategy.Default)
                throw new InvalidOperationException("The method has been set up before");
            strategy = value;
        }
    }

    /// <summary>
    /// Adds the property to parameter options to the selected property to parameter sequence.
    /// </summary>
    /// <param name="options">The property to parameter options.</param>
    public void AddParameterSequence(ToMethodParameterOptions options)
    {
        if (strategy != SourceToMethodStrategy.SelectedParameters)
            throw new InvalidOperationException(
                "Invalid strategy, this method requires the strategy 'SelectedParameters' and it has not been assigned.");

        parametersSequence ??= new List<ToMethodParameterOptions>();
        parametersSequence.Add(options);
    }

    /// <summary>
    /// Add a source property to a parameter in sequence.
    /// </summary>
    /// <param name="options">The source property options.</param>
    public ToMethodParameterResolutionOptions AddPropertyToParameterSequence(PropertyOptions options)
    {
        var parameterOptions = MethodOptions.GetParameterOptions(options.Property);

        // when created the resolution options, the options are resolved by the resolution.
        var resolution = new ToMethodParameterResolutionOptions(options, parameterOptions);

        AddParameterSequence(parameterOptions);

        return resolution;
    }

    /// <summary>
    /// <para>
    ///     Gets the selected property to parameter sequence.
    /// </para>
    /// </summary>
    /// <returns>The selected property to parameter sequence.</returns>
    public IEnumerable<ToMethodParameterOptions> GetAllParameterSequence()
    {
        return parametersSequence ?? Enumerable.Empty<ToMethodParameterOptions>();
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
