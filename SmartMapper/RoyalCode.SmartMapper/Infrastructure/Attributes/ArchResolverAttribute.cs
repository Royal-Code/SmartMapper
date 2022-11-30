
namespace RoyalCode.SmartMapper.Infrastructure.Attributes;

/// <summary>
/// <para>
///     A simple attribute to mark a class as a resolver that is a principle of a resolution.
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class ArchResolverAttribute : ResolverAttribute { }