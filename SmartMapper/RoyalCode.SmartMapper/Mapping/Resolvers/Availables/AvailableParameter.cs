using System.Reflection;

namespace RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

// TODO: Consider to remove this class and use directly the ParameterInfo.

/// <summary>
/// Represents a target parameter of a constructor or method.
/// </summary>
public sealed class AvailableParameter
{
    /// <summary>
    /// Creates a new instance of <see cref="AvailableParameter"/>.
    /// </summary>
    /// <param name="parameterInfo"></param>
    public AvailableParameter(ParameterInfo parameterInfo)
    {
        ParameterInfo = parameterInfo;
    }

    /// <summary>
    /// The parameter information.
    /// </summary>
    public ParameterInfo ParameterInfo { get; }
}