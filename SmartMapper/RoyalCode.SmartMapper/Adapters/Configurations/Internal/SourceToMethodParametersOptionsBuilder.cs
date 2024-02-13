using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Core.Extensions;
using System.Linq.Expressions;
using System.Reflection;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

internal sealed class SourceToMethodParametersOptionsBuilder<TSource> : ISourceToMethodParametersOptionsBuilder<TSource>
{
    private readonly AdapterOptions adapterOptions;
    private readonly SourceToMethodOptions sourceToMethodOptions;

    public SourceToMethodParametersOptionsBuilder(
        AdapterOptions adapterOptions,
        SourceToMethodOptions sourceToMethodOptions)
    {
        this.adapterOptions = adapterOptions;
        this.sourceToMethodOptions = sourceToMethodOptions;
    }

    public void Ignore<TProperty>(Expression<Func<TSource, TProperty>> propertySelector)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(propertyInfo);

        propertyOptions.IgnoreMapping();
    }

    public IToParameterOptionsBuilder<TProperty> Parameter<TProperty>(
        Expression<Func<TSource, TProperty>> propertySelector)
    {
        if (!propertySelector.TryGetMember(out var member))
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        if (member is not PropertyInfo propertyInfo)
            throw new InvalidPropertySelectorException(nameof(propertySelector));

        var propertyOptions = adapterOptions.SourceOptions.GetPropertyOptions(propertyInfo);

        var resolution = sourceToMethodOptions.AddPropertyToParameterSequence(propertyOptions);

        return new ToParameterOptionsBuilder<TProperty>(resolution);
    }
}
