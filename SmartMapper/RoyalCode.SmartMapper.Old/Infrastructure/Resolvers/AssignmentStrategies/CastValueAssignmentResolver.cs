using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.AssignmentStrategies;

public class CastValueAssignmentResolver : IValueAssignmentResolver
{
    public ValueAssignmentStrategy Strategy => ValueAssignmentStrategy.Cast;

    private bool CanResolve(AssignmentRequest request)
    {
        return request.From.IsEnum
                && request.To.IsEnum
                && Enum.GetUnderlyingType(request.From) == Enum.GetUnderlyingType(request.To)
            || request.StrategyOptions?.FindAnnotation<ValueAssignmentStrategy>() == ValueAssignmentStrategy.Cast;
    }

    private AssignmentResolution CreateResolution() => new()
    {
        Resolved = true,
        Strategy = ValueAssignmentStrategy.Cast
    };

    public AssignmentResolution Resolve(AssignmentRequest request)
    {
        if (CanResolve(request))
            return CreateResolution();

        return new()
        {
            Resolved = false,
            FailureMessages = new[] { $"The {request.From.Name} type cannot be cast to the {request.To.Name} type" }
        };
    }

    public bool TryResolve(AssignmentRequest request, out AssignmentResolution? resolution)
    {
        if (CanResolve(request))
        {
            resolution = CreateResolution();
            return true;
        }

        resolution = null;
        return false;
    }
}