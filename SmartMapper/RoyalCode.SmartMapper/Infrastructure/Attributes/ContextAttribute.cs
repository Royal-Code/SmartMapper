
namespace RoyalCode.SmartMapper.Infrastructure.Attributes;

/// <summary>
/// <para>
///     A simple attribute to mark a class as a context of a resolution.
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class ContextAttribute : Attribute { }
