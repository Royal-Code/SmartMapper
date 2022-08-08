using RoyalCode.SmartMapper.Configurations.Adapters;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

/// <inheritdoc />
public class AdapterPropertyToConstructorOptionsBuilder<TSource, TProperty> 
    : IAdapterPropertyToConstructorOptionsBuilder<TSource, TProperty>
{
    private readonly PropertyToConstructorOptions options;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="AdapterPropertyToConstructorOptionsBuilder{TSource, TProperty}"/> class.
    /// </summary>
    /// <param name="options"></param>
    public AdapterPropertyToConstructorOptionsBuilder(PropertyToConstructorOptions options)
    {
        this.options = options;
    }

    /// <inheritdoc />
    public void Parameters(Action<IAdapterPropertyToParametersOptionsBuilder<TSource, TProperty>> configureParameters)
    {
        
    }
}