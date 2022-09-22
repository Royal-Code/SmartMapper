using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;

/// <summary>
/// <para>
///     Interface para estratégias de resolução de atribuíção de valores.
/// </para>
/// <para>
///     Usado em adaptadores e mapeadores.
/// </para>
/// <para>
///     Deve ser verificado se o tipo de origem é compatível com o tipo de destino, dentro da estratégia de atribuíção.
/// </para>
/// </summary>
public interface IValueAssignmentResolver
{
    ValueAssignmentStrategy Strategy { get; }

    /// <summary>
    /// Checa se pode resolver, criar o resultado.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    AssignmentResolution Resolve(AssignmentContext context);

    /// <summary>
    /// Chega se pode resolver, se pode, cria o resultado, caso contrário retorna false sem criar resultado.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="resolution"></param>
    /// <returns></returns>
    bool TryResolve(AssignmentContext context, [NotNullWhen(true)] out AssignmentResolution? resolution);
}