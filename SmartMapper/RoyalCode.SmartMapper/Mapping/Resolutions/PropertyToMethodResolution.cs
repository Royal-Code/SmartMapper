using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace RoyalCode.SmartMapper.Mapping.Resolutions;

/// <summary>
/// <para>
///     A resolution for the method mapping.
/// </para>
/// <para>
///     This resolution is used to map the a source property to a method of a target property.
/// </para>
/// </summary>
public sealed class PropertyToMethodResolution : ResolutionBase
{
    public PropertyToMethodResolution(
        AvailableSourceProperty? availableSourceProperty,
        PropertyInfo? targetProperty,
        MethodInfo? methodInfo,
        IEnumerable<ParameterResolution> parametersResolutions)
    {
        Resolved = true;
        AvailableSourceProperty = availableSourceProperty;
        TargetProperty = targetProperty;
        MethodInfo = methodInfo;
        ParametersResolutions = parametersResolutions;
    }

    /// <summary>
    /// Creates a new instance of <see cref="PropertyToMethodResolution"/> for failed resolution.
    /// </summary>
    /// <param name="failure">The failure of the resolution.</param>
    public PropertyToMethodResolution(ResolutionFailure failure)
    {
        Resolved = false;
        Failure = failure;
        ParametersResolutions = [];
    }

    /// <summary>
    /// The available source property.
    /// </summary>
    public AvailableSourceProperty? AvailableSourceProperty { get; }

    /// <summary>
    /// The target parameter.
    /// </summary>
    public PropertyInfo? TargetProperty { get; }

    /// <summary>
    /// The resolved method information.
    /// </summary>
    public MethodInfo? MethodInfo { get; }

    /// <summary>
    /// The parameters resolutions.
    /// </summary>
    public IEnumerable<ParameterResolution> ParametersResolutions { get; }

    /// <summary>
    /// Check if the resolution has a failure.
    /// </summary>
    [MemberNotNullWhen(false, nameof(MethodInfo), nameof(TargetProperty), nameof(AvailableSourceProperty))]
    public bool HasFailure([NotNullWhen(true)] out ResolutionFailure? failure)
    {
        failure = Failure;
        return !Resolved;
    }

    /// <inheritdoc />
    public override void Completed()
    {
        foreach (var resolution in ParametersResolutions)
            resolution.Completed();
    }
}
