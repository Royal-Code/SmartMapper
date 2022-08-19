using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

/// <summary>
/// <para>
///     Base options for mapping the source properties to construtor or methods of the target type.
/// </para>
/// </summary>
public abstract class ParametersOptionsBase
{
    /// <summary>
    /// Constructor with the target type.
    /// </summary>
    /// <param name="targetType">The target type.</param>
    public ParametersOptionsBase(Type targetType)
    {
        TargetType = targetType;
    }

    /// <summary>
    /// The target type, which will be mapped from properties to constructor parameters or methods.
    /// </summary>
    public Type TargetType { get; }

    /// <summary>
    /// Gets the options for mapping a source property to a parameter.
    /// </summary>
    /// <param name="sourceProperty">The source property.</param>
    /// <returns>
    ///     The options for mapping a source property to a parameter.
    /// </returns>
    public abstract ToParameterOptionsBase GetParameterOptions(PropertyInfo sourceProperty);

    /// <summary>
    /// <para>
    ///     Try to get the options for mapping a source property to a parameter.
    /// </para>
    /// </summary>
    /// <param name="sourceProperty">The source property.</param>
    /// <param name="parameterOptions">The options for mapping a source property to a parameter.</param>
    /// <returns>
    ///     <c>true</c> if the options for mapping a source property to a parameter were found; otherwise, <c>false</c>.
    /// </returns>
    public abstract bool TryGetParameterOptions(
        PropertyInfo sourceProperty,
        [NotNullWhen(true)] out ToParameterOptionsBase? parameterOptions);
}