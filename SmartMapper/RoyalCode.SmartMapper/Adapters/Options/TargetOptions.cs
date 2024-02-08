namespace RoyalCode.SmartMapper.Adapters.Options;

/// <summary>
/// <para>
///     Contains options for configured for mapping of a target type.
/// </para>
/// </summary>
public sealed class TargetOptions
{
    ////private ICollection<ToPropertyOptions>? toPropertyOptions; // eh necessário ? validações futuras?

    private ConstructorOptions? constructorOptions;
    private ICollection<MethodOptions>? methodOptions;

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
    ///     Create a new instance of <see cref="MethodOptions"/> for the target type.
    /// </para>
    /// </summary>
    /// <returns>A new instance of <see cref="MethodOptions"/> for the target type.</returns>
    public MethodOptions CreateMethodOptions()
    {
        var options = new MethodOptions(TargetType);
        methodOptions ??= [];
        methodOptions.Add(options);
        return options;
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