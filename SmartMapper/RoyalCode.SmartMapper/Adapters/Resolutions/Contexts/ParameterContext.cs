using RoyalCode.SmartMapper.Adapters.Resolutions.Targets;
using RoyalCode.SmartMapper.Adapters.Resolvers.Avaliables;
using RoyalCode.SmartMapper.Core.Configurations;

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
        var targetType = TargetParameter.ParameterInfo.Member.DeclaringType!;
        var parameterType = TargetParameter.ParameterInfo.ParameterType;

        // 1 - try get a pre-configured property mapped to the parameter.
        if (AvailableSourceItems.TryGetAvailableSourcePropertyToParameter(parameterName,
            out var availableSourceProperty, out var toParameterResolutionOptions))
        {
            // check if the assignment was pre-configured
            if (toParameterResolutionOptions.AssignmentStrategy is not null &&
                toParameterResolutionOptions.AssignmentStrategy.Resolution != ValueAssignmentResolution.Undefined)
            {
                // use the pre-configured assignment strategy.
            }
            else
            {
                // discover the assignment strategy.
                var assignmentResult = configurations.Discovery.Assignment.Discover(
                    availableSourceProperty.SourceItem.Options.Property.PropertyType,
                    parameterType);
            }
            
        }

        throw new NotImplementedException();
    }
}
