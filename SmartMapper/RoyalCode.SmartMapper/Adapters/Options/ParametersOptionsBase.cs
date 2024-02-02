using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace RoyalCode.SmartMapper.Adapters.Options;

/// <summary>
/// <para>
///     Base options for mapping the source properties to construtor or methods of the target type.
/// </para>
/// </summary>
public abstract class ParametersOptionsBase<TToParameter>
    where TToParameter : ToParameterOptionsBase
{
    /// <summary>
    /// Constructor with the target type.
    /// </summary>
    /// <param name="targetType">The target type.</param>
    protected ParametersOptionsBase(Type targetType)
    {
        TargetType = targetType;
    }

    /// <summary>
    /// The target type, which will be mapped from properties to constructor parameters or methods.
    /// </summary>
    public Type TargetType { get; }

    /// <summary>
    /// The invocable parameters.
    /// </summary>
    protected abstract IEnumerable<TToParameter> Parameters { get; }

    /// <summary>
    /// Gets the options for mapping a source property to a parameter.
    /// </summary>
    /// <param name="sourceProperty">The source property.</param>
    /// <returns>
    ///     The options for mapping a source property to a parameter.
    /// </returns>
    public abstract TToParameter GetParameterOptions(PropertyInfo sourceProperty);

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
    public virtual bool TryGetParameterOptions(
        PropertyInfo sourceProperty,
        [NotNullWhen(true)] out TToParameter? parameterOptions)
    {
        parameterOptions = Parameters.FirstOrDefault(x => x.SourceProperty == sourceProperty);
        return parameterOptions is not null;
    }

    /// <summary>
    /// <para>
    ///     Try to get the options for mapping a source property to a parameter.
    /// </para>
    /// </summary>
    /// <param name="parameterName">The parameter name.</param>
    /// <param name="parameterOptions">The options for mapping a source property to a parameter.</param>
    /// <returns>
    ///     <c>true</c> if the options for mapping a source property to a parameter were found; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     If has more then one property mapped to the same parameter.
    /// </exception>
    public virtual bool TryGetParameterOptions(
        string parameterName,
        [NotNullWhen(true)] out TToParameter? parameterOptions)
    {
        // TODO: require unit tests (TryGetParameterOptions)
        try
        {
            parameterOptions = Parameters.SingleOrDefault(p => p.ParameterName == parameterName)
                ?? Parameters.FirstOrDefault(p => p.SourceProperty.Name == parameterName);
            return parameterOptions is not null;
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException(
                "Multiple properties are mapped to the same parameter",
                ex);
        }
    }
}