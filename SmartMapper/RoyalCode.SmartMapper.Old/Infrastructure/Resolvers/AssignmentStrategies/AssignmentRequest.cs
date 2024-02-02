using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Configurations;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.AssignmentStrategies;

/// <summary>
/// <para>
///     A request for a value assignment.
/// </para>
/// </summary>
/// <param name="From">The source value type.</param>
/// <param name="To">The target value type.</param>
/// <param name="StrategyOptions">The strategy options.</param>
/// <param name="Configuration">The configuration.</param>
public record AssignmentRequest(
    Type From,
    Type To,
    AssignmentStrategyOptions? StrategyOptions,
    ResolutionConfiguration Configuration)
{ }