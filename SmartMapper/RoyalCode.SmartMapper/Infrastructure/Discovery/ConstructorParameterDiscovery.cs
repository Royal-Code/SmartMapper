using RoyalCode.SmartMapper.Infrastructure.Naming;
using RoyalCode.SmartMapper.Infrastructure.Resolvers;

namespace RoyalCode.SmartMapper.Infrastructure.Discovery;

public class ConstructorParameterDiscovery
{
    public ConstructorParameterDiscoveryResult Discover(ConstructorParameterDiscoveryContext context)
    {
        var nameHandlers = context.Configuration.NameHandlers.SourceNameHandlers;
        var matches = new LinkedList<ConstructorParameterMatch>();
        
        foreach (var property in context.AvailableProperties)
        {
            var propertyInfo = property.SourceProperty.PropertyInfo;
            var propertyName = propertyInfo.Name;
            
            TargetParameter? matchParameter = null;
            foreach (var sourceNameHandler in nameHandlers)
            {
                foreach (var name in sourceNameHandler.GetNames(propertyName))
                {
                    matchParameter = context.Parameters.FirstOrDefault(p => p.ParameterInfo.Name == name);
                    if (matchParameter is not null)
                    {
                        var namingContext = new NamingContext(propertyInfo,
                            matchParameter.ParameterInfo.ParameterType,
                            context.Configuration);
                            
                        sourceNameHandler.Validate(namingContext);
                            
                        if (namingContext.IsValid)
                        {
                            var match = new ConstructorParameterMatch(
                                property, matchParameter, namingContext.Resolution);

                            matches.AddLast(match);
                                
                            break;
                        }

                        matchParameter = null;
                    }
                }
                if (matchParameter is not null)
                {
                    break;
                }
            }
        }

        return new ConstructorParameterDiscoveryResult(matches);
    }
}