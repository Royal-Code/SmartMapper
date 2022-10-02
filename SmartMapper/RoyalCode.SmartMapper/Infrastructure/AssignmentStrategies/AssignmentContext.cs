using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Configurations;

namespace RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;

public record AssignmentContext(
    Type From,
    Type To,
    AssignmentStrategyOptions? StrategyOptions,
    ResolutionConfiguration Configuration)
{ }