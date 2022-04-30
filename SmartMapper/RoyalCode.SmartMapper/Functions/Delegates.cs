
namespace RoyalCode.SmartMapper.Functions;

/// <summary>
/// <para>
///     Delegate to map property values from a <typeparamref name="TFrom"/> object
///     to a new object of <typeparamref name="TTo"/>.
/// </para>
/// </summary>
/// <typeparam name="TFrom">The input type from which the properties will be read.</typeparam>
/// <typeparam name="TTo">The output type that receives the values from the input type.</typeparam>
/// <param name="from">Input instance.</param>
/// <returns>Output instance</returns>
public delegate TTo Adapter<in TFrom, out TTo>(TFrom from);

/// <summary>
/// <para>
///     Delegate to map property values from a <typeparamref name="TSource"/> object
///     to a <typeparamref name="TTarget"/> object.
/// </para>
/// </summary>
/// <typeparam name="TSource">The source type from which the properties will be read.</typeparam>
/// <typeparam name="TTarget">The target type to which the properties will be write.</typeparam>
/// <param name="source">Instance to get properties values.</param>
/// <param name="target">Instance to set properties values.</param>
public delegate void Setter<in TSource, in TTarget>(TSource source, TTarget target);


public delegate TTo Selector<in TFrom, out TTo>(TFrom from);