namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

public class ThenToPropertyOptions : ThenToOptionsBase
{
    public ThenToPropertyOptions(ToPropertyOptions sourceProperty, ToPropertyOptions targetProperty) 
        : base(sourceProperty)
    {
        TargetProperty = targetProperty;
    }

    public ToPropertyOptions TargetProperty { get; }
    
    public override ThenToKind Kind => ThenToKind.Property;
}