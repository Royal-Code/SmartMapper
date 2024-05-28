using System.Reflection;

namespace RoyalCode.SmartMapper.Mapping.Options;

/// <summary>
/// <para>
///     Options for define the target constructor.
/// </para>
/// </summary>
public sealed class ConstructorOptions : ParametersOptionsBase<ConstructorParameterOptions>
{
    private ICollection<ConstructorParameterOptions>? parametersOptions;

    /// <summary>
    /// Creates a new instance of the <see cref="ConstructorOptions"/> class.
    /// </summary>
    /// <param name="targetType">The target type to be constructed.</param>
    public ConstructorOptions(Type targetType) : base(targetType) { }

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
    protected override IEnumerable<ConstructorParameterOptions> Parameters
        => parametersOptions ?? Enumerable.Empty<ConstructorParameterOptions>();

    /// <inheritdoc />
    public override ConstructorParameterOptions GetParameterOptions(PropertyInfo sourceProperty)
    {
        parametersOptions ??= [];

        var options = parametersOptions.FirstOrDefault(p => p.SourceProperty == sourceProperty);

        if (options is not null)
            return options;

        options = new ConstructorParameterOptions(TargetType, sourceProperty);
        parametersOptions.Add(options);

        return options;
    }
}