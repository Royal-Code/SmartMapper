using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using RoyalCode.SmartMapper.Mapping.Options;

namespace RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

/// <summary>
/// <para>
///     Represents a target type constructor that is eligible for mapping.
/// </para>
/// </summary>
public sealed class AvailableConstructor
{
    /// <summary>
    /// Creates a collection of <see cref="AvailableConstructor"/> based on the options.
    /// Get all the constructors of the target type that are eligible for mapping.
    /// </summary>
    /// <param name="options">The constructor options.</param>
    /// <returns>The collection of eligible constructors.</returns>
    public static ICollection<AvailableConstructor> Create(ConstructorOptions options)
    {
        if (options.ParameterTypes is not null)
        {
            var constructor = options.TargetType.GetTypeInfo().GetConstructor(
                BindingFlags.Instance | BindingFlags.Public,
                null,
                options.ParameterTypes!,
                null);

            return constructor is null
                ? Array.Empty<AvailableConstructor>()
                : [new AvailableConstructor(constructor, options)];
        }

        var constructors = options.TargetType.GetTypeInfo().DeclaredConstructors;
        if (options.NumberOfParameters.HasValue)
            constructors = constructors.Where(c => c.GetParameters().Length == options.NumberOfParameters.Value);

        // sort the constructors by the number of parameters, highest to lowest
        constructors = constructors.OrderByDescending(c => c.GetParameters().Length);

        return constructors.Select(ctor => new AvailableConstructor(ctor, options)).ToList();
    }

    /// <summary>
    /// Check if the collection of constructors has a single empty constructor.
    /// </summary>
    /// <param name="constructors">The collection of constructors.</param>
    /// <param name="eligible">The single empty constructor.</param>
    /// <returns>
    ///     True if the collection has a single empty constructor; otherwise, false.
    /// </returns>
    public static bool HasSingleEmptyConstructor(ICollection<AvailableConstructor> constructors, 
        [NotNullWhen(true)] out AvailableConstructor? eligible)
    {
        if (constructors.Count != 1)
        {
            eligible = null;
            return false;
        }

        eligible = constructors.Single();
        return eligible.Info.GetParameters().Length == 0;
    }

    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="AvailableConstructor"/>.
    /// </para>
    /// </summary>
    /// <param name="constructorInfo">The constructor information.</param>
    /// <param name="constructorOptions">The options.</param>
    public AvailableConstructor(ConstructorInfo constructorInfo, ConstructorOptions constructorOptions)
    {
        Info = constructorInfo;
        Options = constructorOptions;
    }

    /// <summary>
    /// The constructor information.
    /// </summary>
    public ConstructorInfo Info { get; }

    /// <summary>
    /// The constructor options.
    /// </summary>
    public ConstructorOptions Options { get; }

    /// <summary>
    /// <para>
    ///     For each parameter in the constructor of target type,
    ///     creates a new instance of <see cref="AvailableParameter"/>.
    /// </para>
    /// </summary>
    /// <returns>A collection of <see cref="AvailableParameter"/>.</returns>
    public IEnumerable<AvailableParameter> CreateTargetParameters()
    {
        var parameterInfos = Info.GetParameters();
        var targetParameters = new AvailableParameter[parameterInfos.Length];
        for (int i = 0; i < parameterInfos.Length; i++)
        {
            targetParameters[i] = new(parameterInfos[i]);
        }
        return targetParameters;
    }
}
