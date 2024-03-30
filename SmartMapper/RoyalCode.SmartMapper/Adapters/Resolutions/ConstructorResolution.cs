using RoyalCode.SmartMapper.Core.Resolutions;
using System.Reflection;

namespace RoyalCode.SmartMapper.Adapters.Resolutions;

/// <summary>
/// Resolution for one constructor.
/// </summary>
public class ConstructorResolution : ResolutionBase
{
    // TODO: consider to remove the parameters field and the property below.
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
    /// <param name="constructor">The constructor.</param>
    /// <param name="parameterResolution">The parameters resolutions.</param>
    public ConstructorResolution(ConstructorInfo constructor, IEnumerable<ParameterResolution> parameterResolution)
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