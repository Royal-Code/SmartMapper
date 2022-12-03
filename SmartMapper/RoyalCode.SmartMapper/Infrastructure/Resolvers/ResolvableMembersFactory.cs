using RoyalCode.SmartMapper.Extensions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Callers;
using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers;

/// <summary>
/// <para>
///     Factories for create related members instances.
/// </para>
/// </summary>
public static class ResolvableMembersFactory
{
    /// <summary>
    /// <para>
    ///     For each property of the source type, creates a new instance of <see cref="SourceProperty"/>.
    /// </para>
    /// </summary>
    /// <param name="adapterContext">The context of the adapter resolution process.</param>
    /// <returns>A collection of <see cref="SourceProperty"/>.</returns>
    public static SourceProperty[] CreateSourceProperties(this AdapterContext adapterContext)
        => adapterContext.Options.CreateSourceProperties();

    /// <summary>
    /// <para>
    ///     For each property of the source type, creates a new instance of <see cref="SourceProperty"/>.
    /// </para>
    /// </summary>
    /// <param name="adapterOptions">The adapter options.</param>
    /// <returns>A collection of <see cref="SourceProperty"/>.</returns>
    public static SourceProperty[] CreateSourceProperties(this AdapterOptions adapterOptions)
        => adapterOptions.SourceOptions.CreateSourceProperties();

    /// <summary>
    /// <para>
    ///     For each property of the source type, creates a new instance of <see cref="SourceProperty"/>.
    /// </para>
    /// </summary>
    /// <param name="sourceOptions">The source options with user configuration.</param>
    /// <returns>A collection of <see cref="SourceProperty"/>.</returns>
    public static SourceProperty[] CreateSourceProperties(this SourceOptions sourceOptions)
    {
        var infos = sourceOptions.SourceType.GetReadableProperties();
        var properties = new SourceProperty[infos.Length];
        for (int i = 0; i < infos.Length; i++)
        {
            var info = infos[i];
            var preConfigured = sourceOptions.TryGetPropertyOptions(info.Name, out var option);
            properties[i] = new(info, preConfigured, option ?? new PropertyOptions(info));
        }
        return properties;
    }

    /// <summary>
    /// <para>
    ///     For each constructor of target type that is eligible for mapping,
    ///     creates a new instance of <see cref="EligibleConstructor"/>.
    /// </para>
    /// </summary>
    /// <param name="adapterContext">The adapter context.</param>
    /// <returns>A collection of <see cref="EligibleConstructor"/>.</returns>
    public static EligibleConstructor[] CreateEligibleConstructors(this AdapterContext adapterContext)
        => adapterContext.Options.CreateEligibleConstructors();

    /// <summary>
    /// <para>
    ///     For each constructor of target type that is eligible for mapping,
    ///     creates a new instance of <see cref="EligibleConstructor"/>.
    /// </para>
    /// </summary>
    /// <param name="options">The adapter options with user configuration.</param>
    /// <returns>A collection of <see cref="EligibleConstructor"/>.</returns>
    public static EligibleConstructor[] CreateEligibleConstructors(this AdapterOptions options)
        => options.TargetOptions.GetConstructorOptions().CreateEligibleConstructors();

    /// <summary>
    /// <para>
    ///     For each constructor of target type that is eligible for mapping,
    ///     creates a new instance of <see cref="EligibleConstructor"/>.
    /// </para>
    /// </summary>
    /// <param name="options">The target options with user configuration.</param>
    /// <returns>A collection of <see cref="EligibleConstructor"/>.</returns>
    public static EligibleConstructor[] CreateEligibleConstructors(this ConstructorOptions options)
    {
        if (options.ParameterTypes is not null)
        {
            var constructor = options.TargetType.GetTypeInfo().GetConstructor(
                BindingFlags.Instance | BindingFlags.Public,
                null,
                options.ParameterTypes!,
                null);

            return constructor is null
                ? Array.Empty<EligibleConstructor>()
                : new[] { new EligibleConstructor(constructor, options, true) };
        }

        var constructors = options.TargetType.GetTypeInfo().DeclaredConstructors;
        if (options.NumberOfParameters.HasValue)
            constructors = constructors.Where(c => c.GetParameters().Length == options.NumberOfParameters.Value);

        return constructors.Select(ctor => new EligibleConstructor(ctor, options, false)).ToArray();
    }

    /// <summary>
    /// <para>
    ///     For each parameter in an invocable (constructor or method) of target type,
    ///     creates a new instance of <see cref="TargetParameter"/>.
    /// </para>
    /// </summary>
    /// <param name="request">The request resolve a invocable member.</param>
    /// <returns>A collection of <see cref="TargetParameter"/>.</returns>
    public static TargetParameter[] CreateTargetParameters(this IInvocableRequest request)
    {
        var parameterInfos = request.GetParameters();
        var targetParameters = new TargetParameter[parameterInfos.Length];
        for (int i = 0; i < parameterInfos.Length; i++)
        {
            targetParameters[i] = new(parameterInfos[i]);
        }
        return targetParameters;
    }
        
}
