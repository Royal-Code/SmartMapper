using RoyalCode.SmartMapper.Adapters.Discovery.Parameters;
using RoyalCode.SmartMapper.Adapters.Resolvers.Available;
using RoyalCode.SmartMapper.Adapters.Resolvers.Targets;
using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Core.Discovery.Assignment;
using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Resolutions.Contexts;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

/// <summary>
/// Represents the context for the parameter resolution process.
/// </summary>
internal sealed class ParameterContext
{
    public static ParameterContext Create(TargetParameter targetParameter, AvailableSourceItems availableSourceItems)
    {
        return new ParameterContext()
        {
            TargetParameter = targetParameter,
            AvailableSourceItems = availableSourceItems
        };
    }

    public TargetParameter TargetParameter { get; private init; }

    public AvailableSourceItems AvailableSourceItems { get; private init; }

    public ParameterResolution CreateResolution(MapperConfigurations configurations)
    {
        var parameterName = TargetParameter.ParameterInfo.Name!;

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

                return ParameterResolution.Resolves(availableSourceProperty, TargetParameter, assignmentStrategyResolution);
            }
            
            // discover the assignment strategy.
            var request = new AssignmentDiscoveryRequest(
                configurations,
                availableSourceProperty.SourceItem.Options.Property.PropertyType,
                TargetParameter.ParameterInfo.ParameterType);

            var result = configurations.Discovery.Assignment.Discover(request);

            // when the assignment strategy is resolved, return the resolution.
            if (result.IsResolved)
                return ParameterResolution.Resolves(availableSourceProperty, TargetParameter, result.Resolution);
            
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
        var discoveryRequest = new ToParameterDiscoveryRequest(configurations, TargetParameter, AvailableSourceItems);
        var discoveryResult = configurations.Discovery.ToParameter.Discover(discoveryRequest);

        // when the source property is resolved, return the resolution.
        if (discoveryResult.IsResolved)
            return discoveryResult.Resolution;

        // when the source property is not resolved, return the failure,
        return new ParameterResolution(discoveryResult.Failure);
    }
}
