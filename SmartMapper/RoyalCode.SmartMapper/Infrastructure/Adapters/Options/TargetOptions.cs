using RefactorOptions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

/// <summary>
/// <para>
///     Contains options for configured for mapping of a target type.
/// </para>
/// </summary>
public class TargetOptions
{
    private ICollection<ToPropertyOptions>? toPropertyOptions;
    private ConstructorOptions? constructorOptions;
    private ICollection<MethodOptions>? methodOptions; // eh necessário ? validações futuras?

    /// <summary>
    /// Creates a new instance of <see cref="TargetOptions"/>.
    /// </summary>
    /// <param name="targetType">The target type.</param>
    public TargetOptions(Type targetType)
    {
        TargetType = targetType;
    }

    /// <summary>
    /// The target type.
    /// </summary>
    public Type TargetType { get; }

    /// <summary>
    /// <para>
    ///     Adds an option for mapping a source type to a method.
    /// </para>
    /// </summary>
    /// <param name="optionsBase">The options for mapping a source type to a method.</param>
    public void AddToMethod(MethodOptions optionsBase)
    {
        methodOptions ??= new List<MethodOptions>();
        methodOptions.Add(optionsBase);
    }

    /// <summary>
    /// <para>
    ///     Gets the options for the constructor of the target type.
    /// </para>
    /// </summary>
    /// <returns>
    ///     The options for the constructor of the target type.
    /// </returns>
    public ConstructorOptions GetConstructorOptions()
    {
        constructorOptions ??= new ConstructorOptions(TargetType);
        return constructorOptions;
    }
}