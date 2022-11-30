using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;
using RoyalCode.SmartMapper.Infrastructure.Core;
using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;

/// <summary>
/// <para>
///     The resolution for the class activator.
/// </para>
/// </summary>
public class ActivationResolution : ResolutionBase
{
    /// <summary>
    /// Creates a ActivationResolution for success.
    /// </summary>
    /// <param name="constructor">The constructor.</param>
    /// <param name="parameterResolution">The parameters resolutions.</param>
    public ActivationResolution(ConstructorInfo constructor, IEnumerable<ParameterResolution> parameterResolution)
    {
        Constructor = constructor;
        ParameterResolution = parameterResolution;
    }

    /// <summary>
    /// Creates a ActivationResolution for failure.
    /// </summary>
    public ActivationResolution() { }

    /// <summary>
    /// The parameters resolutions for the constructor.
    /// </summary>
    public IEnumerable<ParameterResolution>? ParameterResolution { get; }

    /// <summary>
    /// The constructor resolved.
    /// </summary>
    public ConstructorInfo? Constructor { get; }
}