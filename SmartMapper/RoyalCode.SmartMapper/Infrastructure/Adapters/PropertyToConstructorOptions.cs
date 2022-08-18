namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

public class PropertyToConstructorOptions : ToParametersTargetRelatedOptionsBase
{
    public PropertyToConstructorOptions(Type propertyType, ConstructorOptions constructorOptions)
        : base(propertyType)
    {
        ConstructorOptions = constructorOptions;
    }
    
    public ConstructorOptions ConstructorOptions { get; }
}