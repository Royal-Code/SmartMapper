using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;

public class ConvertValueAssignmentResolver : IValueAssignmentResolver
{
    public ValueAssignmentStrategy Strategy => ValueAssignmentStrategy.Convert;
    
    private bool CanResolve(AssignmentContext context, [NotNullWhen(true)] out ConvertOptions? convertOptions)
    {
        convertOptions = null;
        context.StrategyOptions?.TryFindAnnotation(out convertOptions);
        if (convertOptions is null)
            context.Configuration.Converters.TryGetConverter(context.From, context.To, out convertOptions);

        return convertOptions is not null;
    }
    
    private AssignmentResolution CreateSuccess(AssignmentContext context, ConvertOptions convertOptions)
    {
        return new()
        {
            Resolved = true,
            Strategy = ValueAssignmentStrategy.Convert
        };
    }
    
    public AssignmentResolution Resolve(AssignmentContext context)
    {
        if (CanResolve(context, out var convertOptions))
            return CreateSuccess(context, convertOptions); 
        
        return new()
        {
            Resolved = false,
            FailureMessages = new[] { $"The {context.From.Name} type cannot be converted to the {context.To.Name} type" }
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