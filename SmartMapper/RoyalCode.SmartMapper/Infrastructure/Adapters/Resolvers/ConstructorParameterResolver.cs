using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

public class ConstructorParameterResolver
{
    private readonly ParameterInfo parameterInfo;

    public ConstructorParameterResolver(ParameterInfo parameterInfo)
    {
        this.parameterInfo = parameterInfo;
    }

    public ParameterResolution Resolve(ConstructorResolutionContext context)
    {
        if(context.TryGetParameterOptionsByName(parameterInfo.Name!, out var toParameterOptions))
        {
            var propertyOptions = toParameterOptions.ResolvedProperty!;

            var sourceProperty = context.GetSourceProperty(propertyOptions.Property);

            var assignmentResolver = context.GetAssignmentStrategyResolver();
            var assignmentResolution = assignmentResolver.Resolve(
                propertyOptions.Property.PropertyType,
                toParameterOptions.TargetType,
                propertyOptions.AssignmentStrategy);
        }

        throw new NotImplementedException();
    }
}