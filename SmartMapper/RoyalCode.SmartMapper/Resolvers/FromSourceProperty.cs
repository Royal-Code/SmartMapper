using System.Reflection;
using RoyalCode.SmartMapper.Configurations;
using RoyalCode.SmartMapper.Resolvers;

namespace RoyalCode.SmartMapper;

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

    public FromSourceProperty(PropertyInfo property)
    {
        Property = property;
    }

    public void UseOptions(PropertyOptions propertyOptions)
    {
        throw new NotImplementedException();
    }
}