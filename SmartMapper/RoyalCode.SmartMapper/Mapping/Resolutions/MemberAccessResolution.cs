using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;
using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

namespace RoyalCode.SmartMapper.Mapping.Resolutions;

/// <summary>
/// A resolution for mapping a source property to inner properties of a target property.
/// </summary>
public sealed class MemberAccessResolution : PropertyResolution
{
    /// <summary>
    /// Creates a new instance of <see cref="MemberAccessResolution"/> for failed resolution.
    /// </summary>
    /// <param name="failure"></param>
    public MemberAccessResolution(ResolutionFailure failure) : base(failure) { }

    /// <summary>
    /// Creates a new instance of <see cref="MemberAccessResolution"/> for successful resolution.
    /// </summary>
    /// <param name="thenResolution"></param>
    /// <param name="availableSourceProperty"></param>
    /// <param name="targetProperty"></param>
    public MemberAccessResolution(PropertyResolution thenResolution,
        AvailableSourceProperty availableSourceProperty, TargetProperty targetProperty) : base(availableSourceProperty, targetProperty)
    {
        ThenResolution = thenResolution;
    }
    
    /// <summary>
    /// <para>
    ///     The resolution to be executed after this resolution.
    /// </para>
    /// </summary>
    public PropertyResolution? ThenResolution { get; }

    /// <summary>
    /// Check if the resolution has a failure.
    /// </summary>
    [MemberNotNullWhen(false, nameof(ThenResolution), nameof(AvailableSourceProperty), nameof(TargetProperty))]
    public bool HasFailure([NotNullWhen(true)] out ResolutionFailure? failure)
    {
        failure = Failure;
        return !Resolved;
    }
    
    /// <inheritdoc />
    public override void Completed()
    {
        base.Completed();
        ThenResolution?.Completed();
    }
}