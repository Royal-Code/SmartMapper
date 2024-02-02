using RoyalCode.SmartMapper.Infrastructure.Core;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Constructors;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.Activations;

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
    /// <param name="constructorResolution">The resolution for the constructor.</param>
    public ActivationResolution(ConstructorResolution constructorResolution)
    {
        Resolved = true;
        ConstructorResolution = constructorResolution;
    }

    /// <summary>
    /// Creates a ActivationResolution for failure.
    /// </summary>
    public ActivationResolution() { }

    /// <summary>
    /// The constructor resolution.
    /// </summary>
    public ConstructorResolution? ConstructorResolution { get; }

}