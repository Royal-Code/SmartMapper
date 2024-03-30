using System.Reflection;
using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Resolutions;

/// <summary>
/// <para>
///     A resolution for the method mapping.
/// </para>
/// <para>
///     This resolution is used to map the source properties to the target method.
/// </para>
/// </summary>
public sealed class SourceToMethodResolution: ResolutionBase
{
    /// <summary>
    /// Creates a new instance of <see cref="SourceToMethodResolution"/> for successful resolutions.
    /// </summary>
    /// <param name="info">The resolved method information.</param>
    /// <param name="parametersResolutions">The parameters resolutions.</param>
    public SourceToMethodResolution(MethodInfo info, IEnumerable<ParameterResolution> parametersResolutions)
    {
        Resolved = true;
        Info = info;
        ParametersResolutions = parametersResolutions;
    }
    
    /// <summary>
    /// Creates a new instance of <see cref="SourceToMethodResolution"/> for failed resolutions.
    /// </summary>
    /// <param name="failure"></param>
    public SourceToMethodResolution(ResolutionFailure failure)
    {
        Resolved = false;
        Failure = failure;
        ParametersResolutions = [];
    }
    
    /// <summary>
    /// The resolved method information.
    /// </summary>
    public MethodInfo? Info { get; }
    
    /// <summary>
    /// The parameters resolutions.
    /// </summary>
    public IEnumerable<ParameterResolution> ParametersResolutions { get; }
}