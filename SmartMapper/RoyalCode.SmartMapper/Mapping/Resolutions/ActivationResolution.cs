using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Mapping.Resolutions;

/// <summary>
/// Resolution for the class activator.
/// </summary>
public class ActivationResolution : ResolutionBase
{
    /// <summary>
    /// Creates a ActivationResolution for success.
    /// </summary>
    /// <param name="constructorResolution"></param>
    public ActivationResolution(ConstructorResolution constructorResolution)
    {
        Resolved = true;
        ConstructorResolution = constructorResolution;
    }

    /// <summary>
    /// Creates a ActivationResolution for failure.
    /// </summary>
    /// <param name="failure"></param>
    public ActivationResolution(ResolutionFailure failure)
    {
        Resolved = false;
        Failure = failure;
    }
    
    /// <summary>
    /// Check if the resolution has a failure.
    /// </summary>
    [MemberNotNullWhen(false, nameof(ConstructorResolution))]
    public bool HasFailure([NotNullWhen(true)] out ResolutionFailure? failure)
    {
        failure = Failure;
        return !Resolved;
    }

    /// <summary>
    /// The constructor resolution.
    /// </summary>
    public ConstructorResolution? ConstructorResolution { get; }

    /// <inheritdoc />
    public override void Completed()
    {
        ConstructorResolution?.Completed();
    }
}
