using RoyalCode.SmartMapper.Configurations.Adapters;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

/// <inheritdoc />
public class AdapterPropertyToConstructorOptionsBuilder<TProperty> 
    : IAdapterPropertyToConstructorOptionsBuilder<TProperty>
{
    private readonly PropertyToConstructorOptions options;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="AdapterPropertyToConstructorOptionsBuilder{TProperty}"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public AdapterPropertyToConstructorOptionsBuilder(PropertyToConstructorOptions options)
    {
        this.options = options;
    }

    /// <inheritdoc />
    public void Parameters(Action<IAdapterPropertyToParametersOptionsBuilder<TProperty>> configureParameters)
    {
        configureParameters.Invoke(new AdapterPropertyToParametersOptionsBuilder<TProperty>(options));
    }
}