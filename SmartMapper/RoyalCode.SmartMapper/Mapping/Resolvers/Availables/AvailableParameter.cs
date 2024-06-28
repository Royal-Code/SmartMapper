using System.Reflection;
using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

namespace RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

// TODO: Consider to remove this class and use directly the ParameterInfo.

/// <summary>
/// Represents a target parameter of a constructor or method.
/// </summary>
public sealed class AvailableParameter
{
    /// <summary>
    /// Creates a new list of <see cref="AvailableParameter"/>.
    /// </summary>
    /// <param name="constructor">The target constructor.</param>
    /// <returns>A new list of <see cref="AvailableParameter"/>.</returns>
    public static IReadOnlyCollection<AvailableParameter> Create(TargetConstructor constructor)
    {
        return constructor.Parameters.Select(p => new AvailableParameter(p)).ToList();
    }

    /// <summary>
    /// Creates a new list of <see cref="AvailableParameter"/>.
    /// </summary>
    /// <param name="method">The target methods.</param>
    /// <returns>A new list of <see cref="AvailableParameter"/>.</returns>
    public static IReadOnlyCollection<AvailableParameter> Create(TargetMethod method)
    {
        return method.Parameters.Select(p => new AvailableParameter(p)).ToList();
    }

    /// <summary>
    /// Creates a new instance of <see cref="AvailableParameter"/>.
    /// </summary>
    /// <param name="parameter"></param>
    public AvailableParameter(TargetParameter parameter)
    {
        Parameter = parameter;
    }

    /// <summary>
    /// The parameter information.
    /// </summary>
    public TargetParameter Parameter { get; }
}