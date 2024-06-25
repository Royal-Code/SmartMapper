using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Core.Discovery.Members;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

public sealed class CallMethodResolver : MemberResolver
{
    public static CallMethodResolver Create(AvailableSourceProperty sourceProperty, IEnumerable<AvailableMethod> availableMethods)
    {
        return new(sourceProperty, availableMethods);
    }

    private CallMethodResolver(AvailableSourceProperty sourceProperty, IEnumerable<AvailableMethod> availableMethods)
    {
        SourceProperty = sourceProperty;
        AvailableMethods = availableMethods;
    }

    public AvailableSourceProperty SourceProperty { get; }

    public IEnumerable<AvailableMethod> AvailableMethods { get; }

    public override MemberResolution CreateResolution(MapperConfigurations configurations)
    {
        
        
        throw new NotImplementedException();
    }
}

