using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace RoyalCode.SmartMapper;

public class SmartMapper
{

    public TTo Map<TFrom, TTo>(TFrom from)
    {
        throw new NotImplementedException();
    }

    public void Map<TSource, TTarget>(TSource source, TTarget target)
    {
        throw new NotImplementedException();
    }

    public TTo Select<TFrom, TTo>(TFrom from)
    {
        throw new NotImplementedException();
    }

    public void Select<TSource, TTarget>(TSource source, TTarget target)
    {
        throw new NotImplementedException();
    }
}

public interface IMapperConfiguration
{

}

public class MapperConfiguration
{
    private readonly Resolutions resolutions = new();

    public Expression<Func<TFrom, TTo>> GetAdapterExpression<TFrom, TTo>()
    {
        var expr = resolutions.TryGetAdapterExpression<TFrom, TTo>();
        if (expr is null)
        {
            expr = CreateAdapterExpression<TFrom, TTo>();
            expr = resolutions.TryAddAdapterExpression(expr);
        }
        return expr;
    }

    private Expression<Func<TFrom, TTo>> CreateAdapterExpression<TFrom, TTo>()
    {


    }
}

/// <summary>
/// <para>
///     A class to store all resolutions of type mappings.
/// </para>
/// </summary>
public class Resolutions
{
    private readonly ConcurrentDictionary<Tuple<Type, Type>, object> adapterExpressions = new();
    private readonly ConcurrentDictionary<Tuple<Type, Type>, object> resolutions = new();

    /// <summary>
    /// Obtém a instância de <see cref="ResolutionMap{TFrom, TTo}"/> para os tipos de origem e destino.
    /// </summary>
    /// <typeparam name="TFrom">Tipo de origem.</typeparam>
    /// <typeparam name="TTo">Tipo de destino.</typeparam>
    /// <returns>Mapa de resolução.</returns>
    private Resolution<TFrom, TTo> GetResolution<TFrom, TTo>()
    {
        var key = new Tuple<Type, Type>(typeof(TFrom), typeof(TTo));
        var map = resolutions.GetOrAdd(key, k => new Resolution<TFrom, TTo>());
        return (Resolution<TFrom, TTo>)map;
    }

    public Expression<Func<TFrom, TTo>>? TryGetAdapterExpression<TFrom, TTo>()
    {
        var key = new Tuple<Type, Type>(typeof(TFrom), typeof(TTo));

        return adapterExpressions.TryGetValue(key, out var value)
            ? (Expression<Func<TFrom, TTo>>)value
            : null;
    }

    public Expression<Func<TFrom, TTo>> TryAddAdapterExpression<TFrom, TTo>(Expression<Func<TFrom, TTo>> expr)
    {
        var key = new Tuple<Type, Type>(typeof(TFrom), typeof(TTo));

        return (Expression<Func<TFrom, TTo>>)adapterExpressions.GetOrAdd(key, expr);
    }
}

public class Resolution<TFrom, TTo>
{

}

public class AdapterResolver
{
    public void TryResolve(Type from, Type to, MapperConfiguration configuration)
    {
        var fromSourceProperties = FromSourceProperty.FromType(from);


    }

}

internal class FromSourceProperty
{
    private readonly PropertyInfo property;

    public static FromSourceProperty[] FromType(Type fromType)
    {
        return fromType.GetProperties().Where(p => p.CanRead)
            .Select(p => new FromSourceProperty(p))
            .ToArray();
    }

    public ResolutionState State { get; private set; }

    public FromSourceProperty(PropertyInfo property)
    {
        this.property = property;
    }

    
}

internal enum ResolutionState
{
    Pending,
    Ignore,
    Resolved
}