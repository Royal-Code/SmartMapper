using RoyalCode.SmartMapper.Infrastructure.Configurations;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// <para>
///     Extension methods for <see cref="IServiceCollection"/>.
/// </para>
/// </summary>
public static class SmartMapperServiceCollectionExtensions
{

    public static IServiceCollection AddSmartMapper(this IServiceCollection services,
        Action<ConfigurationBuilder>? configurer)
    {
        if (services == null) 
            throw new ArgumentNullException(nameof(services));

        var builder = services.TryAddSmartMapperInternal();
        
        
        
        configurer?.Invoke(builder);
        
        return services;
    }

    private static ConfigurationBuilder TryAddSmartMapperInternal(this IServiceCollection services)
    {
        ConfigurationBuilder builder;
        
        var description = services.SingleOrDefault(
            d => d.ServiceType == typeof(ConfigurationBuilder) && d.ImplementationInstance is not null);
        
        if (description is null)
        {
            builder = new ConfigurationBuilder();
            ConfigureDefault(services, builder);
        }
        else
        {
            builder = (ConfigurationBuilder)description.ImplementationInstance!;    
        }

        return builder;
    }

    /// <summary>
    /// <para>
    ///     Adds default services and default configurations.
    /// </para>
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="builder">The configuration builder.</param>
    private static void ConfigureDefault(IServiceCollection services, ConfigurationBuilder builder)
    {
        services.AddSingleton(builder);
        services.AddSingleton(sp => sp.GetRequiredService<ConfigurationBuilder>().Build());

        builder.Default();
    }
}