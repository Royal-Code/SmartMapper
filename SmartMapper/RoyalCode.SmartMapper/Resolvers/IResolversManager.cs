namespace RoyalCode.SmartMapper.Resolvers;

public interface IResolversManager
{
    IEnumerable<SourceNameHandler> SourceNameHandlers { get; }

    IEnumerable<TargetNameHandler> TargetNameHandlers { get; }
}

public class NameHandlerBase
{
    public string Sufix { get; set; }

    public string Prefix { get; set; }
}

public class SourceNameHandler
{
    
}

public class TargetNameHandler
{
    
}