
namespace RoyalCode.SmartMapper.Infrastructure.Attributes;

/// <summary>
/// <para>
///    A simple attribute to mark a class as a member (property, parameter) of a context that needs to be resolved.
/// </para>
/// <para>
///     When used in a class, it means that the class represents a member that needs to be resolved.
/// </para>
/// <para>
///     When used in an archcontext property, 
///     it means that the property contains the objects that represent members to be resolved.
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class ResolvableMemberAttribute : Attribute { }
