using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RefactorOptions;

public class AdapterOptions
{
    public AdapterOptions(Type sourceType, Type targetType)
    {
        SourceType = sourceType;
        TargetType = targetType;
        SourceOptions = new SourceOptions(sourceType);
        TargetOptions = new TargetOptions(targetType);
    }

    public Type SourceType { get; }

    public Type TargetType { get; }

    public SourceOptions SourceOptions { get; }

    public TargetOptions TargetOptions { get; }
}

public class TargetOptions
{
    private ICollection<ToPropertyOptions>? toPropertyOptions;
    private ConstructorOptionsBase? constructorOptions;
    private ICollection<ToMethodOptions>? propertyToMethodOptions;
    private ICollection<MethodOptionsBase>? sourceToMethodOptions;

    public TargetOptions(Type targetType)
    {
        TargetType = targetType;
    }

    public Type TargetType { get; }

    /// <summary>
    /// <para>
    ///     Gets the options for mapping a source type to a method.
    /// </para>
    /// </summary>
    /// <returns>
    ///     All options for mapping a source type to a method or an empty collection if no options have been set.
    /// </returns>
    public IEnumerable<MethodOptionsBase> GetSourceToMethodOptions()
    {
        return sourceToMethodOptions ?? Enumerable.Empty<MethodOptionsBase>();
    }

    /// <summary>
    /// <para>
    ///     Adds an option for mapping a source type to a method.
    /// </para>
    /// </summary>
    /// <param name="optionsBase">The options for mapping a source type to a method.</param>
    public void AddToMethod(MethodOptionsBase optionsBase)
    {
        sourceToMethodOptions ??= new List<MethodOptionsBase>();
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

public class PropertyOptions
{
    public PropertyOptions(PropertyInfo property)
    {
        Property = property;
    }

    public PropertyInfo Property { get; }

    //ResolutionStatus ResolutionStatus
    //AssignmentStrategyOptions? AssignmentStrategy

    public ResolutionOptions? ResolutionOptions { get; private set; }
}

public class ToPropertyOptions : ResolutionOptions
{
}

public class ToMethodOptions : ResolutionOptions
{
}