
using FastExpressionCompiler;
using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Resolutions;
using RoyalCore.SmartMapper.Resolutions;
using System;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Adapters;

/// <summary>
/// Default implementation of <see cref="IAdapter"/>.
/// </summary>
public sealed class DefaultAdapter : IAdapter
{
    private readonly MapperConfigurations configurations;

    public DefaultAdapter(MapperConfigurations configurations)
    {
        this.configurations = configurations;
    }

    /// <inheritdoc />
    public TTo Map<TFrom, TTo>(TFrom from)
    {
        return configurations.GetAdapter<TFrom, TTo>()(from);
    }

    /// <inheritdoc />
    public TTo Map<TTo>(object from, Type? type = null)
    {
        throw new NotImplementedException();
    }

    
}

/// <summary>
/// <para>
///     All the configurations for the mapper, including the adapters, selectors, and other components for
///     creating the mappings.
/// </para>
/// </summary>
public class MapperConfigurations
{
    private readonly ResolutionsMap resolutionsMap = new();
    private readonly MapperOptions options;
    private readonly ResolutionFactory resolutionFactory;
    private readonly ExpressionGenerator expressionGenerator;

    /// <summary>
    /// Get the function that adapts from <typeparamref name="TFrom"/> to <typeparamref name="TTarget"/>.
    /// </summary>
    /// <typeparam name="TFrom"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    /// <returns></returns>
    public Func<TFrom, TTarget> GetAdapter<TFrom, TTarget>()
    {
        return resolutionsMap.GetAdapter<TFrom, TTarget>()
            ??  CreateAdapter<TFrom, TTarget>();
    }

    private Func<TFrom, TTarget> CreateAdapter<TFrom, TTarget>()
    {
        var expression = resolutionsMap.GetAdapterExpression<TFrom, TTarget>();
        if (expression == null)
        {
            AdapterOptions adapterOptions = options.GetAdapterOptions<TFrom, TTarget>();
            AdapterResolution resolution = resolutionFactory.CreateAdapterResolution(adapterOptions);
            expression = expressionGenerator.CreateAdapterExpression<TFrom, TTarget>(resolution);
            resolutionsMap.AddAdapterExpression(expression);
        }

        Func<TFrom, TTarget> function = expression.CompileFast();
        resolutionsMap.AddAdapter(function);
        return function;
    }
}


internal sealed class ExpressionGenerator
{
    internal Expression<Func<TFrom, TTo>> CreateAdapterExpression<TFrom, TTo>(AdapterResolution adapterResolution)
    {
        throw new NotImplementedException();
    }
}

internal sealed class ResolutionFactory
{
    internal AdapterResolution CreateAdapterResolution(AdapterOptions adapterOptions)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// <para>
///     Contains all the options for a single mapping between a source and target type.
/// </para>
/// <para>
///     Holds options for adapters, selectors, and mappers.
/// </para>
/// </summary>
public sealed class MapperOptions
{
    private readonly Dictionary<(Type, Type), AdapterOptions> adapterOptions = [];

    internal AdapterOptions GetAdapterOptions<TFrom, TTarget>()
    {
        if (adapterOptions.TryGetValue((typeof(TFrom), typeof(TTarget)), out var options))
        {
            return options;
        }

        options = new AdapterOptions(typeof(TFrom), typeof(TTarget));
        adapterOptions.Add((typeof(TFrom), typeof(TTarget)), options);
        return options;
    }
}

