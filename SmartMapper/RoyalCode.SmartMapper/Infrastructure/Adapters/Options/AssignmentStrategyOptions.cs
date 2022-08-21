using System.Linq.Expressions;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

public class AssignmentStrategyOptions : OptionsBase
{
    public AssignmentStrategyOptions()
    {
        Strategy = ValueAssignmentStrategy.Undefined;
    }

    public ValueAssignmentStrategy Strategy { get; internal set; }

    public void UseCast()
    {
        Strategy = ValueAssignmentStrategy.Cast;
    }

    public void UseAdapt()
    {
        Strategy = ValueAssignmentStrategy.Adapt;
    }

    public void UseSelect()
    {
        Strategy = ValueAssignmentStrategy.Select;
    }
}

public class AssignmentStrategyOptions<TProperty> : AssignmentStrategyOptions
{
    public void UseConvert<TParameter>(Expression<Func<TProperty, TParameter>> converter)
    {
        Strategy = ValueAssignmentStrategy.Convert;
        
        var convertOptions = new ConvertOptions(typeof(TProperty), typeof(TParameter), converter);
        SetAnnotation<ConvertOptions>(convertOptions);
    }
    
    public void UseProcessor<TService, TParameter>(
        Expression<Func<TService, TProperty, TParameter>> valueProcessor)
    {
        Strategy = ValueAssignmentStrategy.Processor;
        
        var processorOptions = new ProcessorOptions(typeof(TProperty), typeof(TParameter), typeof(TService), valueProcessor);
        SetAnnotation<ProcessorOptions>(processorOptions);
    }
}