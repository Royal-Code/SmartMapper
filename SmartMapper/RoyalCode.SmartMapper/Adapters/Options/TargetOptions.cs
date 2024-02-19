using System.Reflection;
using RoyalCode.SmartMapper.Core.Exceptions;

namespace RoyalCode.SmartMapper.Adapters.Options;

/// <summary>
/// <para>
///     Contains options for configured for mapping of a target type.
/// </para>
/// </summary>
public sealed class TargetOptions
{
    private ConstructorOptions? constructorOptions;
    private ICollection<MethodOptions>? methodOptions;
    private ICollection<ToTargetPropertyOptions>? toTargetPropertyOptions;
    
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
    ///     Create a new instance of <see cref="ToTargetPropertyOptions"/> for the target type.
    /// </para>
    /// </summary>
    /// <param name="sourcePropertyOptions">The source property options.</param>
    /// <param name="targetProperty">The target property.</param>
    /// <returns>A new instance of <see cref="ToTargetPropertyOptions"/> for the target type.</returns>
    public ToTargetPropertyOptions GetOrCreateToTargetPropertyOptions(
        PropertyOptions sourcePropertyOptions,
        PropertyInfo targetProperty)
    {
        var options = toTargetPropertyOptions?.FirstOrDefault(p => p.TargetProperty == targetProperty);
        if (options is not null)
            return options;
        
        // validate if the property is a target property
        if (TargetType != targetProperty.DeclaringType)
            throw new ArgumentException(
                $"The property '{targetProperty.Name}' is not an inner property of the target property '{TargetType.Name}'.",
                nameof(targetProperty));
        
        options = new ToTargetPropertyOptions(this, targetProperty, sourcePropertyOptions);
        toTargetPropertyOptions ??= [];
        toTargetPropertyOptions.Add(options);
        return options;
    }

    /// <summary>
    /// <para>
    ///     Create a new instance of <see cref="ToTargetPropertyOptions"/> for the target type.
    /// </para>
    /// </summary>
    /// <param name="sourcePropertyOptions">The source property options.</param>
    /// <param name="targetPropertyName">The name of the target property.</param>
    /// <typeparam name="TTargetProperty">The type of the target property.</typeparam>
    /// <returns>A new instance of <see cref="ToTargetPropertyOptions"/> for the target type.</returns>
    /// <exception cref="InvalidPropertyNameException">
    ///     Check if the property exists on the target type.
    /// </exception>
    /// <exception cref="InvalidPropertyTypeException">
    ///     Check if the property type is the same as the target property type.
    /// </exception>
    public ToTargetPropertyOptions GetOrCreateToTargetPropertyOptions<TTargetProperty>(
        PropertyOptions sourcePropertyOptions,
        string targetPropertyName)
    {
        // get target property by name, including inherited type properties
        var propertyInfo = TargetType.GetRuntimeProperty(targetPropertyName);

        // check if property exists
        if (propertyInfo is null)
            throw new InvalidPropertyNameException(
                $"The type '{TargetType.Name}' does not have a property with name '{targetPropertyName}'.",
                nameof(targetPropertyName));

        // validate the property type
        if (propertyInfo.PropertyType != typeof(TTargetProperty))
            throw new InvalidPropertyTypeException(
                $"Property '{targetPropertyName}' on type '{TargetType.Name}' " +
                $"is not of type '{typeof(TTargetProperty).Name}', " +
                $"but of type '{propertyInfo.PropertyType.Name}'.",
                nameof(targetPropertyName));
        
        return GetOrCreateToTargetPropertyOptions(sourcePropertyOptions, propertyInfo);
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