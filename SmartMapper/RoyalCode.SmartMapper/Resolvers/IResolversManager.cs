namespace RoyalCode.SmartMapper.Resolvers;

public interface IResolversManager
{
    IEnumerable<SourceNameHandler> SourceNameHandlers { get; }

    IEnumerable<TargetNameHandler> TargetNameHandlers { get; }

    IEnumerable<IAssignmentStrategy> AssignmentStrategies { get; }
}

public class SourceNameHandler : NameHandlerBase
{
    
}

public class TargetNameHandler : NameHandlerBase
{
    
}