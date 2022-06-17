
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Configurations.Adapters;

public interface IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty>
{
    /// <summary>
    /// Mapeia para outro propriedade.
    /// </summary>
    /// <typeparam name="TTargetProperty"></typeparam>
    /// <param name="propertySelection"></param>
    /// <returns></returns>
    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty> To<TTargetProperty>(
        Expression<Func<TTarget, TTargetProperty>> propertySelection);

    /// <summary>
    /// Mapeia para outra propriedade.
    /// </summary>
    /// <typeparam name="TTargetProperty"></typeparam>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty> To<TTargetProperty>(string propertyName);

    /// <summary>
    /// Mapeia as propriedades internas do tipo da propriedade da origem para um construtor.
    /// </summary>
    /// <returns></returns>
    IConstructorPropertyOptionsBuilder<TSource, TSourceProperty> ToConstructor();

    /// <summary>
    /// Mapeia a propriedade atual para um método, onde as propriedades internas serão mapeadas para os parâmetros
    /// do método.
    /// </summary>
    /// <returns></returns>
    IAdapterPropertyToMethodOptionsBuilder<TSource, TTarget, TSourceProperty> ToMethod();
}

public interface IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty>
{
    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty> CastValue();

    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty> UseConverter(
        Expression<Func<TSourceProperty, TTargetProperty>> converter);

    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty> Adapt();

    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty> Select();


    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TTargetProperty> WithService<TService>(
        Expression<Func<TService, TSourceProperty, TTargetProperty>> valueProcessor);

    IAdapterPropertyOptionsBuilder<TSource, TTarget, TSourceProperty, TNextProperty> ThenTo<TNextProperty>(
        Expression<Func<TTargetProperty, TNextProperty>> propertySelection);
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