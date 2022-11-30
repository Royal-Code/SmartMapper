using RoyalCode.SmartMapper.Infrastructure.Core;
using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;

/// <summary>
/// Resolution for one constructor.
/// </summary>
public class ConstrutorResolution : ResolutionBase
{
    private ParameterInfo[]? parameters;

    /// <summary>
    /// Creates new resolution for failures.
    /// </summary>
    /// <param name="constructor">The constructor.</param>
    /// <param name="failureMessages">The failure messages.</param>
    public ConstrutorResolution(ConstructorInfo constructor, IEnumerable<string> failureMessages)
    {
        Resolved = false;
        ParameterResolution = Enumerable.Empty<ParameterResolution>();
        Constructor = constructor;
        FailureMessages = failureMessages;
    }

    /// <summary>
    /// Creates new resolution for success.
    /// </summary>
    /// <param name="rarameterResolution">The parameters resolutions.</param>
    /// <param name="constructor">The constructor.</param>
    public ConstrutorResolution(IEnumerable<ParameterResolution> rarameterResolution, ConstructorInfo constructor)
    {
        Resolved = true;
        ParameterResolution = rarameterResolution;
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