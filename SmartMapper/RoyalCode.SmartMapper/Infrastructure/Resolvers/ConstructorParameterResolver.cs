using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Extensions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;
using RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers;

/// <summary>
/// <para>
///     This resolver attempts to resolve a specific parameter of a constructor,
///     based on the parameter name and/or pre-configured options.
/// </para>
/// </summary>
public class ConstructorParameterResolver
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
    /// <param name="context">The constructor parameter contexto to by resolved.</param>
    /// <param name="resolution">The resolution, if possible.</param>
    /// <returns>
    ///     True if a source property with the parameter name exists or the parameter is preconfigured,
    ///     false when direct mapping between a source property and parameter is not possible.
    /// </returns>
    public bool TryResolve(
        ConstrutorParameterContext context,
        [NotNullWhen(true)] out ParameterResolution? resolution)
    {
        var ctorContext = context.ConstructorContext;
        var parameterName = context.Parameter.ParameterInfo.Name!;

        if (ctorContext.TryGetParameterOptionsByName(
            parameterName,
            out var toParameterOptions))
        {
            var propertyOptions = toParameterOptions.ResolvedProperty!;

            if (!ctorContext.TryGetAvailableSourceProperty(
                propertyOptions.Property,
                out var availableProperty,
                out _))
            {
                resolution = null;
                return false;
            }

            var assignmentResolver = context.ConstructorContext.Configuration.GetResolver<AssignmentStrategyResolver>();
            var assignmentContext = new AssignmentContext(
                propertyOptions.Property.PropertyType,
                toParameterOptions.TargetType,
                propertyOptions.AssignmentStrategy,
                ctorContext.Configuration);

            var assignmentResolution = assignmentResolver.Resolve(assignmentContext);
            if (!assignmentResolution.Resolved)
            {
                resolution = new ParameterResolution(availableProperty)
                {
                    Resolved = false,
                    FailureMessages = new[]
                        {
                            $"The property {propertyOptions.Property.GetPathName()} cannot be assigned to the constructor parameter {parameterName} of {ctorContext.TargetType} type"
                        }
                        .Concat(assignmentResolution.FailureMessages)
                };

                return true;
            }

            resolution = new ParameterResolution(availableProperty)
            {
                Resolved = true,
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