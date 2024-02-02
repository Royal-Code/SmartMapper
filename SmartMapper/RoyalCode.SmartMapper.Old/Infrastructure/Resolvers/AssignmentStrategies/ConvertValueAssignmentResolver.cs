using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.AssignmentStrategies;

public class ConvertValueAssignmentResolver : IValueAssignmentResolver
{
    public ValueAssignmentStrategy Strategy => ValueAssignmentStrategy.Convert;

    private bool CanResolve(AssignmentRequest request, [NotNullWhen(true)] out ConvertOptions? convertOptions)
    {
        convertOptions = null;
        request.StrategyOptions?.TryFindAnnotation(out convertOptions);
        if (convertOptions is null)
            request.Configuration.Converters.TryGetConverter(request.From, request.To, out convertOptions);

        return convertOptions is not null;
    }

    private AssignmentResolution CreateSuccess(AssignmentRequest request, ConvertOptions convertOptions)
    {
        return new()
        {
            Resolved = true,
            Strategy = ValueAssignmentStrategy.Convert
        };
    }

    public AssignmentResolution Resolve(AssignmentRequest request)
    {
        if (CanResolve(request, out var convertOptions))
            return CreateSuccess(request, convertOptions);

        return new()
        {
            Resolved = false,
            FailureMessages = new[] { $"The {request.From.Name} type cannot be converted to the {request.To.Name} type" }
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