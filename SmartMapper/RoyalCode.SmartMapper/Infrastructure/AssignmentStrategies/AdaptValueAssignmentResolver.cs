using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;

public class AdaptValueAssignmentResolver : IValueAssignmentResolver
{
    public ValueAssignmentStrategy Strategy => ValueAssignmentStrategy.Adapt;
    
    public AssignmentResolution Resolve(AssignmentContext context)
    {
        var adapterResolver = context.Configuration.GetResolver<AdapterResolver>();
        var adapterContext = new AdapterResolverContext(new MapKey(context.From, context.To), context.Configuration);
        var adapterResolution = adapterResolver.Resolve(adapterContext);
        
        if (adapterResolution.Resolved)
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
                .Concat(adapterResolution.FailureMessages)
        };
    }

    public bool TryResolve(AssignmentContext context, [NotNullWhen(true)] out AssignmentResolution? resolution)
    {
        var adapterResolver = context.Configuration.GetResolver<AdapterResolver>();
        var adapterContext = new AdapterResolverContext(new MapKey(context.From, context.To), context.Configuration);
        
        if (adapterResolver.TryResolve(adapterContext, out var adapterResolution))
        {
            resolution = new()
            {
                Resolved = true,
                Strategy = ValueAssignmentStrategy.Adapt
            };
            return true;
        }

        resolution = null;
        return false;
    }
}