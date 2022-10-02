using RoyalCode.SmartMapper.Infrastructure.Core;
using RoyalCode.SmartMapper.Infrastructure.Selectors.Resolvers;
using System.Diagnostics.CodeAnalysis;

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
                Strategy = ValueAssignmentStrategy.Select
            };
        
        return new()
        {
            Resolved = false,
            FailureMessages = new[] 
                    { $"The {context.To.Name} type cannot be selected from the {context.From.Name} type" }
                .Concat(selectorResolution.FailureMessages)
        };
    }

    public bool TryResolve(AssignmentContext context, [NotNullWhen(true)] out AssignmentResolution? resolution)
    {
        var selectorResolver = context.Configuration.GetResolver<SelectorResolver>();
        var selectorContext = new SelectorContext(new MapKey(context.From, context.To), context.Configuration);

        if(selectorResolver.TryResolve(selectorContext, out var selectorResolution))
        {
            resolution = new()
            {
                Resolved = true,
                Strategy = ValueAssignmentStrategy.Select
            };
            return true;
        }

        resolution = null;
        return false;
    }
}