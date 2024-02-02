using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

/// <inheritdoc />
public class AdapterPropertyToConstructorOptionsBuilder<TProperty> 
    : IAdapterPropertyToConstructorOptionsBuilder<TProperty>
{
    private readonly ToConstructorOptions options;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="AdapterPropertyToConstructorOptionsBuilder{TProperty}"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public AdapterPropertyToConstructorOptionsBuilder(ToConstructorOptions options)
    {
        this.options = options;
    }

    /// <inheritdoc />
    public void Parameters(Action<IAdapterPropertyToParametersOptionsBuilder<TProperty>> configureParameters)
    {
        var builder = new AdapterPropertyToConstructorParametersOptionsBuilder<TProperty>(
            options.SourceOptions,
            options.ConstructorOptions);
        
        configureParameters.Invoke(builder);
    }
}