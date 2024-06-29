using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;
using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

public sealed class MemberDiscoveryContext
{
    private readonly IEnumerable<INameHandler> nameHandlers;
    
    public MemberDiscoveryContext(MemberDiscoveryRequest request, IEnumerable<INameHandler> nameHandlers)
    {
        this.nameHandlers = nameHandlers;
        Request = request;
        Partitions = new NamePartitions(request.SourceProperty.SourceProperty.Options.Property.Name);
    }
    
    private MemberDiscoveryContext(
        MemberDiscoveryRequest request, 
        IEnumerable<INameHandler> nameHandlers, 
        NamePartitions partitions)
    {
        this.nameHandlers = nameHandlers;
        Request = request;
        Partitions = partitions;
    }

    public NamePartitions Partitions { get; }
    
    public MemberDiscoveryRequest Request { get; }
    
    public MemberDiscoveryContext Inner(TargetProperty targetProperty)
    {
        var innerRequest = new MemberDiscoveryRequest(
            Request.Configurations,
            Request.SourceProperty,
            targetProperty.InnerMethods,
            targetProperty.InnerProperties);

        return new MemberDiscoveryContext(innerRequest, nameHandlers, Partitions);
    }

    public bool HandleNextPart(int index, [NotNullWhen(true)] out MemberResolver? resolver)
    {
        foreach (var handler in nameHandlers)
            if (handler.Handle(this, index, out resolver))
                return true;

        resolver = null;
        return false;
    }
}