using RoyalCode.SmartMapper.Infrastructure.Core;
using RoyalCode.SmartMapper.Infrastructure.Selectors.Resolvers;
using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.AssignmentStrategies;

public class SelectValueAssignmentResolver : IValueAssignmentResolver
{
    public ValueAssignmentStrategy Strategy => ValueAssignmentStrategy.Select;

    public AssignmentResolution Resolve(AssignmentRequest request)
    {
        var selectorResolver = request.Configuration.GetResolver<SelectorResolver>();
        var selectorContext = new SelectorContext(new MapKey(request.From, request.To), request.Configuration);
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
                    { $"The {request.To.Name} type cannot be selected from the {request.From.Name} type" }
                .Concat(selectorResolution.FailureMessages)
        };
    }

    public bool TryResolve(AssignmentRequest request, [NotNullWhen(true)] out AssignmentResolution? resolution)
    {
        var selectorResolver = request.Configuration.GetResolver<SelectorResolver>();
        var selectorContext = new SelectorContext(new MapKey(request.From, request.To), request.Configuration);

        if (selectorResolver.TryResolve(selectorContext, out var selectorResolution))
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