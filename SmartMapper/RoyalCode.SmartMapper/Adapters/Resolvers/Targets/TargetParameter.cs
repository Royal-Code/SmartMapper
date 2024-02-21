using System.Reflection;

namespace RoyalCode.SmartMapper.Adapters.Resolvers.Targets;

/// <summary>
/// Represents a target parameter of a constructor or method.
/// </summary>
public sealed class TargetParameter
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