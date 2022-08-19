
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

public abstract class ToParametersTargetRelatedOptionsBase : TargetRelatedOptionsBase
{
    public ToParametersTargetRelatedOptionsBase(Type sourcePropertyType)
    {
        SourcePropertyType = sourcePropertyType;
    }

    public Type SourcePropertyType { get; }
    
    public ToParametersStrategy Strategy { get; internal set; }

    public abstract ParameterOptionsBase GetParameterOptions(PropertyInfo propertyInfo);

    public abstract bool TryGetPropertyToParameterOptions(
        PropertyInfo property,
        [NotNullWhen(true)] out ParameterOptionsBase? options);
}