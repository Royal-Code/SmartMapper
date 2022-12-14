using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Core;
using RoyalCode.SmartMapper.Infrastructure.Discovery;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Parameters;

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

    public bool HasFailure => TargetParameters.Any(p => p.HasFailure);

    public bool IsParametersResolved => TargetParameters.All(p => !p.Unresolved);

    public bool IsSuccessfullyResolved => IsParametersResolved && groups.All(g => g.Resolved) && !HasFailure;

    public ConstructorResolution GetResolution()
    {
        if (HasFailure)
        {
            return new ConstructorResolution(
                request.Constructor.MemberInfo,
                TargetParameters.Where(p => p.HasFailure).SelectMany(p => p.Resolution!.FailureMessages!));
        }

        if (!IsSuccessfullyResolved)
        {
            // pegar os parâmetros não resolvidos e gerar uma mensagem.
            var unresolvedParametersMessages = TargetParameters.Where(p => p.Unresolved)
                .Select(p => "The parameter '" + p.MemberInfo.Name + "' is not resolved");

            // pegar os grupos não resolvidos, e gerar uma mensagem para cada, incluindo os nomes das
            // propriedades internas não resolvidas.
            var unresolvedGroupsMessages = groups.Where(g => !g.Resolved)
                .Select(g => g.GetFailureMessage());

            return new ConstructorResolution(
                request.Constructor.MemberInfo,
                unresolvedParametersMessages.Concat(unresolvedGroupsMessages));
        }

        // processar sucesso.
        var resolutions = TargetParameters.Select(p => p.Resolution!).ToList();

        return new ConstructorResolution(resolutions, request.Constructor.MemberInfo);
    }

    /// <summary>
    /// Creates a set of <see cref="ConstrutorParameterRequest"/> for each <see cref="TargetParameters"/>.
    /// </summary>
    /// <returns>The set of requests.</returns>
    public IEnumerable<ConstrutorParameterRequest> ConstrutorParameterRequests()
    {
        return TargetParameters.Select(p => new ConstrutorParameterRequest(this, p));
    }

    /// <summary>
    /// Process the parameter matching to set the available source property resolved.
    /// </summary>
    /// <param name="match">The parameter matching.</param>
    public void Resolved(ParameterMatch match)
    {
        Resolved(new ParameterResolution(match.Property)
        {
            Resolved = true,
            AssignmentResolution = match.AssignmentResolution,
            Parameter = match.Parameter
        });
    }

    /// <summary>
    /// Process the <see cref="ParameterResolution"/> to set the available source property resolved
    /// and the target parameter resolved.
    /// </summary>
    /// <param name="resolution"></param>
    public void Resolved(ParameterResolution resolution)
    {
        resolution.AvailableSourceProperty.ResolvedBy(resolution);
        resolution.Parameter?.ResolvedBy(resolution);
    }
}
