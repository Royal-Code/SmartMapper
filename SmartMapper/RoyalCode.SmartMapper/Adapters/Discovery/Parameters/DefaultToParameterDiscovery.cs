
using RoyalCode.SmartMapper.Adapters.Resolutions;
using RoyalCode.SmartMapper.Core.Discovery.Assignment;
using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Discovery.Parameters;

internal sealed class DefaultToParameterDiscovery : IToParameterDiscovery
{
    public ToParameterDiscoveryResult Discover(ToParameterDiscoveryRequest request)
    {
        // 1 - get the target parameter
        var targetParameter = request.TargetParameter;
        
        // 2 - try to find the source parameter by the target parameter name
        var sourceProperty = request.AvailableSourceItems.AvailableSourceProperties
            .FirstOrDefault(x => x.Options.Property.Name.Equals(
                targetParameter.ParameterInfo.Name,
                StringComparison.OrdinalIgnoreCase));
        
        // 3 - if not found, return a failure
        if (sourceProperty is null)
            return new ToParameterDiscoveryResult
            {
                IsResolved = false,
                Failure = new ResolutionFailure(
                    $"The parameter {targetParameter.ParameterInfo.Name} not found in the source type.")
            };
        
        // 4 - Try to find the assignment strategy
        var assignmentRequest = new AssignmentDiscoveryRequest(
            request.Configurations,
            sourceProperty.Options.Property.PropertyType,
            targetParameter.ParameterInfo.ParameterType);

        var assignment = request.Configurations.Discovery.Assignment.Discover(assignmentRequest);
        
        // 5 - if failed to find the assignment strategy, return a failure
        if (!assignment.IsResolved)
            return new ToParameterDiscoveryResult
            {
                IsResolved = false,
                Failure = new ResolutionFailure(
                    $"Failed to find the assignment strategy for the parameter {targetParameter.ParameterInfo.Name}.",
                    assignment.Failure)
            };
        
        // 6 - create the resolution
        return new ToParameterDiscoveryResult
        {
            IsResolved = true,
            Resolution = ParameterResolution.Resolves(sourceProperty, targetParameter, assignment.Resolution)
        };
    }
}
