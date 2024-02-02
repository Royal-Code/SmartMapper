using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Infrastructure.Core;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Adapters;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.AssignmentStrategies;

public class AdaptValueAssignmentResolver : IValueAssignmentResolver
{
    public ValueAssignmentStrategy Strategy => ValueAssignmentStrategy.Adapt;

    public AssignmentResolution Resolve(AssignmentRequest request)
    {
        var adapterResolver = request.Configuration.GetResolver<AdapterResolver>();
        var adapterContext = new AdapterRequest(new MapKey(request.From, request.To), request.Configuration);
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
                { $"The {request.From.Name} type cannot be adapted to the {request.To.Name} type" }
                .Concat(adapterResolution.FailureMessages)
        };
    }

    public bool TryResolve(AssignmentRequest request, [NotNullWhen(true)] out AssignmentResolution? resolution)
    {
        var adapterResolver = request.Configuration.GetResolver<AdapterResolver>();
        var adapterContext = new AdapterRequest(new MapKey(request.From, request.To), request.Configuration);

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