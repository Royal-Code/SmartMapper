using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

namespace RoyalCode.SmartMapper.Mapping.Resolutions;

/// <summary>
/// Resolution for one constructor.
/// </summary>
public class ConstructorResolution : ResolutionBase
{
    /// <summary>
    /// Creates new resolution for failures.
    /// </summary>
    /// <param name="constructor">The target constructor.</param>
    /// <param name="failure">The failure messages.</param>
    public ConstructorResolution(TargetConstructor constructor, ResolutionFailure failure)
    {
        Resolved = false;
        ParameterResolution = [];
        Constructor = constructor;
        Failure = failure;
    }

    /// <summary>
    /// Creates new resolution for success.
    /// </summary>
    /// <param name="constructor">The target constructor.</param>
    /// <param name="parameterResolution">The parameters resolutions.</param>
    public ConstructorResolution(TargetConstructor constructor, IEnumerable<ParameterResolution> parameterResolution)
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
    /// The target constructor resolved.
    /// </summary>
    public TargetConstructor Constructor { get; }

    public override void Completed()
    {
        Constructor.ResolvedBy(this);

        foreach (var parameterResolution in ParameterResolution)
        {
            parameterResolution.Completed();
        }
    }
}