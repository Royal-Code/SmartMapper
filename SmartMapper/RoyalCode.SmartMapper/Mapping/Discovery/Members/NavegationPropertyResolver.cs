using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Core.Discovery.Members;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

#pragma warning disable S4487 // member not used
#pragma warning disable CS1591 // xml doc

public sealed class NavegationPropertyResolver : MemberResolver
{
    private readonly MemberDiscoveryRequest request;
    private readonly AvailableProperty property;
    private readonly NamePartitions names;
    private readonly int nameIndex;

    public NavegationPropertyResolver(
        MemberDiscoveryRequest request,
        AvailableProperty property,
        NamePartitions names,
        int nameIndex)
    {
        this.request = request;
        this.property = property ?? throw new ArgumentNullException(nameof(property));
        this.names = names ?? throw new ArgumentNullException(nameof(names));
        this.nameIndex = nameIndex;
    }

    public override MemberResolution CreateResolution(MapperConfigurations configurations)
    {


        throw new NotImplementedException();
    }
}

