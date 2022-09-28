using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;

public class ConvertValueAssignmentResolver: IValueAssignmentResolver
{
    public ValueAssignmentStrategy Strategy => ValueAssignmentStrategy.Convert;
    
    private bool CanResolve(AssignmentContext context, out ConvertOptions? convertOptions)
    {
        convertOptions = null;
        context.StrategyOptions?.TryFindAnnotation(out convertOptions);
        if (convertOptions is null)
            context.Configuration.Converters.TryGetConverter(context.From, context.To, out convertOptions);

        return convertOptions is not null;
    }
    
    public AssignmentResolution Resolve(AssignmentContext context)
    {
        throw new NotImplementedException();
    }

    public bool TryResolve(AssignmentContext context, out AssignmentResolution? resolution)
    {
        throw new NotImplementedException();
    }
}