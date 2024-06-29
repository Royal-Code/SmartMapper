using System.Reflection;

namespace RoyalCode.SmartMapper.Mapping.Resolvers.Items;

/// <summary>
/// A container for target properties.
/// </summary>
public sealed class TargetProperty : TargetBase
{
    private IReadOnlyCollection<TargetProperty>? innerProperties;
    private IReadOnlyCollection<TargetMethod>? innerMethods;
    
    /// <summary>
    /// Creates a list of <see cref="TargetProperty"/> of a target type.
    /// </summary>
    /// <param name="targetType">The target type.</param>
    /// <returns></returns>
    public static IReadOnlyCollection<TargetProperty> Create(Type targetType)
    {
        List<TargetProperty> list = [];
        var properties = targetType.GetTypeInfo()
            .GetRuntimeProperties()
            .Where(p => p is { CanWrite: true });

        foreach(var property in properties)
        {
            list.Add(new TargetProperty(property));
        }

        return list;
    }

    /// <summary>
    /// Creates a new <see cref="TargetProperty"/>.
    /// </summary>
    /// <param name="propertyInfo">The property.</param>
    public TargetProperty(PropertyInfo propertyInfo)
    {
        PropertyInfo = propertyInfo;
    }

    /// <summary>
    /// The property.
    /// </summary>
    public PropertyInfo PropertyInfo { get; }
    
    /// <summary>
    /// <para>
    ///     The inner methods of the property.
    /// </para>
    /// </summary>
    public IReadOnlyCollection<TargetMethod> InnerMethods => innerMethods ??= TargetMethod.Create(PropertyInfo.PropertyType);
    
    /// <summary>
    /// <para>
    ///     The inner properties of the property.
    /// </para>
    /// </summary>
    public IReadOnlyCollection<TargetProperty> InnerProperties => innerProperties ??= Create(PropertyInfo.PropertyType);
}
