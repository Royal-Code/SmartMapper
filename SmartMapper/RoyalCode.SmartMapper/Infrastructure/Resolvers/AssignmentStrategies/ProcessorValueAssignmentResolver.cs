using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.AssignmentStrategies;

public class ProcessorValueAssignmentResolver : IValueAssignmentResolver
{
    public ValueAssignmentStrategy Strategy => ValueAssignmentStrategy.Processor;

    private bool CanResolve(AssignmentRequest request, [NotNullWhen(true)] out ProcessorOptions? processor)
    {
        processor = null;
        request.StrategyOptions?.TryFindAnnotation(out processor);

        return processor is not null;
    }

    private AssignmentResolution CreateSuccess(AssignmentRequest request, ProcessorOptions processor)
    {
        return new()
        {
            Resolved = true,
            Strategy = ValueAssignmentStrategy.Processor
        };
    }

    public AssignmentResolution Resolve(AssignmentRequest request)
    {
        if (CanResolve(request, out var convertOptions))
            return CreateSuccess(request, convertOptions);

        return new()
        {
            Resolved = false,
            FailureMessages = new[] { $"The {request.From.Name} type cannot be processed to the {request.To.Name} type" }
        };
    }

    public bool TryResolve(AssignmentRequest request, [NotNullWhen(true)] out AssignmentResolution? resolution)
    {
        if (CanResolve(request, out var convertOptions))
        {
            resolution = CreateSuccess(request, convertOptions);
            return true;
        }

        resolution = null;
        return false;
    }
}