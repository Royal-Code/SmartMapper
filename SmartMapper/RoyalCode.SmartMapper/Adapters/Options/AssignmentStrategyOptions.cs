using RoyalCode.SmartMapper.Core.Resolutions;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Adapters.Options;

/// <summary>
/// The options of the strategy used to assign the value of the source property to the destination property or parameter.
/// </summary>
public sealed class AssignmentStrategyOptions
{
    /// <summary>
    /// Creates a new instance of <see cref="AssignmentStrategyOptions"/>.
    /// </summary>
    public AssignmentStrategyOptions()
    {
        Resolution = ValueAssignmentResolution.Undefined;
    }

    /// <summary>
    /// The resolution of the assignment between source type and destination type.
    /// </summary>
    public ValueAssignmentResolution Resolution { get; internal set; }

    /// <summary>
    /// A converter, used to convert the value of the source type to the target type.
    /// </summary>
    public ValueAssignmentConverter? Converter { get; internal set; }

    /// <summary>
    /// Defines the resolution of the assignment as direct.
    /// </summary>
    public void UseDirect()
    {
        Resolution = ValueAssignmentResolution.Direct;
    }

    /// <summary>
    /// Defines the resolution of the assignment as cast.
    /// </summary>
    public void UseCast()
    {
        Resolution = ValueAssignmentResolution.Cast;
    }

    /// <summary>
    /// Define the resolution of the assignment to adapt the source type to the target type.
    /// </summary>
    public void UseAdapt()
    {
        Resolution = ValueAssignmentResolution.Adapt;
    }

    /// <summary>
    /// Define the resolution of the assignment to select the target type from the source type.
    /// </summary>
    public void UseSelect()
    {
        Resolution = ValueAssignmentResolution.Select;
    }
}

/// <summary>
/// The options of the strategy used to assign the value of the source property to the destination property or parameter.
/// </summary>
/// <typeparam name="TProperty">The type of the source property.</typeparam>
public class AssignmentStrategyOptions<TProperty> : AssignmentStrategyOptions
{
    /// <summary>
    /// Define the resolution of the assignment to convert the value of the source type to the target type.
    /// </summary>
    /// <typeparam name="TParameter">The target type.</typeparam>
    /// <param name="converter">The converter expression.</param>
    public void UseConvert<TParameter>(Expression<Func<TProperty, TParameter>> converter)
    {
        Resolution = ValueAssignmentResolution.Convert;

        Converter = new ValueAssignmentConverter(typeof(TProperty), typeof(TParameter), converter);
    }
}
