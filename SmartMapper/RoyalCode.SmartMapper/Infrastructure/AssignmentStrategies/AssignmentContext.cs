using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;

public class AssignmentContext
{
    public Type From { get; init; }

    public Type To { get; init; }

    public AssignmentStrategyOptions? StrategyOptions { get; init; }

    public ResolutionConfiguration Configuration { get; init; }
}