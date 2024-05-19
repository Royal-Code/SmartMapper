using RoyalCode.SmartMapper.Adapters.Resolvers.Targets;
using RoyalCode.SmartMapper.Core.Resolutions;
using System.Reflection;

namespace RoyalCode.SmartMapper.Adapters.Resolvers.Available;

/// <summary>
/// A target eligible method to be mapped.
/// </summary>
public sealed class AvailableMethod
{
    /// <summary>
    /// Creates a collection of <see cref="AvailableMethod"/> for the type.
    /// Get all the methods of the target type that are eligible for mapping.
    /// </summary>
    /// <param name="targetType">The target type to get the methods.</param>
    /// <returns>The collection of eligible constructors.</returns>
    public static ICollection<AvailableMethod> Create(Type targetType)
    {
        return targetType.GetTypeInfo()
            .GetRuntimeMethods()
            .Where(m => m is { IsPublic: true, IsStatic: false, IsSpecialName: false })
            .Select(m => new AvailableMethod(m))
            .ToList();
    }

    /// <summary>
    /// Creates a new instance of <see cref="AvailableMethod"/>
    /// </summary>
    /// <param name="info"></param>
    public AvailableMethod(MethodInfo info)
    {
        Info = info;
    }

    /// <summary>
    /// The method info.
    /// </summary>
    public MethodInfo Info { get; }

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
    ///     creates a new instance of <see cref="TargetParameter"/>.
    /// </para>
    /// </summary>
    /// <returns>A collection of <see cref="TargetParameter"/>.</returns>
    public IEnumerable<TargetParameter> CreateTargetParameters()
    {
        var parameterInfos = Info.GetParameters();
        var targetParameters = new TargetParameter[parameterInfos.Length];
        for (int i = 0; i < parameterInfos.Length; i++)
        {
            targetParameters[i] = new(parameterInfos[i]);
        }
        return targetParameters;
    }
}
