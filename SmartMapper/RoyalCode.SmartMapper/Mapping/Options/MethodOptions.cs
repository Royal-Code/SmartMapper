using RoyalCode.SmartMapper.Core.Exceptions;
using System.Reflection;

namespace RoyalCode.SmartMapper.Mapping.Options;

/// <summary>
/// <para>
///     Options for define the mapping of a source properties to a target method.
/// </para>
/// </summary>
public sealed class MethodOptions : ParametersOptionsBase<MethodParameterOptions>
{
    private ICollection<MethodParameterOptions>? parametersOptions;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="targetType"></param>
    public MethodOptions(Type targetType) : base(targetType) { }

    /// <summary>
    /// The defined mapped method.
    /// </summary>
    public MethodInfo? Method { get; internal set; }

    /// <summary>
    /// The defined mapped method name.
    /// </summary>
    public string? MethodName { get; internal set; }

    /// <inheritdoc />
    protected override IEnumerable<MethodParameterOptions> Parameters 
        => parametersOptions ?? Enumerable.Empty<MethodParameterOptions>();

    /// <inheritdoc />
    public override MethodParameterOptions GetParameterOptions(PropertyInfo sourceProperty)
    {
        parametersOptions ??= [];

        var options = parametersOptions.FirstOrDefault(x => x.SourceProperty == sourceProperty);
        if (options is null)
        {
            options = new MethodParameterOptions(this, sourceProperty);
            parametersOptions.Add(options);
        }

        return options;
    }

    /// <summary>
    /// <para>
    ///     Defines the name of the method of the mapping.
    /// </para>
    /// </summary>
    /// <param name="methodName">The name of the method.</param>
    /// <exception cref="InvalidMethodNameException">
    ///     Thrown if the none method exists in the target class for the given name.
    /// </exception>
    public void WithMethodName(string methodName)
    {
        if (string.IsNullOrWhiteSpace(methodName))
            throw new InvalidMethodNameException("Value cannot be null or whitespace.", nameof(methodName));

        var methods = TargetType.GetMethods().Where(m => m.Name == methodName).ToList();
        if (methods.Count is 0)
            throw new InvalidMethodNameException(
                $"Method '{methodName}' not found on type '{TargetType.Name}'.", nameof(methodName));

        if (methods.Count is 1)
            Method = methods[0];

        MethodName = methodName;
    }
}
