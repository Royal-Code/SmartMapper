using System.Reflection;

namespace RoyalCode.SmartMapper.Configurations;

[Obsolete]
public interface IMapOptions
{
    public PropertyOptions? GetPropertyOptions(PropertyInfo property);

    public PropertyOptions GetOrCreatePropertyOptions(PropertyInfo property);
}

[Obsolete]
public class MapOptions : IMapOptions
{
    private Dictionary<PropertyInfo, PropertyOptions>? propertyOptions;

    public PropertyOptions? GetPropertyOptions(PropertyInfo property)
    {
        return propertyOptions is null
            ? null
            : propertyOptions.ContainsKey(property)
                ? propertyOptions[property]
                : null;
    }

    public PropertyOptions GetOrCreatePropertyOptions(PropertyInfo property)
    {
        var options = GetPropertyOptions(property);
        if (options is null)
        {
            options = new PropertyOptions(property);
            propertyOptions ??= new();
            propertyOptions.Add(property, options);
        }

        return options;
    }
}