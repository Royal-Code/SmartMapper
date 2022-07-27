
using RoyalCode.SmartMapper.Infrastructure.Adapters;

namespace RoyalCode.SmartMapper.Infrastructure.Core;

/// <summary>
/// <para>
///     This base options is for all the options that are related to assigning values from the source property
///     to some destination property, method parameter or constructor parameter.
/// </para>
/// </summary>
public abstract class WithAssignmentOptionsBase : OptionsBase
{
    /// <summary>
    /// The source property related to the assignment.
    /// </summary>
    internal PropertyOptions? PropertyRelated { get; set; }
    
    /// <summary>
    /// Undoes the association between the source property and the destination.
    /// </summary>
    internal void Reset()
    {
        PropertyRelated?.ResetMapping();
        PropertyRelated = null;
    }
}