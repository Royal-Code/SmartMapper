using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.AssignmentStrategies;

/// <summary>
/// <para>
///     Resolution for the assignment strategy between a source property and a target member.
/// </para>
/// </summary>
public class AssignmentResolution : ResolutionBase
{
    /// <summary>
    /// <para>
    ///     The strategy that will be used to assign the value of the source property to the target member.
    /// </para>
    /// </summary>
    public ValueAssignmentStrategy Strategy { get; init; }

    // TODO: Esta resolution dever� ter mais informa��es de como aplicar a opera��o na hora de gerar o c�digo.
    // ainda n�o sei o que...
}