
using System.Text;
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

        parametersSequence ??= [];
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
    public IEnumerable<ToMethodParameterOptions> GetAllParameterSequence()
    {
        return parametersSequence ?? [];
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

    /// <inheritdoc />
    public override string ToString()
    {
        var sb = new StringBuilder("Strategy: ").Append(Strategy).Append(", ");

        if (parametersSequence is not null)
        {
            sb.Append("Parameters: ").Append(parametersSequence.Count).Append("(");
            foreach (var parameter in parametersSequence)
            {
                sb.Append(parameter).Append(", ");
            }
            sb.Append("), ");
        }
        else
        {
            sb.Append("Parameters: none, ");
        }

        if (MethodOptions.Method is not null)
        {
            sb.Append("Informed method: ").Append(MethodOptions.Method);
        }
        else if (MethodOptions.MethodName is not null)
        {
            sb.Append("Informed method name: ").Append(MethodOptions.MethodName);
        }
        else
        {
            sb.Append("No method informed.");
        }
        
        return sb.ToString();
    }
}
