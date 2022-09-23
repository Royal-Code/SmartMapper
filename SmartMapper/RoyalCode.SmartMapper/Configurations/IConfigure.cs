namespace RoyalCode.SmartMapper.Configurations;

public interface IConfigure
{
    IConfigureAdapter Adapter { get; }
    
    IConfigureMapper Mapper { get; }
    
    IConfigureSelector Selector { get; }
    
    IConfigureSpecifier Specifier { get; }
}