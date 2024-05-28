using System.Reflection;
using RoyalCode.SmartMapper.Adapters.Resolutions;
using RoyalCode.SmartMapper.Mapping.Options;

namespace RoyalCode.SmartMapper.Adapters.Options.Resolutions;

/// <summary>
/// Options that contains the resolution of a property to be mapped to target method.
/// </summary>
public sealed class ToMethodResolutionOptions : InnerPropertiesResolutionOptionsBase
{
    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="ToMethodResolutionOptions"/>.
    /// </para>
    /// <para>
    ///     Call <see cref="PropertyOptions.ResolvedBy(ResolutionOptionsBase)"/> to add the current instance
    ///     as a resolution of the source property.
    /// </para>
    /// </summary>
    /// <param name="methodOptions">The method options mapped by the property.</param>
    /// <param name="resolvedProperty">The property options that will be resolved by the method.</param>
    /// <returns>A new instance of <see cref="ToMethodResolutionOptions"/>.</returns>
    public static ToMethodResolutionOptions Resolvers(MethodOptions methodOptions, PropertyOptions resolvedProperty)
    {
        return new(methodOptions, resolvedProperty);
    }
    
    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="ToMethodResolutionOptions"/>.
    /// </para>
    /// <para>
    ///     Call <see cref="PropertyOptions.ResolvedBy(ResolutionOptionsBase)"/> to add the current instance
    ///     as a resolution of the source property.
    /// </para>
    /// </summary>
    /// <param name="methodOptions"></param>
    /// <param name="resolvedProperty"></param>
    private ToMethodResolutionOptions(MethodOptions methodOptions, PropertyOptions resolvedProperty) : base(resolvedProperty)
    {
        Status = ResolutionStatus.MappedToMethod;
        MethodOptions = methodOptions;
    }

    /// <summary>
    /// The method options mapped by the property.
    /// </summary>
    public MethodOptions MethodOptions { get; }
    
    /// <summary>
    /// <para>
    ///     Verify if this method resolution can accept a method.
    /// </para>
    /// <para>
    ///     The method can be accepted if the <see cref="MethodOptions.Method"/> is the same as the method
    ///     or if the <see cref="MethodOptions.MethodName"/> is the same as the method name.
    /// </para>
    /// <para>
    ///     If the method options not define any method, the method is accepted.
    /// </para>
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    public bool CanAcceptMethod(MethodInfo method)
    {
        if (MethodOptions.Method is not null)
            return MethodOptions.Method == method;
        
        if (MethodOptions.MethodName is not null)
            return MethodOptions.MethodName == method.Name;

        return true;
    }
}
