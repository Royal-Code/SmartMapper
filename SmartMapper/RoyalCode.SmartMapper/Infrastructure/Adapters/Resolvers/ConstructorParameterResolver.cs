using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Extensions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;
using RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

public class ConstructorParameterResolver
{
    public bool TryResolve(
        ConstrutorParameterContext context,
        [NotNullWhen(true)] out ParameterResolution? resolution)
    {
        var ctorContext = context.ConstructorContext;
        var parameterName = context.Parameter.ParameterInfo.Name!;
        
        if(ctorContext.TryGetParameterOptionsByName(
               parameterName,
            out var toParameterOptions))
        {
            var propertyOptions = toParameterOptions.ResolvedProperty!;

            var sourceProperty = ctorContext.GetSourceProperty(propertyOptions.Property);

            var assignmentResolver = ctorContext.GetAssignmentStrategyResolver();
            var assignmentContext = new AssignmentContext(
                propertyOptions.Property.PropertyType,
                toParameterOptions.TargetType,
                propertyOptions.AssignmentStrategy,
                ctorContext.Configuration);

            var assignmentResolution = assignmentResolver.Resolve(assignmentContext);
            if (!assignmentResolution.Resolved)
            {
                resolution = new ParameterResolution
                {
                    Resolved = false,
                    SourceProperty = sourceProperty,
                    FailureMessages = new[]
                            { $"The property {propertyOptions.Property.GetPathName()} cannot be assigned to the constructor parameter {parameterName} of {ctorContext.TargetType} type" }
                        .Concat(assignmentResolution.FailureMessages)
                };

                return true;
            }

            resolution = new ParameterResolution
            {
                Resolved = true,
                SourceProperty = sourceProperty,
                AssignmentResolution = assignmentResolution,
                Parameter = context.Parameter,
                ToParameterOptions = toParameterOptions
            };
            return true;
        }

        resolution = null;
        return false;
    }
}