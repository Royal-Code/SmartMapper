using System.Reflection;
using RoyalCode.SmartMapper.Extensions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;
using RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;
using RoyalCode.SmartMapper.Infrastructure.Discovery;

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
            var assignmentContext = new AssignmentContext(
                propertyOptions.Property.PropertyType,
                toParameterOptions.TargetType,
                propertyOptions.AssignmentStrategy,
                context.Configuration);

            var assignmentResolution = assignmentResolver.Resolve(assignmentContext);
            if (!assignmentResolution.Resolved)
            {
                return new ParameterResolution
                {
                    Resolved = false,
                    FailureMessages = new[]
                        { $"The property {propertyOptions.Property.GetPathName()} cannot be assigned to the constructor parameter {parameterInfo.Name!} of {context.TargetType} type" }
                        .Concat(assignmentResolution.FailureMessages)
                };
            }

            return new ParameterResolution
            {
                Resolved = true,
                SourceProperty = sourceProperty,
                AssignmentResolution = assignmentResolution,
                ParameterInfo = parameterInfo,
                ToParameterOptions = toParameterOptions
            };
        }
        else
        {
            var discoveryContext = context.CreateDiscoveryContext(parameterInfo);
            var ctorParamDiscovery = context.Configuration.GetResolver<ConstructorParameterDiscovery>();

            if (ctorParamDiscovery.TryGetPropertyForParameter(discoveryContext, out var property))
            {

            }
            else
            {
                // return error
            }
        }

        throw new NotImplementedException();
    }
}