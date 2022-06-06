using RoyalCode.SmartMapper.Configurations;

namespace RoyalCode.SmartMapper.Resolvers;

public class AssignmentResolver
{
    private readonly IResolversManager resolversManager;


    public AssignmentResolver(IResolversManager resolversManager)
    {
        this.resolversManager = resolversManager;
    }
    
    public void Resolve(AssignmentOptions options, Type sourceType, Type targetType)
    {
        
        
        throw new NotImplementedException();
    }
}