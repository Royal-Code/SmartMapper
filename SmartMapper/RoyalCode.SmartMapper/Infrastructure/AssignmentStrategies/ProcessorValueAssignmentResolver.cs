using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;

public class ProcessorValueAssignmentResolver : IValueAssignmentResolver
{
    public ValueAssignmentStrategy Strategy => ValueAssignmentStrategy.Processor;
    
    private bool CanResolve(AssignmentContext context, [NotNullWhen(true)] out ProcessorOptions? processor)
    {
        processor = null;
        context.StrategyOptions?.TryFindAnnotation(out processor);

        return processor is not null;
    }
    
    private AssignmentResolution CreateSuccess(AssignmentContext context, ProcessorOptions processor)
    {
        return new()
        {
            Resolved = true,
            Strategy = ValueAssignmentStrategy.Processor
        };
    }
    
    public AssignmentResolution Resolve(AssignmentContext context)
    {
        if (CanResolve(context, out var convertOptions))
            return CreateSuccess(context, convertOptions); 
        
        return new()
        {
            Resolved = false,
            FailureMessages = new[] { $"The {context.From.Name} type cannot be processed to the {context.To.Name} type" }
        };
    }

    public bool TryResolve(AssignmentContext context, [NotNullWhen(true)] out AssignmentResolution? resolution)
    {
        if (CanResolve(context, out var convertOptions))
        {
            resolution = CreateSuccess(context, convertOptions);
            return true;
        }

        resolution = null;
        return false;
    }
}