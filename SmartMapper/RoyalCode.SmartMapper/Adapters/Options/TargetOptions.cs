using System.Linq.Expressions;
using System.Reflection;
using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;

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
    /// <param name="targetProperty">The target property.</param>
    /// <returns>A new instance of <see cref="ToTargetPropertyOptions"/> for the target type.</returns>
    public ToTargetPropertyOptions GetToTargetPropertyOptions(PropertyInfo targetProperty)
    {
        var options = toTargetPropertyOptions?.FirstOrDefault(p => p.TargetProperty == targetProperty);
        if (options is not null)
            return options;
        
        // validate if the property is a target property
        if (TargetType != targetProperty.DeclaringType)
            throw new ArgumentException(
                $"The property '{targetProperty.Name}' is not an inner property of the target property '{TargetType.Name}'.",
                nameof(targetProperty));
        
        options = new ToTargetPropertyOptions(this, targetProperty);
        toTargetPropertyOptions ??= [];
        toTargetPropertyOptions.Add(options);
        return options;
    }

    /// <summary>
    /// <para>
    ///     Gets or create the options for a property of the source type from a lambda expression.
    /// </para>
    /// </summary>
    /// <param name="propertySelector">
    ///     A lambda expression that get a property value, used to extract the property info.
    /// </param>
    /// <returns>
    ///     The options for the property of the source type or a new instance if no options have been set.
    /// </returns>
    /// <exception cref="InvalidPropertySelectorException">
    ///     If the lambda expression does not select a property.
    /// </exception>
    public ToTargetPropertyOptions GetToTargetPropertyOptions(LambdaExpression propertySelector)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        return GetToTargetPropertyOptions(propertyInfo);
    }

    /// <summary>
    /// <para>
    ///     Gets or create the options for a property of the source type from a lambda expression.
    /// </para>
    /// </summary>
    /// <param name="propertyName">The property name.</param>
    /// <param name="propertyType">The property type.</param>
    /// <returns></returns>
    /// <exception cref="InvalidPropertyNameException">
    ///     If the source type does not contains the a property with informed name.
    /// </exception>
    /// <exception cref="InvalidPropertyTypeException">
    ///     If the source property is not of the informed type.
    /// </exception>
    public ToTargetPropertyOptions GetToTargetPropertyOptions(string propertyName, Type propertyType)
    {
        var options = toTargetPropertyOptions?.FirstOrDefault(x => x.TargetProperty.Name == propertyName);
        if (options is not null)
            return options;

        // get target property by name, including inherited type properties
        var propertyInfo = TargetType.GetRuntimeProperty(propertyName);

        // check if property exists
        if (propertyInfo is null)
            throw new InvalidPropertyNameException(
                $"The type '{TargetType.Name}' does not have a property with name '{propertyName}'.",
                nameof(propertyName));

        // validate the property type
        if (propertyInfo.PropertyType != propertyType)
            throw new InvalidPropertyTypeException(
                $"Property '{propertyName}' on type '{TargetType.Name}' " +
                $"is not of type '{propertyType.Name}', " +
                $"but of type '{propertyInfo.PropertyType.Name}'.",
                nameof(propertyName));

        return GetToTargetPropertyOptions(propertyInfo);
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