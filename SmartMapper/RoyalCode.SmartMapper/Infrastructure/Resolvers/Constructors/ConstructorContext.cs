
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.Constructors;

public class ConstructorContext
{
    private readonly List<InnerSourcePropertiesGroup> groups = new();


    public ConstructorContext(ConstructorRequest request)
	{
        TargetParameters = request.CreateTargetParameters();
        AvailableSourceProperties = request.CreateAvailableSourceProperties(
            ResolutionStatus.MappedToConstructor, 
            groups);
    }

    public IEnumerable<TargetParameter> TargetParameters { get; }

    public IEnumerable<AvailableSourceProperty> AvailableSourceProperties { get; }
}
