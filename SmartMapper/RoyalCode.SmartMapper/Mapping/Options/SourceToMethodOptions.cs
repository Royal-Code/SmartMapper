using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace RoyalCode.SmartMapper.Mapping.Options;

/// <summary>
/// Options containing configuration for the mapping of a source type to a destination type method.
/// </summary>
public sealed class SourceToMethodOptions
{
    private SourceToMethodStrategy strategy;

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
        private set
        {
            if (strategy is not SourceToMethodStrategy.Default)
                throw new InvalidOperationException("The method has been set up before");
            strategy = value;
        }
    }

    /// <summary>
    /// Options for mapping the parameters of the target method.
    /// </summary>
    public SourceToMethodParametersOptions? ParametersOptions { get; set; }

    /// <summary>
    /// Check that this mapping uses the strategy of mapping all properties from the source to the target method.
    /// </summary>
    [MemberNotNullWhen(true, nameof(PropertiesOptions))]
    public bool HasAllProperties => Strategy is SourceToMethodStrategy.AllParameters;

    /// <summary>
    /// Options for mapping all source properties to the target method.
    /// </summary>
    public SourceToMethodPropertiesOptions? PropertiesOptions { get; set; }

    /// <summary>
    /// Configure the strategy to be <see cref="SourceToMethodStrategy.SelectedParameters"/>.
    /// </summary>
    /// <param name="parametersOptions">The parameters options.</param>
    public void UseSelectedParameters(out SourceToMethodParametersOptions parametersOptions)
    {
        if (IsSelectedParameters(out var options))
        {
            parametersOptions = options;
            return;
        }

        Strategy = SourceToMethodStrategy.SelectedParameters;
        ParametersOptions = new(MethodOptions);
        parametersOptions = ParametersOptions;
    }

    /// <summary>
    /// Configure the strategy to be <see cref="SourceToMethodStrategy.AllParameters"/>.
    /// </summary>
    /// <param name="propertiesOptions">The properties options.</param>
    public void UseAllProperties(out SourceToMethodPropertiesOptions propertiesOptions)
    {
        if (IsAllParameters(out var options))
        {
            propertiesOptions = options;
            return;
        }

        Strategy = SourceToMethodStrategy.AllParameters;
        PropertiesOptions = new(MethodOptions);
        propertiesOptions = PropertiesOptions;
    }

    /// <summary>
    /// Check that this mapping uses the strategy of selecting the parameters of the target method.
    /// </summary>
    /// <param name="parametersOptions">
    ///     The parameters options when the strategy is <see cref="SourceToMethodStrategy.SelectedParameters"/>
    /// </param>
    /// <returns>
    ///     true if the strategy is <see cref="SourceToMethodStrategy.SelectedParameters"/>, false otherwise.
    /// </returns>
    public bool IsSelectedParameters([NotNullWhen(true)] out SourceToMethodParametersOptions? parametersOptions)
    {
        parametersOptions = ParametersOptions;
        return parametersOptions is not null;
    }

    /// <summary>
    /// Check that this mapping uses the strategy of mapping all properties from the source to the target method.
    /// </summary>
    /// <param name="propertiesOptions">
    ///     The properties options when the strategy is <see cref="SourceToMethodStrategy.AllParameters"/>.
    /// </param>
    /// <returns>
    ///     true if the strategy is <see cref="SourceToMethodStrategy.AllParameters"/>.
    /// </returns>
    public bool IsAllParameters([NotNullWhen(true)] out SourceToMethodPropertiesOptions? propertiesOptions)
    {
        propertiesOptions = PropertiesOptions;
        return propertiesOptions is not null;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        var sb = new StringBuilder("Strategy: ").Append(Strategy).Append(", ");

        // TODO: refactor this part.
        //if (parametersSequence is not null)
        //{
        //    sb.Append("Parameters: ").Append(parametersSequence.Count).Append("(");
        //    foreach (var parameter in parametersSequence)
        //    {
        //        sb.Append(parameter).Append(", ");
        //    }
        //    sb.Append("), ");
        //}
        //else
        //{
        //    sb.Append("Parameters: none, ");
        //}

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
