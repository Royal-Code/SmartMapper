using RoyalCode.SmartMapper.Configurations;
using RoyalCode.SmartMapper.Exceptions;

namespace RoyalCode.SmartMapper.Resolvers;

public class AdapterResolver
{
    public void TryResolve(Type from, Type to, IMapOptions options) // here, the options must exists for the key FromType, ToType and Adapter Kind !!!
    {
        var fromSourceProperties = FromSourceProperty.FromType(from);

        foreach (var sourceProperty in fromSourceProperties)
        {
            var propertyOptions = options.GetPropertyOptions(sourceProperty.Property);
            if (propertyOptions is not null && propertyOptions.TargetProperty != null)
            {
                if (propertyOptions.Action == PropertyMapAction.Undefined)
                    ResolveMapAction(propertyOptions);
                
                sourceProperty.UseOptions(propertyOptions);
            }
            
            ResolvePropertyMap(sourceProperty, to, options);
        }
        
        // every source property is resolved now,
        // so, it is time to create the expressions
    }

    /// <summary>
    /// Automatic resolve the property map.
    /// </summary>
    /// <param name="sourceProperty"></param>
    /// <param name="to"></param>
    /// <param name="options"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void ResolvePropertyMap(FromSourceProperty sourceProperty, Type to, IMapOptions options)
    {
        try
        {
            var propertyOptions = options.GetOrCreatePropertyOptions(sourceProperty.Property);
            propertyOptions.TargetProperty = to.SelectProperty(sourceProperty.Property.Name);
            ResolveMapAction(propertyOptions);
            
            sourceProperty.UseOptions(propertyOptions);
        }
        catch (Exception ex)
        {
            throw new UnmappablePropertyException(sourceProperty.Property, to, ex);
        }
    }

    /// <summary>
    /// Automatic resolve the action of map.
    /// </summary>
    /// <param name="propertyOptions"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void ResolveMapAction(PropertyOptions propertyOptions)
    {
        
        
        throw new NotImplementedException();
    }
}