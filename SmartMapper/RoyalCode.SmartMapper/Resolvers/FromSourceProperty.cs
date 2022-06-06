using System.Reflection;
using RoyalCode.SmartMapper.Configurations;

namespace RoyalCode.SmartMapper.Resolvers;

internal class FromSourceProperty
{
    public static FromSourceProperty[] FromType(Type fromType)
    {
        return fromType.GetProperties().Where(p => p.CanRead)
            .Select(p => new FromSourceProperty(p))
            .ToArray();
    }

    public PropertyInfo Property { get; }
    
    public ResolutionState State { get; private set; }

    public PropertyOptions? Options { get; private set; }

    public FromSourceProperty(PropertyInfo property)
    {
        Property = property;
    }

    public void UseOptions(PropertyOptions propertyOptions)
    {
        Options = propertyOptions;
        State = propertyOptions.Action == PropertyMapAction.Ignore
            ? ResolutionState.Ignore
            : ResolutionState.Resolved;
    }
}