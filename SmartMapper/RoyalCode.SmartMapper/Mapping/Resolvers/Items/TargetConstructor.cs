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
    /// <returns>A new list of <see cref="TargetConstructor"/>.</returns>
    public static IReadOnlyCollection<TargetConstructor> Create(Type type)
    {
        List<TargetConstructor> list = [];
        foreach(var ctor in type.GetTypeInfo().DeclaredConstructors)
        {
            var parms = TargetParameter.Create(ctor);
            list.Add(new TargetConstructor(ctor, parms));
        }
        return list;
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