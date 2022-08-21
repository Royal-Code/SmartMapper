using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

/// <summary>
/// <para>
///     Options for define the target construtor.
/// </para>
/// </summary>
public class ConstructorOptionsBase : ParametersOptionsBase
{
    private ICollection<ToConstructorParameterOptions>? parametersOptions;

    /// <summary>
    /// Creates a new instance of the <see cref="ConstructorOptionsBase"/> class.
    /// </summary>
    /// <param name="targetType">The target type to be constructed.</param>
    public ConstructorOptionsBase(Type targetType) : base(targetType) { }
    
    /// <summary>
    /// <para>
    ///     A value for select the constructor.
    /// </para>
    /// <para>
    ///     Defines a number of parameters for the constructor.
    /// </para>
    /// </summary>
    public int? NumberOfParameters { get; internal set; }

    /// <summary>
    /// <para>
    ///     A value for select the constructor.
    /// </para>
    /// <para>
    ///     Defines the parameter types for the constructor.
    /// </para>
    /// </summary>
    public Type[]? ParameterTypes { get; internal set; }

    /// <inheritdoc />
    public override ToParameterOptionsBase GetParameterOptions(PropertyInfo sourceProperty)
    {
        parametersOptions ??= new List<ToConstructorParameterOptions>();

        var options = parametersOptions.FirstOrDefault(p => p.SourceProperty == sourceProperty);
        if (options is null)
        {
            options = new ToConstructorParameterOptions(TargetType, sourceProperty);
            parametersOptions.Add(options);
        }

        return options;
    }

    /// <inheritdoc />
    public override bool TryGetParameterOptions(
        PropertyInfo sourceProperty,
        [NotNullWhen(true)] out ToParameterOptionsBase? parameterOptions)
    {
        parameterOptions = parametersOptions?.FirstOrDefault(p => p.SourceProperty == sourceProperty);
        return parameterOptions != null;
    }
}