
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.Constructors;

/// <summary>
/// <para>
///     A context to resolve a constructor of a target class, used by adapters.
/// </para>
/// </summary>
public class ConstructorContext
{
    private readonly List<InnerSourcePropertiesGroup> groups = new();
    private readonly ConstructorRequest request;

    /// <summary>
    /// Creates a new instance of <see cref="ConstructorContext"/>.
    /// </summary>
    /// <param name="request">The request for the constructor resolution.</param>
    public ConstructorContext(ConstructorRequest request)
	{
        TargetParameters = request.CreateTargetParameters();
        AvailableSourceProperties = request.CreateAvailableSourceProperties(
            ResolutionStatus.MappedToConstructor, 
            groups);
        this.request = request;
    }

    /// <summary>
    /// The target parameters to be resulved for the constructor.
    /// </summary>
    public IEnumerable<TargetParameter> TargetParameters { get; }

    /// <summary>
    /// The available source properties to be used in the resolution.
    /// </summary>
    public IEnumerable<AvailableSourceProperty> AvailableSourceProperties { get; }

    /// <summary>
    /// <para>
    ///     The configuration used to resolve the constructor.
    /// </para>
    /// </summary>
    public ResolutionConfiguration Configuration => request.Configuration;

    /// <summary>
    /// <para>
    ///     The options used to resolve the constructor.
    /// </para>
    /// </summary>
    public ConstructorOptions ConstructorOptions => request.Constructor.Options;

    /// <summary>
    /// Creates a set of <see cref="ConstrutorParameterRequest"/> for each <see cref="TargetParameters"/>.
    /// </summary>
    /// <returns>The set of requests.</returns>
    public IEnumerable<ConstrutorParameterRequest> ConstrutorParameterRequests()
    {
        return TargetParameters.Select(p => new ConstrutorParameterRequest(this, p));
    }
}
