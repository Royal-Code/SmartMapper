namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

public class PropertyToConstructorOptions : InnerPropertiesOptionsBase
{
    public PropertyToConstructorOptions(Type propertyType, ConstructorOptions constructorOptions)
    {
        PropertyType = propertyType;
        ConstructorOptions = constructorOptions;
    }
    
    public Type PropertyType { get; }

    public ConstructorOptions ConstructorOptions { get; }
}