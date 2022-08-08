namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

public class PropertyToConstructorOptions : InnerPropertiesOptionsBase
{
    public PropertyToConstructorOptions(Type propertyType, Type target, ConstructorOptions constructorOptions)
    {
        PropertyType = propertyType;
        Target = target;
        ConstructorOptions = constructorOptions;
    }
    
    public Type PropertyType { get; }
    
    public Type Target { get; }
    
    public ConstructorOptions ConstructorOptions { get; }
}