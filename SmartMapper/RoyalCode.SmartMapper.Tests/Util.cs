using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Core.Gererators;
using RoyalCode.SmartMapper.Mapping.Builders;
using RoyalCode.SmartMapper.Mapping.Resolvers;

namespace RoyalCode.SmartMapper.Tests;

internal static class Util
{
    internal static void PrepareAdapter<TSource, TTarget>(
        out MapperConfigurations configurations,
        out ActivationResolver activationResolver)
    {
        var mapperOptions = new MapperOptions();
        configurations = new MapperConfigurations(mapperOptions, new ExpressionGenerator());
        var options = mapperOptions.GetAdapterOptions<TSource, TTarget>();
        var adapterContext = AdapterResolver.Create(options);
        activationResolver = ActivationResolver.Create(adapterContext);
    }
    
    internal static void PrepareAdapter<TSource, TTarget>(
        Action<IAdapterBuilder<TSource, TTarget>> configure,
        out MapperConfigurations configurations,
        out ActivationResolver activationResolver)
    {
        var mapperOptions = new MapperOptions();
        configurations = new MapperConfigurations(mapperOptions, new ExpressionGenerator());
        configure(configurations.ConfigureAdapter<TSource, TTarget>());
        var options = mapperOptions.GetAdapterOptions<TSource, TTarget>();
        var adapterContext = AdapterResolver.Create(options);
        activationResolver = ActivationResolver.Create(adapterContext);
    }
    
    internal static void PrepareConfiguration(
        out MapperConfigurations configurations)
    {
        var mapperOptions = new MapperOptions();
        var expressionGenerator = new ExpressionGenerator();
        configurations = new MapperConfigurations(mapperOptions, expressionGenerator);
    }
}