using RoyalCode.SmartMapper.Configurations;
using RoyalCode.SmartMapper.Exceptions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Resolvers;

public class AdapterResolver
{
    public void TryResolve(Type from, Type to, AdapterOptions options)
        // here, the options must exists for the key FromType, ToType and Adapter Kind !!!
    {
        var fromSourceProperties = FromSourceProperty.FromType(from);

        // full refactor required!
        
        foreach (var sourceProperty in fromSourceProperties)
        {
            if (options.SourceOptions.TryGetPropertyOptions(sourceProperty.Property.Name, out var propertyOptions))
            {
                if (propertyOptions.ResolutionStatus is not ResolutionStatus.Undefined)
                    sourceProperty.UseOptions(propertyOptions);
                else
                    ResolvePropertyMap(sourceProperty, to, options);
            }
            else
            {
                ResolveMapAction(propertyOptions);
            }
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
    private void ResolvePropertyMap(FromSourceProperty sourceProperty, Type to, AdapterOptions options)
    {
        try
        {
            var propertyOptions = options.SourceOptions.GetPropertyOptions(sourceProperty.Property);
            
            // TODO: must select and if selected must configure the right options.
            //propertyOptions.TargetProperty = to.SelectProperty(sourceProperty.Property.Name);
            
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