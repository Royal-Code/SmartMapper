using RoyalCode.SmartMapper.Core.Resolutions;
using System.Reflection;

namespace RoyalCode.SmartMapper.Adapters.Resolutions;

/// <summary>
/// Resolution for one constructor.
/// </summary>
public class ConstructorResolution : ResolutionBase
{
    private ParameterInfo[]? parameters;

    /// <summary>
    /// Creates new resolution for failures.
    /// </summary>
    /// <param name="constructor">The constructor.</param>
    /// <param name="failure">The failure messages.</param>
    public ConstructorResolution(ConstructorInfo constructor, ResolutionFailure failure)
    {
        Resolved = false;
        ParameterResolution = [];
        Constructor = constructor;
        Failure = failure;
    }

    /// <summary>
    /// Creates new resolution for success.
    /// </summary>
    /// <param name="parameterResolution">The parameters resolutions.</param>
    /// <param name="constructor">The constructor.</param>
    public ConstructorResolution(IEnumerable<ParameterResolution> parameterResolution, ConstructorInfo constructor)
    {
        Resolved = true;
        ParameterResolution = parameterResolution;
        Constructor = constructor;
    }

    /// <summary>
    /// The parameters resolutions for the constructor.
    /// </summary>
    public IEnumerable<ParameterResolution> ParameterResolution { get; }

    /// <summary>
    /// The constructor resolved.
    /// </summary>
    public ConstructorInfo Constructor { get; }

    /// <summary>
    /// Get the constructor parameters.
    /// </summary>
    public ParameterInfo[] Parameters => parameters ??= Constructor.GetParameters();
}