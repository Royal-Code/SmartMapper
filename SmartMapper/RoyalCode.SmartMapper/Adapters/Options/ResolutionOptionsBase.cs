using RoyalCode.SmartMapper.Adapters.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Options;

/// <summary>
/// <para>
///     This base options is for all the options that are related to assigning values from the source property
///     to some destination property, method parameter or constructor parameter.
/// </para>
/// </summary>
public abstract class ResolutionOptionsBase
{
    /// <summary>
    /// <para>
    ///     Base constructor for the resolution options.
    /// </para>
    /// <para>
    ///     Call <see cref="PropertyOptions.ResolvedBy(ResolutionOptionsBase)"/> to add the current instance
    ///     as a resolution of the source property.
    /// </para>
    /// </summary>
    /// <param name="resolvedProperty">The source property related to the assignment.</param>
    protected ResolutionOptionsBase(PropertyOptions resolvedProperty)
    {
        ResolvedProperty = resolvedProperty;

        resolvedProperty.ResolvedBy(this);
    }

    /// <summary>
    /// The source property related to the assignment.
    /// </summary>
    public PropertyOptions ResolvedProperty { get; }

    /// <summary>
    /// The kind or status of the mapping of the property.
    /// </summary>
    public ResolutionStatus Status { get; set; }

    /// <summary>
    /// Options of the strategy to be used to assign the property value to the destination counterpart.
    /// </summary>
    public AssignmentStrategyOptions? AssignmentStrategy { get; private set; }

    /// <summary>
    /// <para>
    ///     Get o create the <see cref="AssignmentStrategyOptions{TProperty}"/>.
    /// </para>
    /// <para>
    ///     When the <see cref="AssignmentStrategy"/> is null or not for the <typeparamref name="TProperty"/> type,
    ///     a new instance of the <see cref="AssignmentStrategyOptions{TProperty}"/> is created.
    /// </para>
    /// </summary>
    /// <typeparam name="TProperty">The type of the source property.</typeparam>
    /// <returns>The <see cref="AssignmentStrategyOptions{TProperty}"/>.</returns>
    public AssignmentStrategyOptions<TProperty> GetOrCreateAssignmentStrategyOptions<TProperty>()
    {
        if (typeof(TProperty) != ResolvedProperty.Property.PropertyType)
            throw new InvalidOperationException($"The type of the property '{ResolvedProperty.Property.Name}' is not '{typeof(TProperty).Name}'");

        if (AssignmentStrategy is not AssignmentStrategyOptions<TProperty> strategyOptions)
        {
            strategyOptions = new AssignmentStrategyOptions<TProperty>();
            AssignmentStrategy = strategyOptions;
        }

        return strategyOptions;
    }
}
