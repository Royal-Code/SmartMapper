
namespace RoyalCode.SmartMapper.Infrastructure.Attributes;

/// <summary>
/// <para>
///     A simple attribute to mark a class as a principle resolution.
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class ArchResolutionAttribute : Attribute { }
