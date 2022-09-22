using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;

public class CastValueAssignmentResolver : IValueAssignmentResolver
{
    public ValueAssignmentStrategy Strategy => ValueAssignmentStrategy.Cast;

    private bool CanResolve(AssignmentContext context)
    {
        return (context.From.IsEnum 
                && context.To.IsEnum
                && Enum.GetUnderlyingType(context.From) == Enum.GetUnderlyingType(context.To))
            || context.StrategyOptions?.FindAnnotation<ValueAssignmentStrategy>() == ValueAssignmentStrategy.Cast;
    }

    private AssignmentResolution CreateResolution() => new()
    {
        Resolved = true,
        Strategy = ValueAssignmentStrategy.Cast
    };
    
    public AssignmentResolution Resolve(AssignmentContext context)
    {
        if (CanResolve(context))
            return CreateResolution();

        return new()
        {
            Resolved = false,
            FailureMessages = new[] { $"The {context.From.Name} type cannot be cast to the {context.To.Name} type" }
        };
    }

    public bool TryResolve(AssignmentContext context, out AssignmentResolution? resolution)
    {
        if (CanResolve(context))
        {
            resolution = CreateResolution();
            return true;
        }

        resolution = null;
        return false;
    }
}