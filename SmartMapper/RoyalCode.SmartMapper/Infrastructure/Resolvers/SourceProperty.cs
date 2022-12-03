using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers;

/// <summary>
/// Represents a property of the source type as a resolvable member.
/// </summary>
public class SourceProperty : ResolvableMember<PropertyInfo, PropertyOptions>
{
    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="SourceProperty"/>.
    /// </para>
    /// </summary>
    /// <param name="propertyInfo">The property information.</param>
    /// <param name="preConfigured">The pre-configured options.</param>
    /// <param name="options">The options.</param>
    public SourceProperty(
        PropertyInfo propertyInfo,
        bool preConfigured,
        PropertyOptions options)
        : base(propertyInfo, options, preConfigured)
    { }

    // Ver isso aqui, é estranho. Ao atribuir uma resolution, deverá ser atribuído o resolved.
    private SourcePropertyResolution Resolution { get; } = new();

    /// <inheritdoc />
    public override string GetMemberName() => MemberInfo.Name;
}
