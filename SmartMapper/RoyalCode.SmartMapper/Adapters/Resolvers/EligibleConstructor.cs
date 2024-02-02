
using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Resolutions;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;


namespace RoyalCode.SmartMapper.Adapters.Resolvers;

/// <summary>
/// <para>
///     Represents a target type constructor that is eligible for mapping.
/// </para>
/// </summary>
public sealed class EligibleConstructor
{
    /// <summary>
    /// Creates a collection of <see cref="EligibleConstructor"/> based on the options.
    /// Get all the constructors of the target type that are eligible for mapping.
    /// </summary>
    /// <param name="options">The constructor options.</param>
    /// <returns>The collection of eligible constructors.</returns>
    public static ICollection<EligibleConstructor> Create(ConstructorOptions options)
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
                : [new EligibleConstructor(constructor, options)];
        }

        var constructors = options.TargetType.GetTypeInfo().DeclaredConstructors;
        if (options.NumberOfParameters.HasValue)
            constructors = constructors.Where(c => c.GetParameters().Length == options.NumberOfParameters.Value);

        // sort the constructors by the number of parameters, highest to lowest
        constructors = constructors.OrderByDescending(c => c.GetParameters().Length);

        return constructors.Select(ctor => new EligibleConstructor(ctor, options)).ToList();
    }

    /// <summary>
    /// Check if the collection of constructors has a single empty constructor.
    /// </summary>
    /// <param name="constructors">The collection of constructors.</param>
    /// <param name="eligible">The single empty constructor.</param>
    /// <returns>
    ///     True if the collection has a single empty constructor; otherwise, false.
    /// </returns>
    public static bool HasSingleEmptyConstructor(ICollection<EligibleConstructor> constructors, 
        [NotNullWhen(true)] out EligibleConstructor? eligible)
    {
        if (constructors.Count != 1)
        {
            eligible = null;
            return false;
        }

        eligible = constructors.Single();
        return eligible.Info.GetParameters().Length == 0;
    }


    private ConstructorResolution? resolution;

    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="EligibleConstructor"/>.
    /// </para>
    /// </summary>
    /// <param name="constructorInfo">The constructor information.</param>
    /// <param name="constructorOptions">The options.</param>
    public EligibleConstructor(ConstructorInfo constructorInfo, ConstructorOptions constructorOptions)
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
    ///     Gets the resolution of the constructor.
    /// </para>
    /// </summary>
    public ConstructorResolution Resolution
    {
        get => resolution ?? new ConstructorResolution(Info, new("The resolution is not defined."));
        set => resolution = value;
    }
}
