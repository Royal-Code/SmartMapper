
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Callers;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.Constructors;

public class ConstructorContext
{

    
	public ConstructorContext(IInvocableRequest request)
	{
        TargetParameters = request.CreateTargetParameters();

        
    }

    public IEnumerable<TargetParameter> TargetParameters { get; }
}
