using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Resolutions;

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
    /// The constructor resolution.
    /// </summary>
    public ConstructorResolution? ConstructorResolution { get; }

    /// <inheritdoc />
    public override void Completed()
    {
        ConstructorResolution?.Completed();
    }
}
