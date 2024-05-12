using RoyalCode.SmartMapper.Adapters.Configurations;
using RoyalCode.SmartMapper.Adapters.Resolutions.Contexts;
using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Core.Gererators;
using RoyalCode.SmartMapper.Core.Options;

namespace RoyalCode.SmartMapper.Tests;

internal static class Util
{
    internal static void PrepareAdapter<TSource, TTarget>(
        out MapperConfigurations configurations,
        out ActivationContext activationContext)
    {
        var mapperOptions = new MapperOptions();
        configurations = new MapperConfigurations(mapperOptions, new ExpressionGenerator());
        var options = mapperOptions.GetAdapterOptions<TSource, TTarget>();
        var adapterContext = AdapterContext.Create(options);
        activationContext = ActivationContext.Create(adapterContext);
    }
    
    internal static void PrepareAdapter<TSource, TTarget>(
        Action<IAdapterOptionsBuilder<TSource, TTarget>> configure,
        out MapperConfigurations configurations,
        out ActivationContext activationContext)
    {
        var mapperOptions = new MapperOptions();
        configurations = new MapperConfigurations(mapperOptions, new ExpressionGenerator());
        configure(configurations.ConfigureAdapter<TSource, TTarget>());
        var options = mapperOptions.GetAdapterOptions<TSource, TTarget>();
        var adapterContext = AdapterContext.Create(options);
        activationContext = ActivationContext.Create(adapterContext);
    }
    
    internal static void PrepareConfiguration(
        out MapperConfigurations configurations)
    {
        var mapperOptions = new MapperOptions();
        var expressionGenerator = new ExpressionGenerator();
        configurations = new MapperConfigurations(mapperOptions, expressionGenerator);
    }
}