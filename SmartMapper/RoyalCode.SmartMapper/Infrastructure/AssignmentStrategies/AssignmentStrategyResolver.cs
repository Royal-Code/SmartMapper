using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;

/// <summary>
/// <para>
///     This resolver attempts to resolve the strategy that will be used to assign the value of the source property
///     to the target member (property or parameter).
/// </para>
/// </summary>
public class AssignmentStrategyResolver
{
    /// <summary>
    /// <para>
    ///     Resolves the assignment strategy between a source property and a target member.
    /// </para>
    /// </summary>
    /// <param name="context">Context of assignment to be resolved.</param>
    /// <returns>
    ///     The resolution, which can be successful or unsuccessful.
    /// </returns>
    public AssignmentResolution Resolve(AssignmentContext context)
    {
        var resolvers = context.Configuration.GetResolver<IEnumerable<IValueAssignmentResolver>>();
        
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