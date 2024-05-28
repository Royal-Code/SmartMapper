
using RoyalCode.SmartMapper.Mapping.Builders;

namespace RoyalCode.SmartMapper.Adapters.Configurations;

/// <summary>
/// <para>
///     A builder to configure the mapping of the internal properties of a source property 
///     to a target constructor parameters.
/// </para>
/// </summary>
/// <typeparam name="TSourceProperty">The source property type.</typeparam>
public interface IPropertyToConstructorOptionsBuilder<TSourceProperty>
{
    /// <summary>
    /// <para>
    ///     In this option, the internal properties of the source type property
    ///     are mapped as parameters of the target type constructor.
    /// </para>
    /// </summary>
    /// <param name="configurePrameters">
    ///     A function to configure the constructor parameters. 
    /// </param>
    void Parameters(Action<IConstructorParametersBuilder<TSourceProperty>> configurePrameters);
}
