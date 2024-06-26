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
    /// <param name="request">The member discovery request.</param>
    /// <param name="names">The name partitions.</param>
    /// <param name="index">The index of the name part to handle.</param>
    /// <param name="resolver">A resolver if the name part was handled successfully.</param>
    /// <returns>True if the name part was handled successfully, otherwise false.</returns>
    public bool Handle(MemberDiscoveryName names, int index, [NotNullWhen(true)] out MemberResolver? resolver);
}

public sealed class MemberDiscoveryName
{
    private readonly IEnumerable<INameHandler> nameHandlers;
    
    public MemberDiscoveryName(MemberDiscoveryRequest request, IEnumerable<INameHandler> nameHandlers)
    {
        this.nameHandlers = nameHandlers;
        Request = request;
        Partitions = new NamePartitions(request.SourceProperty.SourceItem.Options.Property.Name);
    }
    
    public NamePartitions Partitions { get; }
    
    public MemberDiscoveryRequest Request { get; }
    
    public bool HandleNextPart(int index, [NotNullWhen(true)] out MemberResolver? resolver)
    {
        foreach (var handler in nameHandlers)
            if (handler.Handle(this, index, out resolver))
                return true;

        resolver = null;
        return false;
    }
}