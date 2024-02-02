namespace RoyalCode.SmartMapper.Adapters.Configurations;

/// <summary>
/// <para>
///     A builder to configurate the mapping of the constructor of the destination type.
/// </para>
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
public interface IConstructorOptionsBuilder<TSource>
{
    /// <summary>
    /// <para>
    ///     Configure the parameters of the constructor of the destination type.
    /// </para>
    /// </summary>
    /// <param name="configurePrameters"></param>
    void Parameters(Action<IConstructorParametersOptionsBuilder<TSource>> configurePrameters);

    /// <summary>
    /// <para>
    ///     Adds information about the constructor of the destination type.
    ///     Set the number of parameters that the constructor of the destination type has.
    /// </para>
    /// </summary>
    /// <param name="numberOfParameters">The number of parameters used to select the constructor.</param>
    void WithParameters(int numberOfParameters);

    /// <summary>
    /// <para>
    ///     Adds information about the constructor of the destination type.
    ///     Set the paramters types that the constructor of the destination type has.
    /// </para>
    /// </summary>
    /// <param name="parameterTypes">The parameters types.</param>
    void WithParameters(params Type[] parameterTypes);
}
