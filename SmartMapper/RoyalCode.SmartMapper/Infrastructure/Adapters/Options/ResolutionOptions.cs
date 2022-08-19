namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

/// <summary>
/// <para>
///     This base options is for all the options that are related to assigning values from the source property
///     to some destination property, method parameter or constructor parameter.
/// </para>
/// </summary>
public class ResolutionOptions
{
    /// <summary>
    /// The source property related to the assignment.
    /// </summary>
    internal RefactorOptions.PropertyOptions? ResolvedProperty { get; set; }
}