using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

/// <summary>
/// <para>
///     A component that can handle a name part and return a resolver.
/// </para>
/// <para>
///     May exist many handlers for different purposes.
///     For example, one handler for properties, another for methods, etc.
/// </para>
/// </summary>
public interface INameHandler
{
    /// <summary>
    /// Handle the name part and return a resolver.
    /// </summary>
    /// <param name="context">A context to discovery the member.</param>
    /// <param name="index">The index of the name part to handle.</param>
    /// <param name="resolver">A resolver if the name part was handled successfully.</param>
    /// <returns>True if the name part was handled successfully, otherwise false.</returns>
    public bool Handle(MemberDiscoveryContext context, int index, [NotNullWhen(true)] out MemberResolver? resolver);
}
