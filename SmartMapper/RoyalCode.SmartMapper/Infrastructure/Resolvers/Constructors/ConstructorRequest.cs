using RoyalCode.SmartMapper.Infrastructure.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Activations;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Callers;
using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.Constructors;

/// <summary>
/// <para>
///     Represents a request for resolve a constructor.
/// </para>
/// </summary>
/// <param name="ActivationContext">The activation context.</param>
/// <param name="Constructor">The constructor.</param>
public record ConstructorRequest(
    ActivationContext ActivationContext,
    EligibleConstructor Constructor) : IInvocableRequest
{
    /// <inheritdoc />
    public ParameterInfo[] GetParameters()
    {
        return Constructor.MemberInfo.GetParameters();
    }

    /// <summary>
    /// <para>
    ///     The configuration used to resolve the constructor.
    /// </para>
    /// </summary>
    public ResolutionConfiguration Configuration => ActivationContext.AdapterContext.Configuration;
}
