using RoyalCode.SmartMapper.Adapters.Options;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Mapping.Builders.Internal;

internal sealed class ParameterBuilder<TProperty> : IParameterBuilder<TProperty>
{
    private readonly ResolutionOptionsBase resolutionOptions;

    public ParameterBuilder(ResolutionOptionsBase resolutionOptions)
    {
        this.resolutionOptions = resolutionOptions;
    }

    public IParameterBuilder<TProperty> Adapt()
    {
        var assigmentOptions = resolutionOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseAdapt();
        return this;
    }

    public IParameterBuilder<TProperty> CastValue()
    {
        var assigmentOptions = resolutionOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseCast();
        return this;
    }

    public IParameterBuilder<TProperty> Select()
    {
        var assigmentOptions = resolutionOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseSelect();
        return this;
    }

    public IParameterBuilder<TProperty> UseConverter<TParameter>(Expression<Func<TProperty, TParameter>> converter)
    {
        var assigmentOptions = resolutionOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseConverter(converter);
        return this;
    }
}
