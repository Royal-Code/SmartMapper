using System.Reflection;

namespace RoyalCode.SmartMapper.Mapping.Resolvers.Items;

/// <summary>
/// A container for the target method.
/// </summary>
public sealed class TargetMethod : TargetInvocableBase
{
    /// <summary>
    /// Creates a new list of the target methods.
    /// </summary>
    /// <param name="type">The target type.</param>
    /// <returns>A new list of <see cref="TargetMethod"/>.</returns>
    public static IReadOnlyCollection<TargetMethod> Create(Type type)
    {
        List<TargetMethod> list = new List<TargetMethod>();
        var methods = type.GetTypeInfo()
            .GetRuntimeMethods()
            .Where(m => m is { IsPublic: true, IsStatic: false, IsSpecialName: false });

        foreach(var method in methods)
        {
            var parms = TargetParameter.Create(method);
            list.Add(new TargetMethod(method, parms));
        }

        return list;
    }

    /// <summary>
    /// Creates a new <see cref="TargetMethod"/>;
    /// </summary>
    /// <param name="method">The method.</param>
    /// <param name="parameters">The parameters.</param>
    public TargetMethod(MethodInfo method, IReadOnlyCollection<TargetParameter> parameters) : base(parameters)
    {
        MethodInfo = method;
    }

    /// <summary>
    /// The target method.
    /// </summary>
    public MethodInfo MethodInfo { get; }
}
