using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;
using RoyalCode.SmartMapper.Infrastructure.Core;
using RoyalCode.SmartMapper.Infrastructure.Selectors.Resolvers;

namespace RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;

public class SelectValueAssignmentResolver : IValueAssignmentResolver
{
    public ValueAssignmentStrategy Strategy => ValueAssignmentStrategy.Select;
    
    public AssignmentResolution Resolve(AssignmentContext context)
    {
        var selectorResolver = context.Configuration.GetResolver<SelectorResolver>();
        var selectorContext = new SelectorContext(new MapKey(context.From, context.To), context.Configuration);
        var selectorResolution = selectorResolver.Resolve(selectorContext);
        
        if (selectorResolution.Resolved)
            return new()
            {
                Resolved = true,
                Strategy = ValueAssignmentStrategy.Adapt
            };
        
        return new()
        {
            Resolved = false,
            FailureMessages = new[] 
                    { $"The {context.From.Name} type cannot be adapted to the {context.To.Name} type" }
                .Concat(selectorResolution.FailureMessages)
        };
    }

    public bool TryResolve(AssignmentContext context, out AssignmentResolution? resolution)
    {
        throw new NotImplementedException();
    }
}