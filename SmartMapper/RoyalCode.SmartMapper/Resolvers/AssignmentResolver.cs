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
        // TODO: refactor here, to use new methods.
        foreach (var assignmentStrategy in resolversManager.AssignmentStrategies)
        {
            if (assignmentStrategy.TryResolve(options, sourceType, targetType))
                return;
        }
    }
}

public interface IAssignmentStrategy
{
    bool TryResolve(AssignmentOptions options, Type sourceType, Type targetType);

    ValueAssignmentStrategy Strategy { get; }

    bool CanResolve(Type sourceType, Type targetType);

    void GetValueExpression(); // ??? how will the expression be created?
}

public class DirectSetValueStrategy : IAssignmentStrategy
{
    public ValueAssignmentStrategy Strategy => ValueAssignmentStrategy.Direct;

    public bool CanResolve(Type sourceType, Type targetType)
    {
        throw new NotImplementedException();
    }

    public void GetValueExpression()
    {
        throw new NotImplementedException();
    }

    public bool TryResolve(AssignmentOptions options, Type sourceType, Type targetType)
    {
        if (!targetType.IsAssignableFrom(sourceType))
            return false;

        options.Strategy = ValueAssignmentStrategy.Direct;
        return true;
    }
}