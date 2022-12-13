using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Extensions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.AssignmentStrategies;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Invocables;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.Parameters;

/// <summary>
/// <para>
///     This resolver attempts to resolve a specific parameter of a constructor,
///     based on the parameter name and/or pre-configured options.
/// </para>
/// </summary>
public class ParameterResolver
{
    /// <summary>
    /// <para>
    ///     Try resolving the parameter, by name.
    /// </para>
    /// <para>
    ///     The name can be from the source property or the name of the preconfigured parameter.
    /// </para>
    /// <para>
    ///     When not found a relationship between a source property and the parameter, will return false.
    /// </para>
    /// <para>
    ///     If a relationship is found, a resolution will be generated.
    /// </para>
    /// <para>
    ///     The resolution may contain failures if the relationship exists
    ///     and it is not possible to automatically assign the source property to the parameter
    ///     because of incompatible data types.
    /// </para>
    /// </summary>
    /// <param name="request">The parameter request to by resolved.</param>
    /// <param name="resolution">The resolution, if possible.</param>
    /// <returns>
    ///     True if a source property with the parameter name exists or the parameter is preconfigured,
    ///     false when direct mapping between a source property and parameter is not possible.
    /// </returns>
    public bool TryResolve(
        IParameterRequest request,
        [NotNullWhen(true)] out ParameterResolution? resolution)
    {
        var parameterName = request.Parameter.MemberInfo.Name!;
        var targetType = request.Parameter.MemberInfo.Member.DeclaringType!;

        if (request.TryGetParameterOptionsByName(
            parameterName,
            out var toParameterOptions))
        {
            var propertyOptions = toParameterOptions.ResolvedProperty!;

            if (!request.TryGetAvailableSourceProperty(
                propertyOptions.Property,
                out var availableProperty))
            {
                resolution = null;
                return false;
            }

            var assignmentResolver = request.Configuration.GetResolver<AssignmentStrategyResolver>();
            var assignmentRequest = new AssignmentRequest(
                propertyOptions.Property.PropertyType,
                targetType,
                propertyOptions.AssignmentStrategy,
                request.Configuration);

            var assignmentResolution = assignmentResolver.Resolve(assignmentRequest);
            if (!assignmentResolution.Resolved)
            {
                resolution = new ParameterResolution(availableProperty)
                {
                    Resolved = false,
                    FailureMessages = new[]
                        {
                            $"The property {propertyOptions.Property.GetPathName()} cannot be assigned to the constructor parameter {parameterName} of {targetType} type"
                        }
                        .Concat(assignmentResolution.FailureMessages)
                };

                return true;
            }

            resolution = new ParameterResolution(availableProperty)
            {
                Resolved = true,
                AssignmentResolution = assignmentResolution,
                Parameter = request.Parameter
            };
            return true;
        }

        resolution = null;
        return false;
    }
}