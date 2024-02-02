
using System.Linq.Expressions;
using RoyalCode.SmartMapper.Adapters.Resolutions;
using RoyalCode.SmartMapper.Core.Exceptions;

namespace RoyalCode.SmartMapper.Core.Gererators;

/// <summary>
/// <para>
///     A component that generates the expression trees for adapters, selectors, and mappers.
/// </para>
/// </summary>
public sealed class ExpressionGenerator
{
    /// <summary>
    /// Generate an expression tree for an adapter from the given adapter resolution.
    /// </summary>
    /// <typeparam name="TSource">The source type of the adapter.</typeparam>
    /// <typeparam name="TTarget">The target type of the adapter.</typeparam>
    /// <param name="adapterResolution">The resolution of the adapter.</param>
    /// <returns>
    ///     The expression tree for the adapter.
    /// </returns>
    /// <exception cref="GenerationException">
    ///     When the expression tree could not be generated.
    /// </exception>
    public Expression<Func<TSource, TTarget>> CreateAdapterExpression<TSource, TTarget>(AdapterResolution adapterResolution)
    {
        throw new NotImplementedException();
    }
}
