namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

/// <summary>
/// <para>
///     Options to map the inner properties of a source property to a target constructor.
/// </para>
/// </summary>
public class ToConstructorOptions : ResolutionOptions
{
    /// <summary>
    /// Creates new <see cref="ToConstructorOptions"/>.
    /// </summary>
    /// <param name="propertyOptions">The source property.</param>
    /// <param name="constructorOptions">The target constructor.</param>
    public ToConstructorOptions(PropertyOptions propertyOptions, ConstructorOptions constructorOptions)
    {
        ConstructorOptions = constructorOptions;
        SourceOptions = new SourceOptions(propertyOptions.Property.PropertyType);
    }
    
    /// <summary>
    /// The target constructor options.
    /// </summary>
    public ConstructorOptions ConstructorOptions { get; }

    /// <summary>
    /// Source options created for the property mapped to the target constructor.
    /// </summary>
    public SourceOptions SourceOptions { get; }
}