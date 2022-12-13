using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.AssignmentStrategies;

public class DirectValueAssignmentResolver : IValueAssignmentResolver
{
    public ValueAssignmentStrategy Strategy => ValueAssignmentStrategy.Direct;

    private bool CanResolve(AssignmentRequest request)
        => request.To.IsAssignableFrom(request.From);

    private AssignmentResolution CreateResolution() => new()
    {
        Resolved = true,
        Strategy = ValueAssignmentStrategy.Direct
    };

    public AssignmentResolution Resolve(AssignmentRequest request)
    {
        if (CanResolve(request))
            return CreateResolution();

        return new()
        {
            Resolved = false,
            FailureMessages = new[] { $"The type {request.To.Name} is not assignable from type {request.From.Name}" }
        };
    }

    public bool TryResolve(
        AssignmentRequest request,
        [NotNullWhen(true)] out AssignmentResolution? resolution)
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