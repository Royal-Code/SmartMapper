using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

public class PropertyToConstructorOptions : ToParametersTargetRelatedOptionsBase
{
    public PropertyToConstructorOptions(Type propertyType, ConstructorOptions constructorOptions)
        : base(propertyType)
    {
        ConstructorOptions = constructorOptions;
    }
    
    public ConstructorOptions ConstructorOptions { get; }
    
    public override ParameterOptionsBase GetParameterOptions(PropertyInfo propertyInfo)
    {
        throw new NotImplementedException();
    }

    public override bool TryGetPropertyToParameterOptions(PropertyInfo property, out ParameterOptionsBase? options)
    {
        throw new NotImplementedException();
    }
}