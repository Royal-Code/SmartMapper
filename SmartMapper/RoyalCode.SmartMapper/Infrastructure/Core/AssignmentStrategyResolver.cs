

using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RoyalCode.SmartMapper.Infrastructure.Core;

public class AssignmentStrategyResolver
{
    public AssignmentResolution Resolve(Type fromType, Type toType, AssignmentStrategyOptions? assignmentStrategy)
    {
        throw new NotImplementedException();
    }

    public AssignmentResolution Resolve(AssignmentContext context)
    {
        var strategy = context.StrategyOptions?.Strategy ?? ValueAssignmentStrategy.Undefined;
        switch (strategy)
        {
            case ValueAssignmentStrategy.Undefined:
                break;
            
            case ValueAssignmentStrategy.Adapt:
                break;
            
            case ValueAssignmentStrategy.Convert:
                break;
            
            case ValueAssignmentStrategy.Direct:
                return ResolveDirect(context);
                
            case ValueAssignmentStrategy.Processor:
                break;
            
            case ValueAssignmentStrategy.Select:
                break;
            
            default:
                throw new NotSupportedException($"The Strategy '{strategy}' is not supported");
        }
        
        throw new NotImplementedException();
    }

    private AssignmentResolution ResolveDirect(AssignmentContext context)
    {
        if (context.To.IsAssignableFrom(context.From))
            return new AssignmentResolution
            {
                Resolved = true,
                Strategy = ValueAssignmentStrategy.Direct
            };
        
        return new AssignmentResolution
        {
            Resolved = false,
            FailureMessages = new []{ $"The type {context.To.Name} is not assignable from type {context.From.Name}" }
        };
    }
}

public class AssignmentContext
{
    public Type From { get; init; }

    public Type To { get; init; }

    public AssignmentStrategyOptions? StrategyOptions { get; init; }
    
    
}

public class AssignmentResolution : ResolutionBase
{
    public ValueAssignmentStrategy Strategy { get; init; }
}

public abstract class ResolutionBase
{
    public bool Resolved { get; init; }
    
    public IEnumerable<string>? FailureMessages { get; init; }
}