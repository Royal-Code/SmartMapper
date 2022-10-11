using System.Linq.Expressions;
using RoyalCode.SmartMapper.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Core;
using RoyalCode.SmartMapper.Infrastructure.Naming;

namespace RoyalCode.SmartMapper.Infrastructure.Configurations;

/// <summary>
/// <para>
///     A builder for configure e create <see cref="ResolutionConfiguration"/>.
/// </para>
/// </summary>
public class ConfigurationBuilder
{
    private readonly MappingConfiguration mappingConfiguration = new();
    private readonly Converters converters = new();
    private readonly NameHandlers nameHandlers = new();
    private readonly Dictionary<Type, object> resolvers = new();
    private readonly Dictionary<Type, object> discoveries = new();

    internal ConfigurationBuilder() { }

    public static ConfigurationBuilder CreateDefault()
    {
        return new ConfigurationBuilder().Default();
    }
    
    public ResolutionConfiguration Build()
        => new(mappingConfiguration, converters, nameHandlers, resolvers, discoveries);

    /// <summary>
    /// Gets the object for mapping option settings.
    /// </summary>
    public IConfigure Configure => mappingConfiguration.Configure;

    public void AddConverter(MapKey key, ConvertOptions options) => converters.AddConverter(key, options);

    public void AddConverter(ConvertOptions options) =>
        AddConverter(new MapKey(options.SourceValueType, options.TargetValueType), options);

    public void AddConverter<TSourceType, TTargetType>(Expression<Func<TSourceType, TTargetType>> converter)
        => AddConverter(new ConvertOptions(typeof(TSourceType), typeof(TTargetType), converter));
    
    public void AddNameHandler(SourceNameHandler nameHandler) => nameHandlers.Add(nameHandler);

    public void AddNameHandler(TargetNameHandler nameHandler) => nameHandlers.Add(nameHandler);

    public void AddResolver<TResolver>(TResolver resolver)
        where TResolver : class
        => resolvers[typeof(TResolver)] = resolver;

    public void AddDiscovery<TDiscovery>(TDiscovery discovery) 
        where TDiscovery : class
        => discoveries[typeof(TDiscovery)] = discovery;
}