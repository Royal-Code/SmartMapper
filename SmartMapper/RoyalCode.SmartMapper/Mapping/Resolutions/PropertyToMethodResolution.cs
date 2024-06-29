using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;
using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

namespace RoyalCode.SmartMapper.Mapping.Resolutions;

/// <summary>
/// <para>
///     A resolution for mapping a source property to a method of a target property.
/// </para>
/// </summary>
public sealed class PropertyToMethodResolution : PropertyResolution
{
    /// <summary>
    /// Constructor for failed resolution of <see cref="PropertyToMethodResolution"/> .
    /// </summary>
    /// <param name="failure"></param>
    public PropertyToMethodResolution(ResolutionFailure failure) : base(failure) { }

    /// <summary>
    /// Constructor for successful resolution of <see cref="PropertyToMethodResolution"/> .
    /// </summary>
    /// <param name="method">The resolved method information.</param>
    /// <param name="parametersResolutions">The parameters resolutions.</param>
    /// <param name="availableSourceProperty">The available source property.</param>
    /// <param name="targetProperty">The target property.</param>
    public PropertyToMethodResolution(
        TargetMethod method,
        IReadOnlyCollection<ParameterResolution> parametersResolutions,
        AvailableSourceProperty availableSourceProperty,
        TargetProperty targetProperty) : base(availableSourceProperty, targetProperty)
    {
        Method = method;
        ParametersResolutions = parametersResolutions;
    }
    
    /// <summary>
    /// The resolved method information.
    /// </summary>
    public TargetMethod? Method { get; }

    /// <summary>
    /// The parameters resolutions.
    /// </summary>
    public IReadOnlyCollection<ParameterResolution>? ParametersResolutions { get; }

    /// <summary>
    /// Check if the resolution has a failure.
    /// </summary>
    [MemberNotNullWhen(false, nameof(Method), nameof(ParametersResolutions), nameof(AvailableSourceProperty), nameof(TargetProperty))]
    public bool HasFailure([NotNullWhen(true)] out ResolutionFailure? failure)
    {
        failure = Failure;
        return !Resolved;
    }

    /// <inheritdoc />
    public override void Completed()
    {
        base.Completed();
        Method?.ResolvedBy(this);
        foreach (var resolution in ParametersResolutions)
            resolution.Completed();
    }
}
