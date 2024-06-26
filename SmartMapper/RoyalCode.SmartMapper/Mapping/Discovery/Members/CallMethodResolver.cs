using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

public sealed class CallMethodResolver : MemberResolver
{
    public CallMethodResolver(AvailableSourceProperty sourceProperty, IEnumerable<AvailableMethod> availableMethods)
    {
        SourceProperty = sourceProperty;
        AvailableMethods = availableMethods;
    }

    public AvailableSourceProperty SourceProperty { get; }

    public IEnumerable<AvailableMethod> AvailableMethods { get; }

    public override MemberDiscoveryResult CreateResolution(MapperConfigurations configurations)
    {
        
        
        throw new NotImplementedException();
    }
}

