using System.Reflection;
using RoyalCode.SmartMapper.Configurations;

namespace RoyalCode.SmartMapper.Resolvers;

/// <summary>
/// <para>
///     This class must resolve the mapping of a property of a source type to a method of the target type. 
/// </para>
/// </summary>
public class PropertyToMethodResolver
{

    public void Resolve(PropertyToMethodOptions options, Type targetType)
    {
        
    }
}

public class PropertyToParameterResolver
{

    public void Resolver(PropertyToMethodOptions options, IEnumerable<PropertyInfo> properties)
    {
        var method = options.TargetMethod 
            ?? throw new ArgumentException("Target method not resolved", nameof(options));

        foreach (var property in properties)
        {
            PropertyToParameterOptions parameterOptions;
            // for each property, try read the PropertyToParameterOptions from the options,
            // if exists, check if the parameter was specified and try resolve the parameter type.
            // otherwise, try find a parameter and then resolve the type.
        }
    }
}