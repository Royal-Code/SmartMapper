using RoyalCode.SmartMapper.Configurations;

namespace RoyalCode.SmartMapper.Resolvers;

public class AdapterResolver
{
    public void TryResolve(Type from, Type to, MapOptions options)
    {
        var fromSourceProperties = FromSourceProperty.FromType(from);

        foreach (var sourceProperty in fromSourceProperties)
        {
            var propertyOptions = options.GetAdapterPropertyOptions(sourceProperty.Property);
            if (propertyOptions is not null && propertyOptions.Action != PropertyMapAction.Undefined)
                sourceProperty.UseOptions(propertyOptions);
        }
    }

}