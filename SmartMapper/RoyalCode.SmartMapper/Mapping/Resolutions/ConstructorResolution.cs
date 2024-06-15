using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Mapping.Resolutions;

/// <summary>
/// Resolution for one constructor.
/// </summary>
public class ConstructorResolution : ResolutionBase
{
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
    /// Check if the resolution has a failure.
    /// </summary>
    [MemberNotNullWhen(false, nameof(ParameterResolution))]
    public bool HasFailure([NotNullWhen(true)] out ResolutionFailure? failure)
    {
        failure = Failure;
        return !Resolved;
    }

    /// <summary>
    /// The parameters resolutions for the constructor.
    /// </summary>
    public IEnumerable<ParameterResolution> ParameterResolution { get; }

    /// <summary>
    /// The constructor resolved.
    /// </summary>
    public ConstructorInfo Constructor { get; }

    public override void Completed()
    {
        foreach (var parameterResolution in ParameterResolution)
        {
            parameterResolution.Completed();
        }
    }
}