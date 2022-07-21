namespace RoyalCode.SmartMapper.Configurations.Adapters;

/// <summary>
/// <para>
///     A builder to configure the mapping of the internal properties of a source property 
///     to a target constructor parameters.
/// </para>
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
/// <typeparam name="TSourceProperty">The source property type.</typeparam>
public interface IAdapterPropertyToConstructorOptionsBuilder<TSource, TSourceProperty>
{
    /// <summary>
    /// In this option, the internal properties of the source type property
    /// are mapped as parameters of the target type constructor.
    /// </summary>
    /// <param name="configureParameters">
    ///     A function to configure the constructor parameters. 
    /// </param>
    void Parameters(Action<IAdapterPropertyToParametersOptionsBuilder<TSource, TSourceProperty>> configureParameters);
}
