
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Configurations.Adapters;

/// <summary>
/// <para>
///     A builder to configure the mapping of a source property.
/// </para>
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
/// <typeparam name="TTarget">The destination type.</typeparam>
/// <typeparam name="TSourceProperty">The source property type.</typeparam>
public interface IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty>
{
    /// <summary>
    /// <para>
    ///     Map to another property.
    /// </para>
    /// </summary>
    /// <typeparam name="TTargetProperty">The destination property type.</typeparam>
    /// <param name="propertySelection">The property selection expression.</param>
    /// <returns>
    ///     The builder to configure the property to property mapping.
    /// </returns>
    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty> To<TTargetProperty>(
        Expression<Func<TTarget, TTargetProperty>> propertySelection);

    /// <summary>
    /// <para>
    ///     Map to another property.
    /// </para>
    /// </summary>
    /// <typeparam name="TTargetProperty">The destination property type.</typeparam>
    /// <param name="propertyName">The property name.</param>
    /// <returns>
    ///     The builder to configure the property to property mapping.
    /// </returns>
    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty> To<TTargetProperty>(string propertyName);

    /// <summary>
    /// <para>
    ///     Maps the internal properties of the source property type to a constructor.
    /// </para>
    /// </summary>
    /// <returns>
    ///     The builder to configure the property to constructor mapping.
    /// </returns>
    IConstructorPropertyOptionsBuilder<TSource, TSourceProperty> ToConstructor();

    /// <summary>
    /// Maps the current property to a method, where the internal properties will be mapped to the method parameters.
    /// </summary>
    /// <returns>
    ///     The builder to configure the property to method mapping.
    /// </returns>
    IAdapterPropertyToMethodOptionsBuilder<TSource, TTarget, TSourceProperty> ToMethod();
    
    /// <summary>
    /// Maps the current property to a method, where the internal properties will be mapped to the method parameters.
    /// </summary>
    /// <param name="methodSelect">
    ///     A function to select the method of the destination type.
    /// </param>
    /// <returns>
    ///     The builder to configure the property to method mapping.
    /// </returns>
    IAdapterPropertyToMethodOptionsBuilder<TSource, TTarget, TSourceProperty> ToMethod(
        Expression<Func<TTarget, Delegate>> methodSelect);
}

/// <summary>
/// <para>
///     A builder to configure the mapping of a source property to a target property.
/// </para>
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
/// <typeparam name="TTarget">The destination type.</typeparam>
/// <typeparam name="TSourceProperty">The source property type.</typeparam>
/// <typeparam name="TTargetProperty">The destination property type.</typeparam>
public interface IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty>
    : IAdapterPropertyStrategyBuilder<TSourceProperty, TTargetProperty, IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty>>
{
    /// <summary>
    /// <para>
    ///     Continues the mapping of the source property to an internal property of the target property.
    /// </para>
    /// </summary>
    /// <param name="propertySelection">
    ///     The property selection expression.
    /// </param>
    /// <typeparam name="TNextProperty">
    ///     The internal property type.
    /// </typeparam>
    /// <returns>
    ///     The builder to configure the property to property mapping.
    /// </returns>
    IAdapterPropertyThenOptionsBuilder<TSourceProperty, TTargetProperty, TNextProperty> ThenTo<TNextProperty>(
        Expression<Func<TTargetProperty, TNextProperty>> propertySelection);

    /// <summary>
    /// <para>
    ///     Continues the mapping of the source property to an internal property of the target property.
    /// </para>
    /// </summary>
    /// <returns>
    ///     The builder to configure the property to property mapping.
    /// </returns>
    IAdapterPropertyThenOptionsBuilder<TSourceProperty, TTargetProperty> Then();
}


public interface IConstructorPropertyOptionsBuilder<TSource, TSourceProperty>
{
    /// <summary>
    /// Nesta opção, algumas propriedades do tipo de origem são mapeadas os parâmetros.
    /// A ordem das propriedades é a ordem dos parâmetros.
    /// </summary>
    /// <param name="configureParameters"></param>
    /// <returns></returns>
    void Parameters(Action<IConstructorPropertyParametersOptionsBuilder<TSource, TSourceProperty>> configureParameters);
}

public interface IConstructorPropertyParametersOptionsBuilder<TSource, TSourceProperty>
{
    /// <summary>
    /// Mapeia uma propriedade do tipo de origem para um parâmetro do construtor no tipo de destino.
    /// A ordem de chamada deste método durante a configuração dos parâmetros definirá a posição
    /// do parâmetro.
    /// </summary>
    /// <typeparam name="TProperty">The source property type.</typeparam>
    /// <param name="propertySelector"></param>
    /// <returns></returns>
    IConstructorParameterPropertyOptionsBuilder<TSource, TSourceProperty, TProperty> Parameter<TProperty>(
        Expression<Func<TSourceProperty, TProperty>> propertySelector);
}

public interface IConstructorParameterPropertyOptionsBuilder<TSource, TSourceProperty, TParameterProperty>
{
    // deve ser possível configurar a estratégia de atribuíção.
    // semelhante ao IAdapterToMethodPropertyParameterOptionsBuilder
}

public interface IAdapterPropertyToMethodOptionsBuilder<TSource, TTarget, TSourceProperty>
{
    /// <summary>
    /// Nesta opção, as propriedades internas do tipo da propriedade da origem são mapeadas os parâmetros.
    /// A ordem das propriedades é a ordem dos parâmetros.
    /// </summary>
    /// <param name="configureParameters"></param>
    /// <returns></returns>
    void Parameters(Action<IAdapterPropertyToMethodParametersOptionsBuilder<TSource, TSourceProperty>> configureParameters);

    /// <summary>
    /// Nesta opção, a propriedade da origem é mapeada como parâmetro do método, onde o método deverá ter um único
    /// parâmetro.
    /// </summary>
    /// <param name="configureProperty"></param>
    void Value(Action<IAdapterPropertyToMethodParameterOptionsBuilder<TSource, TSourceProperty>> configureProperty);
    
    // deve ser possível configurar o nome do método ou o método por uma expressão.
}

public interface IAdapterPropertyToMethodParametersOptionsBuilder<TSource, TSourceProperty>
{
    /// <summary>
    /// Mapeia uma propriedade do tipo da propriedade da origem para um parâmetro do método no tipo de destino.
    /// A ordem de chamada deste método durante a configuração dos parâmetros definirá a posição
    /// do parâmetro.
    /// </summary>
    /// <typeparam name="TProperty">The source property type.</typeparam>
    /// <param name="propertySelector"></param>
    /// <returns></returns>
    IConstructorParameterPropertyOptionsBuilder<TSource, TSourceProperty, TProperty> Parameter<TProperty>(
        Expression<Func<TSourceProperty, TProperty>> propertySelector);
}

public interface IAdapterPropertyToMethodParameterOptionsBuilder<TSource, TSourceProperty, TParameterProperty>
{
    // deve ser possível configurar a estratégia de atribuíção.
    // semelhante ao IAdapterToMethodPropertyParameterOptionsBuilder
}

public interface IAdapterPropertyToMethodParameterOptionsBuilder<TSource, TSourceProperty>
{
    // deve ser possível configurar a estratégia de atribuíção.
    // semelhante ao IAdapterToMethodPropertyParameterOptionsBuilder
}