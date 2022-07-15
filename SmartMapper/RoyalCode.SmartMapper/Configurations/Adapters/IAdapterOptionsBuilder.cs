
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Configurations.Adapters;

/// <summary>
/// <para>
///     A builder to configurate the mapping for adapters.
/// </para>
/// </summary>
public interface IAdapterOptionsBuilder
{
    /// <summary>
    /// <para>
    ///     Configures the mapping of a source type to a destination type.
    /// </para>
    /// </summary>
    /// <param name="configure">Configuration action.</param>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TTarget">The destination type.</typeparam>
    /// <returns>The same instance for chained calls.</returns>
    IAdapterOptionsBuilder Configure<TSource, TTarget>(Action<IAdapterOptionsBuilder<TSource, TTarget>> configure);

    /// <summary>
    /// <para>
    ///     Gets the configuration for the specified source and destination types.
    /// </para>
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TTarget">The destination type.</typeparam>
    /// <returns>
    ///     The configuration for the specified source and destination types.
    /// </returns>
    IAdapterOptionsBuilder<TSource, TTarget> Configure<TSource, TTarget>();
}

/// <summary>
/// <para>
///     A builder to configurate the mapping for adapter of a source type to a destination type.
/// </para>
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
/// <typeparam name="TTarget">The destination type.</typeparam>
public interface IAdapterOptionsBuilder<TSource, TTarget>
{
    /// <summary>
    /// <para>
    ///     Adapts the source type to a method of the destination type.
    /// </para>
    /// <para>
    ///     The properties of the source type are mapped to the parameters of the destination method.
    /// </para>
    /// </summary>
    /// <returns>
    ///     A builder to configure the method mapping options.
    /// </returns>
    IAdapterToMethodOptionsBuilder<TSource, TTarget> MapToMethod();

    /// <summary>
    /// <para>
    ///     Adapts the source type to a method of the destination type.
    /// </para>
    /// <para>
    ///     The properties of the source type are mapped to the parameters of the destination method.
    /// </para>
    /// </summary>
    /// <param name="methodSelect">
    ///     A function to select the method of the destination type.
    /// </param>
    /// <returns>
    ///     A builder to configure the method mapping options.
    /// </returns>
    IAdapterToMethodOptionsBuilder<TSource, TTarget> MapToMethod(Expression<Func<TTarget, Delegate>> methodSelect);

    /// <summary>
    /// <para>
    ///     Adapts the source type to a method of the destination type.
    /// </para>
    /// <para>
    ///     The properties of the source type are mapped to the parameters of the destination method.
    /// </para>
    /// </summary>
    /// <param name="methodName">
    ///     The name of the method of the destination type.
    /// </param>
    /// <returns>
    ///     A builder to configure the method mapping options.
    /// </returns>
    IAdapterToMethodOptionsBuilder<TSource, TTarget> MapToMethod(string methodName);

    /// <summary>
    /// <para>
    ///     Configure the constructor that will be used to create the destination instance.
    /// </para>
    /// </summary>
    /// <returns>
    ///     A builder to configure the constructor mapping options.
    /// </returns>
    IAdapterConstructorOptionsBuilder<TSource, TTarget> Constructor();

    /// <summary>
    /// <para>
    ///     Configure the mapping of a property from the source type.
    /// </para>
    /// </summary>
    /// <param name="propertySelection">
    ///     An expression to select the property of the source type.
    /// </param>
    /// <typeparam name="TProperty">
    ///     The type of the property of the source type.
    /// </typeparam>
    /// <returns>
    ///     A builder to configure the property mapping options.
    /// </returns>
    IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty> Map<TProperty>(Expression<Func<TSource, TProperty>> propertySelection);

    /// <summary>
    /// <para>
    ///     Configure the mapping of a property from the source type.
    /// </para>
    /// </summary>
    /// <param name="propertyName">
    ///     The name of the property of the source type.
    /// </param>
    /// <typeparam name="TProperty">
    ///     The type of the property of the source type.
    /// </typeparam>
    /// <returns>
    ///     A builder to configure the property mapping options.
    /// </returns>
    IAdapterPropertyOptionsBuilder<TSource, TTarget, TProperty> Map<TProperty>(string propertyName);
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
            b.MapToMethod(e => e.DoSomething)
                .Parameters(b2 =>
                {
                    b2.Parameter(e => e.Id);
                });
            
            b.Map(d => d.ValueObject).ToMethod(e => e.DoSomething);

            b.Constructor().Parameters(b2 =>
            {
                b2.Parameter(e => e.Id);
            });

            b.Map(d => d.ValueObject).ToConstructor().Parameters(b2 =>
            {
                b2.Parameter(e => e.Id);
            });
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

        public SomeValueObject ValueObject { get; set; }
    }
    
    public class SomeValueObject
    {
        public string Value { get; set; }
    }
}