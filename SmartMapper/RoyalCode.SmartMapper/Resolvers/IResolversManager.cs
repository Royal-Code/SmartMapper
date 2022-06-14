using RoyalCode.SmartMapper.Resolvers.Assigners;

namespace RoyalCode.SmartMapper.Resolvers;

public interface IResolversManager
{
    IEnumerable<SourceNameHandler> SourceNameHandlers { get; }

    IEnumerable<TargetNameHandler> TargetNameHandlers { get; }

    IEnumerable<IAssignmentStrategy> AssignmentStrategies { get; }

    AdaptResolveResult ResolveAdapter(Type sourceType, Type targetType);
}

public class AdaptResolveResult : ResolveResult
{

}

public class ResolveResult
{
    public bool Success { get; internal set; }
}

public class SourceNameHandler : NameHandlerBase
{
    
}

public class TargetNameHandler : NameHandlerBase
{
    
}