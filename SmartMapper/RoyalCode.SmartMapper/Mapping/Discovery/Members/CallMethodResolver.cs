using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;
using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

public sealed class CallMethodResolver : MemberResolver
{
    public CallMethodResolver(AvailableSourceProperty sourceProperty, IReadOnlyCollection<TargetMethod> targetMethods)
    {
        SourceProperty = sourceProperty;
        TargetMethods = targetMethods;
    }

    public AvailableSourceProperty SourceProperty { get; }

    public IReadOnlyCollection<TargetMethod> TargetMethods { get; }

    public override MemberDiscoveryResult CreateResolution(MapperConfigurations configurations)
    {
        
        
        throw new NotImplementedException();
    }
}

