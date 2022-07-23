namespace RoyalCode.SmartMapper.Infrastructure.Core;

/// <summary>
/// <para>
///     A record to identify object to object mappings.
/// </para>
/// </summary>
/// <param name="SourceType">The type of the source object of a mapping.</param>
/// <param name="TargetType">The type of the target object of a mapping.</param>
public record MapKey(Type SourceType, Type TargetType);