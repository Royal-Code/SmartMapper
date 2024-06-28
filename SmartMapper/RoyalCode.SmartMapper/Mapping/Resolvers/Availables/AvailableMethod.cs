using System.Reflection;
using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

namespace RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

/// <summary>
/// A target eligible method to be mapped.
/// </summary>
public sealed class AvailableMethod
{
    /// <summary>
    /// Creates a collection of <see cref="AvailableMethod"/> for the type.
    /// Get all the methods of the target type that are eligible for mapping.
    /// </summary>
    /// <param name="targetMethods">The target methods.</param>
    /// <returns>The collection of eligible constructors.</returns>
    public static IReadOnlyCollection<AvailableMethod> Create(IReadOnlyCollection<TargetMethod> targetMethods)
    {
        return targetMethods
            .Where(m => !m.IsResolved)
            .Select(m => new AvailableMethod(m))
            .ToList();
    }

    /// <summary>
    /// Creates a new instance of <see cref="AvailableMethod"/>
    /// </summary>
    /// <param name="method"></param>
    public AvailableMethod(TargetMethod method)
    {
        Method = method;
    }

    /// <summary>
    /// The method info.
    /// </summary>
    public TargetMethod Method { get; }

    /// <summary>
    /// if the method is resolved.
    /// </summary>
    public bool Resolved { get; private set; }

    /// <summary>
    /// The resolution of the property, if resolved.
    /// </summary>
    public ResolutionBase? Resolution { get; private set; }

    /// <summary>
    /// Assign the resolution of the property.
    /// </summary>
    /// <param name="resolution"></param>
    public void ResolvedBy(ResolutionBase resolution)
    {
        Resolution = resolution;
        Resolved = true;
    }

    /// <summary>
    /// <para>
    ///     For each parameter in the constructor of target type,
    ///     creates a new instance of <see cref="AvailableParameter"/>.
    /// </para>
    /// </summary>
    /// <returns>A collection of <see cref="AvailableParameter"/>.</returns>
    public IReadOnlyCollection<AvailableParameter> CreateAvailableParameters()
    {
        return AvailableParameter.Create(Method);
    }
}
