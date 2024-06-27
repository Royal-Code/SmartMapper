using System.Reflection;

namespace RoyalCode.SmartMapper.Mapping.Resolvers.Items;

/// <summary>
/// A container for target parameters, for constructors or methods.
/// </summary>
public sealed class TargetParameter : TargetBase
{
    /// <summary>
    /// Creates a new list of <see cref="TargetParameter"/> from a <paramref name="method"/>.
    /// </summary>
    /// <param name="method"></param>
    /// <returns>A list of <see cref="TargetParameter"/>.</returns>
    public static IReadOnlyCollection<TargetParameter> Create(MethodInfo method)
    {
        return method.GetParameters().Select(p => new TargetParameter(p)).ToList();
    }

    /// <summary>
    /// Creates a new list of <see cref="TargetParameter"/> from a <paramref name="constructor"/>.
    /// </summary>
    /// <param name="constructor">The constructor.</param>
    /// <returns>A list of <see cref="TargetParameter"/>.</returns>
    public static IReadOnlyCollection<TargetParameter> Create(ConstructorInfo constructor)
    {
        return constructor.GetParameters().Select(p => new TargetParameter(p)).ToList();
    }

    /// <summary>
    /// Creates a new <see cref="TargetParameter"/>.
    /// </summary>
    /// <param name="parameterInfo"></param>
    public TargetParameter(ParameterInfo parameterInfo)
    {
        ParameterInfo = parameterInfo;
    }

    /// <summary>
    /// The target parameter.
    /// </summary>
    public ParameterInfo ParameterInfo { get; }
}