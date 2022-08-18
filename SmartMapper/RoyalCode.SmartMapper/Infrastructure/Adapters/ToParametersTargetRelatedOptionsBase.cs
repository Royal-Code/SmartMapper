
namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

public class ToParametersTargetRelatedOptionsBase : TargetRelatedOptionsBase
{
    public ToParametersTargetRelatedOptionsBase(Type sourcePropertyType)
    {
        SourcePropertyType = sourcePropertyType;
    }

    public Type SourcePropertyType { get; }
    
    public ToParametersStrategy Strategy { get; internal set; }
}