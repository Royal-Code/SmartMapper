using RoyalCode.SmartMapper.Infrastructure.Naming;
using RoyalCode.SmartMapper.Infrastructure.Resolvers;
using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Discovery;

public class ParameterDiscovery
{
    public ParameterDiscoveryResult Discover(ParameterDiscoveryRequest request)
    {
        var nameHandlers = request.Configuration.NameHandlers.SourceNameHandlers;
        var matches = new LinkedList<ParameterMatch>();
        
        foreach (var property in request.AvailableProperties)
        {
            var propertyInfo = property.SourceProperty.MemberInfo;
            var propertyName = propertyInfo.Name;
            
            TargetParameter? matchParameter = null;
            foreach (var sourceNameHandler in nameHandlers)
            {
                foreach (var name in sourceNameHandler.GetNames(propertyName))
                {
                    matchParameter = request.Parameters.FirstOrDefault(p => p.MemberInfo.Name == name);
                    if (matchParameter is not null)
                    {
                        var namingContext = new NamingContext(propertyInfo,
                            matchParameter.MemberInfo.ParameterType,
                            request.Configuration);
                            
                        sourceNameHandler.Validate(namingContext);
                            
                        if (namingContext.IsValid)
                        {
                            var match = new ParameterMatch(
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

        return new ParameterDiscoveryResult(matches);
    }
}