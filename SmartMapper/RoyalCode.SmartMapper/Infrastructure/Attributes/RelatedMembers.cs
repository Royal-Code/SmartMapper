
namespace RoyalCode.SmartMapper.Infrastructure.Attributes;

/// <summary>
/// <para>
///     A simple attribute to mark a class, or property, or field as a member to be resolved.
/// </para>
/// <para>
///     The members can be related to source or target types.
/// </para>
/// <para>
///     When used in a class,
///     it means that the class represents a member that needs to be resolved,
///     or that can be used to resolve other members.
/// </para>
/// <para>
///     When used in a property or field,
///     it means that the property or field contains the objects that represent members to be resolved.
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
internal class RelatedMemberAttribute : Attribute { }


/// <summary>
/// <para>
///     A <see cref="RelatedMemberAttribute"/> that marks a class, or property, or field as a source member.
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
internal class SourceRelatedMemberAttribute : RelatedMemberAttribute { }


/// <summary>
/// <para>
///     A <see cref="RelatedMemberAttribute"/> that marks a class, or property, or field as a target member.
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
internal class TargetRelatedMemberAttribute : RelatedMemberAttribute { }