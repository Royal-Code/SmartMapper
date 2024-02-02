namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

public class ThenToMethodOptions : ThenToOptionsBase
{
    public ThenToMethodOptions(ToPropertyOptions sourceOptions, ToMethodOptions toMethodOptions)
        : base(sourceOptions)
    {
        ToMethodOptions = toMethodOptions;
        TargetOptions.AddToMethod(toMethodOptions.MethodOptions);
    }
    
    public ToMethodOptions ToMethodOptions { get; }

    public override ThenToKind Kind => ThenToKind.Method;
}