using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using RefactorOptions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

/// <summary>
/// <para>
///     Options for mapping the source properties to a method.
/// </para>
/// </summary>
public class MethodOptionsBase : ParametersOptionsBase
{
    private ICollection<ToMethodParameterOptions>? parametersOptions;

    /// <summary>
    /// Creates a new instance of <see cref="MethodOptionsBase"/>.
    /// </summary>
    /// <param name="targetType"></param>
    public MethodOptionsBase(Type targetType) : base(targetType) { }

    /// <summary>
    /// The defined mapped method.
    /// </summary>
    public MethodInfo? Method { get; internal set; }
    
    /// <summary>
    /// The defined mapped method name.
    /// </summary>
    public string? MethodName { get; internal set; }
    
    /// <inheritdoc />
    public override ToParameterOptionsBase GetParameterOptions(PropertyInfo sourceProperty)
    {
        parametersOptions ??= new List<ToMethodParameterOptions>();

        var options = parametersOptions.FirstOrDefault(x => x.SourceProperty == sourceProperty);
        if (options is null)
        {
            options = new ToMethodParameterOptions(this, sourceProperty);
            parametersOptions.Add(options);
        }

        return options;
    }

    /// <inheritdoc />
    public override bool TryGetParameterOptions(
        PropertyInfo sourceProperty, 
        [NotNullWhen(true)] out ToParameterOptionsBase? parameterOptions)
    {
        parameterOptions = parametersOptions?.FirstOrDefault(x => x.SourceProperty == sourceProperty);
        return parameterOptions is not null;
    }
}