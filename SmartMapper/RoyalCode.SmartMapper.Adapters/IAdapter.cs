namespace RoyalCode.SmartMapper;

/// <summary>
/// <para>
///     A interface for mapping objects.
/// </para>
/// <para>
///     Adapters map the property values of a source object to a new target object, 
///     where a new instance of the target object is created.
/// </para>
/// <para>
///     The properties of the source object can be mapped as parameters of a constructor of the target object,
///     as method parameters, or to other properties of the target object.
/// </para>
/// </summary>
public interface IAdapter
{
    /// <summary>
    /// <para>
    ///     Map a source object to a target object.
    /// </para>
    /// </summary>
    /// <typeparam name="TTo">The target object type.</typeparam>
    /// <param name="from">
    ///     The source object.
    /// </param>
    /// <param name="type">
    ///     The type of the source object to avaliate the properties to be mapped.
    /// </param>
    /// <returns>
    ///     A new instance of the target object.
    /// </returns>
    TTo Map<TTo>(object from, Type? type = null);

    /// <summary>
    /// <para>
    ///     Map a source object to a target object.
    /// </para>
    /// </summary>
    /// <typeparam name="TFrom">
    ///     The type of the source object.
    /// </typeparam>
    /// <typeparam name="TTo">
    ///     The type of the target object.
    /// </typeparam>
    /// <param name="from">
    ///     The source object.
    /// </param>
    /// <returns>
    ///     A new instance of the target object.
    /// </returns>
    TTo Map<TFrom, TTo>(TFrom from);
}