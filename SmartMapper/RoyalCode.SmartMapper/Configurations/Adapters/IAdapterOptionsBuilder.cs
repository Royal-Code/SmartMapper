
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Configurations.Adapters;

public interface IAdapterOptionsBuilder
{
    IAdapterOptionsBuilder Configure<TSource, TTarget>(Action<IAdapterOptionsBuilder<TSource, TTarget>> configure);

    IAdapterOptionsBuilder<TSource, TTarget> Configure<TSource, TTarget>();
}

public interface IAdapterOptionsBuilder<TSource, TTarget>
{
    IAdapterToMethodOptionsBuilder<TSource, TTarget> MapToMethod();

    IAdapterToMethodOptionsBuilder<TSource, TTarget> MapToMethod(Expression<Func<TTarget, Delegate>> methodSelect);

    IAdapterToMethodOptionsBuilder<TSource, TTarget> MapToMethod(string methodName);

    IAdapterConstructorOptionsBuilder<TSource, TTarget> Constructor();

    IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty> Map<TProperty>(Expression<Func<TSource, TProperty>> propertySelection);

    IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty> Map<TProperty>(string propertyName);
}

public interface IAdapterConstructorOptionsBuilder<TSource, TTarget>
{
    void Parameters(Action<IAdapterConstructorParametersOptionsBuilder<TSource, TTarget>> configurePrameters);

    void WithParameters(int numberOfParameters);

    void WithParameters(params Type[] parameterTypes);
}

public interface IAdapterConstructorParametersOptionsBuilder<TSource, TTarget>
{

}

/// <summary>
/// Aqui o tipo de origem é mapeado para um método.
/// Por padrão, todas propriedades serão mapeadas para o método, e serão resolvidas pelo nome da propriedade/parâmetro.
/// É possível escolher só algumas propriedades a serem mapeadas para um método.
/// Esta opção requer que o método seja identificado.
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <typeparam name="TTarget"></typeparam>
public interface IAdapterToMethodOptionsBuilder<TSource, TTarget>
{
    /// <summary>
    /// Nesta opção, algumas propriedades do tipo de origem são mapeadas os parâmetros.
    /// A ordem das propriedades é a ordem dos parâmetros.
    /// </summary>
    /// <param name="configureParameters"></param>
    /// <returns></returns>
    void Parameters(Action<IAdapterToMethodParametersOptionsBuilder<TSource, TTarget>> configureParameters);

    /// <summary>
    /// Esta é a opção padrão caso a outra não seja executada.
    /// Nele é possível configurar cada propriedade do tipo de origem para um parâmetro.
    /// </summary>
    /// <param name="configureProperties"></param>
    void AllProperties(Action<IAdapterToMethodPropertiesOptionsBuilder<TSource, TTarget>> configureProperties);
}

/// <summary>
/// Configura os parâmetros de um método adaptado do tipo de origem.
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <typeparam name="TTarget"></typeparam>
public interface IAdapterToMethodParametersOptionsBuilder<TSource, TTarget>
{
    /// <summary>
    /// Mapeia uma propriedade do tipo de origem para um parâmetro no tipo de destino.
    /// A ordem de chamada deste método durante a configuração dos parâmetros definirá a posição
    /// do parâmetro.
    /// </summary>
    /// <typeparam name="TProperty">The source property type.</typeparam>
    /// <param name="propertySelector"></param>
    /// <returns></returns>
    IAdapterToMethodPropertyParameterOptionsBuilder<TSource, TTarget, TProperty> Parameter<TProperty>(
        Expression<Func<TSource, TProperty>> propertySelector);
}

/// <summary>
/// Opções para configurar uma propriedade do tipo de origem que é mapeada para um parâmetro de um método no tipo 
/// de destino.
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <typeparam name="TTarget"></typeparam>
/// <typeparam name="TProperty"></typeparam>
public interface IAdapterToMethodPropertyParameterOptionsBuilder<TSource, TTarget, TProperty>
{
    // deve ser possível configurar a estratégia de atribuíção.
}

/// <summary>
/// Opções onde todas propriedades do tipo de origem são mapeadas como parâmetros no tipo de destino.
/// É possível mapear a propriedade para um parâmetro, definindo o nome do parâmetro e estratégia de atribuíção.
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <typeparam name="TTarget"></typeparam>
public interface IAdapterToMethodPropertiesOptionsBuilder<TSource, TTarget>
{

}

public static class ConfigureSample
{
    public static void Configure(IAdapterOptionsBuilder builder)
    {
        builder.Configure<MyDto, MyEntity>(b =>
        {
            b.Map(d => d.Id).To(e => e.Id);
        });

        builder.Configure<MyDto, MyEntity>(b =>
        {
            b.MapToMethod(e => e.DoSomething);
        });
    }

    public class MyEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public void DoSomething(string value)
        {
            Console.WriteLine(value);
        }
    }

    public class MyDto
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}