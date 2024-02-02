using System.Diagnostics.CodeAnalysis;
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

    /// <summary>
    /// The resolution of the parameter.
    /// </summary>
    public ParameterResolution? Resolution { get; private set; }

    /// <summary>
    /// Determines if the parameter is resolved.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Resolution))]
    public bool IsResolved => Resolution is not null;
    
    /// <summary>
    /// Marks the parameter as resolved.
    /// </summary>
    /// <param name="resolution">The resolution.</param>
    public void ResolvedBy(ParameterResolution resolution) => Resolution = resolution;
}