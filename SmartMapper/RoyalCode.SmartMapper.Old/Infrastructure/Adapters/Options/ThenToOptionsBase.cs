namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

public abstract class ThenToOptionsBase : ResolutionOptions
{
    protected ThenToOptionsBase(ToPropertyOptions sourceProperty)
    {
        SourceProperty = sourceProperty;
        TargetOptions = new TargetOptions(sourceProperty.TargetProperty.PropertyType);
    }
    
    public abstract ThenToKind Kind { get; }
    
    public ToPropertyOptions SourceProperty { get; }
    
    public TargetOptions TargetOptions { get; }
    
    public ThenToOptionsBase? Previous { get; internal set; }
}