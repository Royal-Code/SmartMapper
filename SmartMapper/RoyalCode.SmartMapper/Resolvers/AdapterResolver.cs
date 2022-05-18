using RoyalCode.SmartMapper.Configurations;

namespace RoyalCode.SmartMapper.Resolvers;

public class AdapterResolver
{
    public void TryResolve(Type from, Type to, IMapOptions options) // here, the options must exists for the key FromType, ToType and Adapter Kind !!!
    {
        var fromSourceProperties = FromSourceProperty.FromType(from);

        foreach (var sourceProperty in fromSourceProperties)
        {
            var propertyOptions = options.GetPropertyOptions(sourceProperty.Property);
            if (propertyOptions is not null && propertyOptions.Action != PropertyMapAction.Undefined)
                sourceProperty.UseOptions(propertyOptions);

            // automatic resolve the
        }
    }

}