using RoyalCode.SmartMapper.Adapters.Options.Resolutions;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Adapters.Configurations.Internal;

internal sealed class ToParameterOptionsBuilder<TProperty> : IToParameterOptionsBuilder<TProperty>
{
    private readonly ToParameterResolutionOptions resolutionOptions;

    public ToParameterOptionsBuilder(ToParameterResolutionOptions resolutionOptions)
    {
        this.resolutionOptions = resolutionOptions;
    }

    public IToParameterOptionsBuilder<TProperty> Adapt()
    {
        var assigmentOptions = resolutionOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseAdapt();
        return this;
    }

    public IToParameterOptionsBuilder<TProperty> CastValue()
    {
        var assigmentOptions = resolutionOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseCast();
        return this;
    }

    public IToParameterOptionsBuilder<TProperty> Select()
    {
        var assigmentOptions = resolutionOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseSelect();
        return this;
    }

    public IToParameterOptionsBuilder<TProperty> UseConverter<TParameter>(Expression<Func<TProperty, TParameter>> converter)
    {
        var assigmentOptions = resolutionOptions.GetOrCreateAssignmentStrategyOptions<TProperty>();
        assigmentOptions.UseConvert(converter);
        return this;
    }
}
