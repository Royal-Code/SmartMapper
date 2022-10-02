using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;

public class AssignmentStrategyResolver
{
    private readonly ICollection<IValueAssignmentResolver> resolvers;

    public AssignmentStrategyResolver(ICollection<IValueAssignmentResolver> resolvers)
    {
        this.resolvers = resolvers;
    }

    public AssignmentResolution Resolve(AssignmentContext context)
    {
        var strategy = context.StrategyOptions?.Strategy ?? ValueAssignmentStrategy.Undefined;
        if (strategy == ValueAssignmentStrategy.Undefined)
        {
            foreach (var resolver in resolvers)
            {
                if(resolver.TryResolve(context, out var resolution))
                    return resolution;
            }

            return new()
            {
                Resolved = false,
                Strategy = ValueAssignmentStrategy.Undefined,
                FailureMessages = new[] { $"The {context.From.Name} type cannot be assigned to {context.To.Name} type" }
            };
        }
        else
        {
            var resolver = resolvers.FirstOrDefault(r => r.Strategy == strategy);
            if (resolver is null)
                throw new NotSupportedException($"The value assignment strategy {strategy} is not suported");

            return resolver.Resolve(context);
        }
    }
}