using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;

public class DirectValueAssignmentResolver : IValueAssignmentResolver
{
    public ValueAssignmentStrategy Strategy => ValueAssignmentStrategy.Direct;

    private bool CanResolve(AssignmentContext context)
        => context.To.IsAssignableFrom(context.From);

    private AssignmentResolution CreateResolution() => new()
    {
        Resolved = true,
        Strategy = ValueAssignmentStrategy.Direct
    };

    public AssignmentResolution Resolve(AssignmentContext context)
    {
        if (CanResolve(context))
            return CreateResolution();

        return new()
        {
            Resolved = false,
            FailureMessages = new[] { $"The type {context.To.Name} is not assignable from type {context.From.Name}" }
        };
    }

    public bool TryResolve(
        AssignmentContext context,
        [NotNullWhen(true)] out AssignmentResolution? resolution)
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