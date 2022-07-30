using System.Linq.Expressions;
using RoyalCode.SmartMapper.Configurations.Adapters;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Builders;

public class AdapterConstructorParametersOptionsBuilder<TSource> : IAdapterConstructorParametersOptionsBuilder<TSource>
{
    private readonly AdapterOptions options;
    private readonly ConstructorOptions constructorOptions;

    public AdapterConstructorParametersOptionsBuilder(AdapterOptions options, ConstructorOptions constructorOptions)
    {
        this.options = options;
        this.constructorOptions = constructorOptions;
    }
    
    public IAdapterParameterStrategyBuilder<TSource, TProperty> Parameter<TProperty>(
        Expression<Func<TSource, TProperty>> propertySelector,
        string? parameterName = null)
    {
        throw new NotImplementedException();
    }
}