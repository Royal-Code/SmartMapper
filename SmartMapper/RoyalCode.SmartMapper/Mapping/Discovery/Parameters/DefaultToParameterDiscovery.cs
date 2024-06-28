using RoyalCode.SmartMapper.Core.Discovery.Assignment;
using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolutions;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Parameters;

internal sealed class DefaultToParameterDiscovery : IToParameterDiscovery
{
    public ToParameterDiscoveryResult Discover(ToParameterDiscoveryRequest request)
    {
        // 1 - get the target parameter
        var targetParameter = request.AvailableParameter;
        
        // 2 - try to find the source parameter by the target parameter name
        var sourceProperty = request.AvailableSourceItems.AvailableSourceProperties
            .FirstOrDefault(x => x.Options.Property.Name.Equals(
                targetParameter.Parameter.Name,
                StringComparison.OrdinalIgnoreCase));
        
        // 3 - if not found, return a failure
        if (sourceProperty is null)
            return new ToParameterDiscoveryResult
            {
                IsResolved = false,
                Failure = new ResolutionFailure(
                    $"The parameter {targetParameter.Parameter.Name} not found in the source type.")
            };
        
        // 4 - Try to find the assignment strategy
        var assignmentRequest = new AssignmentDiscoveryRequest(
            request.Configurations,
            sourceProperty.Options.Property.PropertyType,
            targetParameter.Parameter.ParameterType);

        var assignment = request.Configurations.Discovery.Assignment.Discover(assignmentRequest);
        
        // 5 - if failed to find the assignment strategy, return a failure
        if (!assignment.IsResolved)
            return new ToParameterDiscoveryResult
            {
                IsResolved = false,
                Failure = new ResolutionFailure(
                    $"Failed to find the assignment strategy for the parameter {targetParameter.Parameter.Name}.",
                    assignment.Failure)
            };
        
        // 6 - create the resolution
        return new ToParameterDiscoveryResult
        {
            IsResolved = true,
            Resolution = new ParameterResolution(sourceProperty, targetParameter, assignment.Resolution)
        };
    }
}
