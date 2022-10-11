using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;
using RoyalCode.SmartMapper.Infrastructure.Core;
using RoyalCode.SmartMapper.Infrastructure.Naming;

namespace RoyalCode.SmartMapper.Infrastructure.Discovery;

public class ConstructorParameterDiscovery
{
    // ConstructorParameterMatch
    
    public ConstructorParameterDiscoveryResult Discover(ConstructorParameterDiscoveryContext context)
    {
        var nameHandlers = context.Configuration.NameHandlers.SourceNameHandlers;
        var matches = new LinkedList<ConstructorParameterMatch>();
        
        foreach (var property in context.SourceProperties)
        {
            var propertyName = property.PropertyInfo.Name;
            
            if (property.Options.ResolutionStatus == ResolutionStatus.MappedToConstructor)
            {

            }
            else
            {
                if (property.Options.ResolutionStatus == ResolutionStatus.MappedToConstructorParameter)
                {
                    // pegar options pq é pré-configurado
                    var opt = property.Options.ResolutionOptions as ToConstructorParameterOptions;
                    
                }
                
                // match with the source name handler
                TargetParameter? matchParameter = null;
                SourceNameHandler? matchNameHandler = null;
                foreach (var sourceNameHandler in nameHandlers)
                {
                    foreach (var name in sourceNameHandler.GetNames(propertyName))
                    {
                        matchParameter = context.Parameters.FirstOrDefault(p => p.ParameterInfo.Name == name);
                        if (matchParameter is not null)
                        {
                            var namingContext = new NamingContext(property.PropertyInfo,
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
        }

        return new ConstructorParameterDiscoveryResult(matches);
    }
}