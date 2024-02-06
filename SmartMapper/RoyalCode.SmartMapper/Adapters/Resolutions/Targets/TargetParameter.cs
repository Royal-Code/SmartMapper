using System.Reflection;

namespace RoyalCode.SmartMapper.Adapters.Resolutions.Targets;

/// <summary>
/// Represents a target parameter of a constructor or method.
/// </summary>
public class TargetParameter 
{
    /// <summary>
    /// Creates a new instance of <see cref="TargetParameter"/>.
    /// </summary>
    /// <param name="parameterInfo"></param>
    public TargetParameter(ParameterInfo parameterInfo)
    {
        ParameterInfo = parameterInfo;
    }
    
    /// <summary>
    /// The parameter information.
    /// </summary>
    public ParameterInfo ParameterInfo { get; }
}