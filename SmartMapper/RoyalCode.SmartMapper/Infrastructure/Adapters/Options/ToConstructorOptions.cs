namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

public class ToConstructorOptions : ResolutionOptions
{
    public ToConstructorOptions(PropertyOptions propertyOptions, ConstructorOptions constructorOptions)
    {
        ConstructorOptions = constructorOptions;
        SourceOptions = new SourceOptions(propertyOptions.Property.PropertyType);
    }
    
    public ConstructorOptions ConstructorOptions { get; }

    public SourceOptions SourceOptions { get; }
}