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
    private ConstructorOptionsBase? constructorOptions;
    private ICollection<ToMethodOptions>? propertyToMethodOptions;
    private ICollection<MethodOptions>? sourceToMethodOptions;

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
    ///     Gets the options for mapping a source type to a method.
    /// </para>
    /// </summary>
    /// <returns>
    ///     All options for mapping a source type to a method or an empty collection if no options have been set.
    /// </returns>
    public IEnumerable<MethodOptions> GetSourceToMethodOptions()
    {
        return sourceToMethodOptions ?? Enumerable.Empty<MethodOptions>();
    }

    /// <summary>
    /// <para>
    ///     Adds an option for mapping a source type to a method.
    /// </para>
    /// </summary>
    /// <param name="optionsBase">The options for mapping a source type to a method.</param>
    public void AddToMethod(MethodOptions optionsBase)
    {
        sourceToMethodOptions ??= new List<MethodOptions>();
        sourceToMethodOptions.Add(optionsBase);
    }

    /// <summary>
    /// <para>
    ///     Gets the options for the constructor of the target type.
    /// </para>
    /// </summary>
    /// <returns>
    ///     The options for the constructor of the target type.
    /// </returns>
    public ConstructorOptionsBase GetConstructorOptions()
    {
        constructorOptions ??= new ConstructorOptionsBase(TargetType);
        return constructorOptions;
    }
}