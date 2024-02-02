namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

public class ToMethodOptions : ResolutionOptions
{
    public ToMethodOptions(PropertyOptions propertyOptions, MethodOptions methodOptions)
    {
        MethodOptions = methodOptions;
        SourceOptions = new SourceOptions(propertyOptions.Property.PropertyType);
    }
    
    public MethodOptions MethodOptions { get; }

    public SourceOptions SourceOptions { get; }
    
    public ToParametersStrategy Strategy { get; internal set; }
}