using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Core.Discovery.Assignment;
using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Discovery.Parameters;
using RoyalCode.SmartMapper.Mapping.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

namespace RoyalCode.SmartMapper.Mapping.Resolvers;

/// <summary>
/// Represents the context for the parameter resolution process.
/// </summary>
internal sealed class ParameterResolver
{
    public static ParameterResolver Create(AvailableParameter availableParameter, AvailableSourceItems availableSourceItems)
    {
        return new ParameterResolver(availableParameter, availableSourceItems);
    }

    private ParameterResolver(AvailableParameter availableParameter, AvailableSourceItems availableSourceItems)
    {
        AvailableParameter = availableParameter;
        AvailableSourceItems = availableSourceItems;
    }

    public AvailableParameter AvailableParameter { get; }

    public AvailableSourceItems AvailableSourceItems { get; }

    public ParameterResolution CreateResolution(MapperConfigurations configurations)
    {
        var parameterName = AvailableParameter.ParameterInfo.Name!;

        // 1 - try to get a pre-configured property mapped to the parameter.
        if (AvailableSourceItems.TryGetAvailableSourcePropertyToParameter(parameterName,
            out var availableSourceProperty, out var toParameterResolutionOptions))
        {
            // check if the assignment was pre-configured
            if (toParameterResolutionOptions.AssignmentStrategy is not null &&
                toParameterResolutionOptions.AssignmentStrategy.Resolution != ValueAssignmentResolution.Undefined)
            {
                // use the pre-configured assignment strategy.
                var assignmentStrategyResolution = new AssignmentStrategyResolution(
                    toParameterResolutionOptions.AssignmentStrategy.Resolution,
                    toParameterResolutionOptions.AssignmentStrategy.Converter);

                return ParameterResolution.Resolves(availableSourceProperty, AvailableParameter, assignmentStrategyResolution);
            }
            
            // discover the assignment strategy.
            var request = new AssignmentDiscoveryRequest(
                configurations,
                availableSourceProperty.SourceItem.Options.Property.PropertyType,
                AvailableParameter.ParameterInfo.ParameterType);

            var result = configurations.Discovery.Assignment.Discover(request);

            // when the assignment strategy is resolved, return the resolution.
            if (result.IsResolved)
                return ParameterResolution.Resolves(availableSourceProperty, AvailableParameter, result.Resolution);
            
            // when the assignment strategy is not resolved, return the failure,
            // adding the messages from the resolution failure.

            ResolutionFailure failure = new(
                "The assignment strategy between the source property " +
                $"({availableSourceProperty.GetPropertyPathString()}) " +
                $"and the constructor parameter ({parameterName}) could not be resolved.");

            failure.AddMessages(result.Failure.Messages);

            return new ParameterResolution(failure);
        }

        // 2 - discover the source property to the parameter.
        var discoveryRequest = new ToParameterDiscoveryRequest(configurations, AvailableParameter, AvailableSourceItems);
        var discoveryResult = configurations.Discovery.ToParameter.Discover(discoveryRequest);

        // when the source property is resolved, return the resolution.
        if (discoveryResult.IsResolved)
            return discoveryResult.Resolution;

        // when the source property is not resolved, return the failure,
        return new ParameterResolution(discoveryResult.Failure);
    }
}
