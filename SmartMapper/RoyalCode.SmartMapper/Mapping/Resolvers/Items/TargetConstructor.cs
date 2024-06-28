using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace RoyalCode.SmartMapper.Mapping.Resolvers.Items;

/// <summary>
/// A container of a target constructor.
/// </summary>
public sealed class TargetConstructor : TargetInvocableBase
{
    /// <summary>
    /// Creates a new list of the target constructor for the type.
    /// </summary>
    /// <param name="type">The target type.</param>
    /// <param name="paramterTypes">Optional, the parameters types of the constructor.</param>
    /// <param name="numberOfParameters">Optional, the number of parameters that the constructor must have.</param>
    /// <returns>A new list of <see cref="TargetConstructor"/>.</returns>
    public static IReadOnlyCollection<TargetConstructor> Create(Type type, Type[]? paramterTypes, int? numberOfParameters)
    {
        if (paramterTypes is not null)
        {
            var constructor = type.GetTypeInfo().GetConstructor(
                BindingFlags.Instance | BindingFlags.Public,
                null,
                paramterTypes,
                null);

            return constructor is null
                ? Array.Empty<TargetConstructor>()
                : [new TargetConstructor(constructor, TargetParameter.Create(constructor))];
        }

        var constructors = type.GetTypeInfo().DeclaredConstructors;
        if (numberOfParameters.HasValue)
            constructors = constructors.Where(c => c.GetParameters().Length == numberOfParameters.Value);

        constructors = constructors.OrderByDescending(c => c.GetParameters().Length);

        List<TargetConstructor> list = [];
        foreach(var ctor in constructors)
        {
            var parms = TargetParameter.Create(ctor);
            list.Add(new TargetConstructor(ctor, parms));
        }
        return list;
    }

    /// <summary>
    /// Check if the collection of constructors has a single empty constructor.
    /// </summary>
    /// <param name="constructors">The collection of constructors.</param>
    /// <param name="constructor">The single empty constructor.</param>
    /// <returns>
    ///     True if the collection has a single empty constructor; otherwise, false.
    /// </returns>
    public static bool HasSingleEmptyConstructor(IReadOnlyCollection<TargetConstructor> constructors, 
        [NotNullWhen(true)] out TargetConstructor? constructor)
    {
        if (constructors.Count is not 1)
        {
            constructor = null;
            return false;
        }

        constructor = constructors.Single();
        return constructor.Parameters.Count is 0;
    }

    /// <summary>
    /// Creates a new <see cref="TargetConstructor"/>.
    /// </summary>
    /// <param name="constructorInfo">The constructor.</param>
    /// <param name="parameters">The parameters.</param>
    public TargetConstructor(ConstructorInfo constructorInfo, IReadOnlyCollection<TargetParameter> parameters) 
        : base(parameters)
    {
        ConstructorInfo = constructorInfo;
    }

    /// <summary>
    /// The Constructor.
    /// </summary>
    public ConstructorInfo ConstructorInfo { get; }

    
}