using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RefactorOptions;

public class ToPropertyOptions : ResolutionOptions
{
    public ToPropertyOptions(Type targetType, PropertyInfo targetProperty)
    {
        TargetType = targetType;
        TargetProperty = targetProperty;
    }
    
    public Type TargetType { get; }
    
    public PropertyInfo TargetProperty { get; }

    public ThenToPropertyOptions ThenToProperty(PropertyInfo propertyInfo)
    {
        if (ResolvedProperty is null)
            throw new InvalidOperationException("The 'ResolvedProperty' is not assigned");
        
        var toProperty = new ToPropertyOptions(TargetProperty.PropertyType, propertyInfo);
        var target = new TargetOptions(TargetProperty.PropertyType);
        var thenToProperty = new ThenToPropertyOptions(target, toProperty);
        
        var then = new ThenOptions(this, thenToProperty);
        ResolvedProperty.ThenMappedTo(thenToProperty);

        return thenToProperty;
    }
}


// TODO: Rever relacionamentos entre o ThenOptions, ToPropertyOptions e ThenToOptionsBase.
public class ThenOptions
{
    public ThenOptions(ToPropertyOptions previousProperty, ThenToPropertyOptions thenTo)
    {
        Kind = ThenToKind.Property;
        PreviousProperty = previousProperty;
        ThenTo = thenTo;
        thenTo.Then = this;
    }
    
    public ThenOptions(ToPropertyOptions previousProperty, ThenToMethodOptions thenTo)
    {
        Kind = ThenToKind.Method;
        PreviousProperty = previousProperty;
        ThenTo = thenTo;
        thenTo.Then = this;
    }
    
    public ThenToKind Kind { get; }

    public ToPropertyOptions PreviousProperty { get; }
    
    public ThenToOptionsBase ThenTo { get; }
}

public abstract class ThenToOptionsBase : ResolutionOptions
{
    protected ThenToOptionsBase(TargetOptions targetOptions)
    {
        TargetOptions = targetOptions;
    }
    
    public TargetOptions TargetOptions { get; }

    public ThenOptions Then { get; internal set; }
    
    public ThenToOptionsBase? Previous { get; internal set; }
}

public class ThenToPropertyOptions : ThenToOptionsBase
{
    public ThenToPropertyOptions(TargetOptions targetOptions, ToPropertyOptions toPropertyOptions)
        : base(targetOptions)
    {
        ToPropertyOptions = toPropertyOptions;
    }
    
    public ToPropertyOptions ToPropertyOptions { get; }
}

public class ThenToMethodOptions : ThenToOptionsBase
{
    public ThenToMethodOptions(TargetOptions targetOptions, ToMethodOptions toMethodOptions)
        : base(targetOptions)
    {
        ToMethodOptions = toMethodOptions;
    }
    
    public ToMethodOptions ToMethodOptions { get; }
}

public class ToMethodOptions : ResolutionOptions
{
}

public class ToConstructorOptions : ResolutionOptions
{
}

public class ToTargetOptions : ResolutionOptions
{
}