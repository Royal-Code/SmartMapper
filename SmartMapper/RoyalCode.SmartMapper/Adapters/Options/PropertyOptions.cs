using System.Reflection;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;
using RoyalCode.SmartMapper.Adapters.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Options;

/// <summary>
/// <para>
///     Options for one property of the source object.
/// </para>
/// <para>
///     Contains the resolution for the mapping of the property.
/// </para>
/// </summary>
public class PropertyOptions
{
    /// <summary>
    /// Creates a new instance of <see cref="PropertyOptions"/>.
    /// </summary>
    /// <param name="property">The property to map.</param>
    public PropertyOptions(PropertyInfo property)
    {
        Property = property;
    }

    /// <summary>
    /// The source property to map.
    /// </summary>
    public PropertyInfo Property { get; }

    /// <summary>
    /// The resolution options between this source property to some member of the destination.
    /// </summary>
    public ResolutionOptionsBase? ResolutionOptions { get; private set; }

    /// <summary>
    /// Set the resolution options that resolved the property.
    /// </summary>
    /// <param name="resolutionOptionsBase"></param>
    internal void ResolvedBy(ResolutionOptionsBase resolutionOptionsBase)
    {
        if (ResolutionOptions is not null)
            throw new InvalidOperationException(
                $"The property '{Property.Name}' is already resolved by '{ResolutionOptions.GetType().Name}'");

        ResolutionOptions = resolutionOptionsBase;
    }

    /// <summary>
    /// <para>
    ///     Try get the <see cref="SourceOptions"/> of the inner properties.
    /// </para>
    /// <para>
    ///     The value will not be null if the <see cref="ResolutionOptions"/> is an instance of
    ///     <see cref="InnerPropertiesResolutionOptionsBase"/>. Otherwise, the value will be null.
    ///     The <see cref="ResolutionOptions"/> is set by the method <see cref="ResolvedBy"/>,
    ///     so the property must be resolved with some resolution that supports inner properties.
    /// </para>
    /// </summary>
    /// <returns></returns>
    internal SourceOptions? GetInnerPropertiesSourceOptions()
    {
        return ResolutionOptions is InnerPropertiesResolutionOptionsBase innerPropertiesResolutionOptions
            ? innerPropertiesResolutionOptions.InnerSourceOptions
            : null;
    }

    /////// <summary>
    /////// Sets the mapping of the property to be an method parameter.
    /////// </summary>
    /////// <param name="options">The options that configure the property to be mapped to a method parameter.</param>
    ////public void MappedToMethodParameter(ToMethodParameterOptions options)
    ////{
    ////    UpdateResolutionStatus(ResolutionStatus.MappedToMethodParameter);
    ////    ResolutionOptions = options;
    ////    options.ResolvedProperty = this;
    ////}

    /////// <summary>
    /////// Sets the mapping of the property to a constructor parameter.
    /////// </summary>
    /////// <param name="parameterOptions">The options that configure the property to be mapped to a constructor parameter.</param>
    ////public void MappedToConstructorParameter(ToConstructorParameterOptions parameterOptions)
    ////{
    ////    UpdateResolutionStatus(ResolutionStatus.MappedToConstructorParameter);
    ////    ResolutionOptions = parameterOptions;
    ////    parameterOptions.ResolvedProperty = this;
    ////}

    /////// <summary>
    /////// Sets the mapping of the property to a property of the destination object.
    /////// </summary>
    /////// <param name="toPropertyOptions">The options that configure the property to be mapped to a property of the destination object.</param>
    ////public void MappedToProperty(ToPropertyOptions toPropertyOptions)
    ////{
    ////    UpdateResolutionStatus(ResolutionStatus.MappedToProperty);
    ////    ResolutionOptions = toPropertyOptions;
    ////    toPropertyOptions.ResolvedProperty = this;
    ////}

    /////// <summary>
    /////// Sets the mapping of the property to a method of the destination object.
    /////// </summary>
    /////// <param name="methodOptions">The options that configure the property to be mapped to a method of the destination object.</param>
    ////public void MappedToMethod(ToMethodOptions methodOptions)
    ////{
    ////    UpdateResolutionStatus(ResolutionStatus.MappedToMethod);
    ////    ResolutionOptions = methodOptions;
    ////    methodOptions.ResolvedProperty = this;
    ////}

    /////// <summary>
    /////// Sets the mapping of the property to a constructor of the destination object.
    /////// </summary>
    /////// <param name="constructorOptions">The options that configure the property to be mapped to a constructor of the destination object.</param>
    ////public void MappedToConstructor(ToConstructorOptions constructorOptions)
    ////{
    ////    UpdateResolutionStatus(ResolutionStatus.MappedToConstructor);
    ////    ResolutionOptions = constructorOptions;
    ////    constructorOptions.ResolvedProperty = this;
    ////}

    ////public void ThenMappedTo(ThenToOptionsBase options)
    ////{
    ////    var status = options.Kind == ThenToKind.Property
    ////        ? ResolutionStatus.MappedToProperty
    ////        : ResolutionStatus.MappedToMethod;

    ////    UpdateThenToResolutionStatus(status);

    ////    var previousThenTo = ResolutionOptions as ThenToOptionsBase;
    ////    ResolutionOptions = options;
    ////    options.ResolvedProperty = this;

    ////    if (previousThenTo is not null)
    ////        options.Previous = previousThenTo;
    ////}

    ///// <summary>
    ///// Sets to ignore the mapping of the property.
    ///// </summary>
    //public void IgnoreMapping()
    //{
    //    UpdateResolutionStatus(ResolutionStatus.Ignored);
    //}

    ///// <summary>
    ///// <para>
    /////     Get o create the <see cref="AssignmentStrategyOptions{TProperty}"/>.
    ///// </para>
    ///// <para>
    /////     When the <see cref="AssignmentStrategy"/> is null or not for the <typeparamref name="TProperty"/> type,
    /////     a new instance of the <see cref="AssignmentStrategyOptions{TProperty}"/> is created.
    ///// </para>
    ///// </summary>
    ///// <typeparam name="TProperty">The type of the source property.</typeparam>
    ///// <returns>The <see cref="AssignmentStrategyOptions{TProperty}"/>.</returns>
    //public AssignmentStrategyOptions<TProperty> GetOrCreateAssignmentStrategyOptions<TProperty>()
    //{
    //    if (typeof(TProperty) != Property.PropertyType)
    //        throw new InvalidOperationException($"The type of the property '{Property.Name}' is not '{typeof(TProperty).Name}'");

    //    var strategyOptions = AssignmentStrategy as AssignmentStrategyOptions<TProperty>;
    //    if (strategyOptions is null)
    //    {
    //        strategyOptions = new AssignmentStrategyOptions<TProperty>();
    //        AssignmentStrategy = strategyOptions;
    //    }

    //    return strategyOptions;
    //}

    ////public ToConstructorOptions GetToConstructorOptionsResolution()
    ////{
    ////    if (ResolutionStatus != ResolutionStatus.MappedToConstructor)
    ////        throw new InvalidOperationException(
    ////            "To retrieve 'ToConstructorOptions' the status must be 'MappedToConstructor'" +
    ////            $" and the current status is '{ResolutionStatus}'");

    ////    return (ToConstructorOptions)ResolutionOptions!;
    ////}

    //private void UpdateResolutionStatus(ResolutionStatus status)
    //{
    //    if (ResolutionStatus == ResolutionStatus.Undefined)
    //    {
    //        ResolutionStatus = status;
    //        return;
    //    }

    //    if (ResolutionStatus != status)
    //        throw new InvalidOperationException(
    //            $"The resolution status of the property '{Property.Name}' is already set to '{ResolutionStatus}'" +
    //            $" and cannot be changed to '{status}'");
    //}

    //private void UpdateThenToResolutionStatus(ResolutionStatus status)
    //{
    //    if (ResolutionStatus == ResolutionStatus.MappedToProperty)
    //        switch (status)
    //        {
    //            case ResolutionStatus.MappedToProperty:
    //                return;
    //            case ResolutionStatus.MappedToMethod:
    //                ResolutionStatus = status;
    //                return;
    //        }

    //    throw new InvalidOperationException(
    //        $"The resolution status of the property '{Property.Name}' is '{ResolutionStatus}'" +
    //        $" and the 'then to' cannot change the resolution status to '{status}'");
    //}
}