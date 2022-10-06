using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Discovery;

public class ConstructorParameterDiscovery
{
    // ConstructorParameterMatch
    
    public ConstructorParameterDiscovered Discover(ConstructorParameterDiscoveryContext context)
    {
        var nameHandlers = context.Configuration.NameHandlers.SourceNameHandlers;
        
        foreach (var property in context.SourceProperties)
        {
            var propertyName = property.PropertyInfo.Name;
            
            if (property.Options.ResolutionStatus == ResolutionStatus.MappedToConstructorParameter)
            {

            }
            else
            {
                if (property.Options.ResolutionStatus == ResolutionStatus.MappedToConstructorParameter)
                {
                    // pegar options pq é pré-configurado
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
                            if (sourceNameHandler.Validate(property.PropertyInfo,
                                    matchParameter.ParameterInfo.ParameterType))
                            {
                                // TODO: aqui deve ser obtido um componente que irá configurar a atribuíção.
                                matchNameHandler = sourceNameHandler;
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
                
                // se temos match, retornamos um objeto de sucesso.
            }
        }

        return new ConstructorParameterDiscovered();
    }
    
    
}

public record ConstructorParameterDiscoveryContext(
    IEnumerable<SourceProperty> SourceProperties,
    IEnumerable<TargetParameter> Parameters,
    ResolutionConfiguration Configuration);
    
public record ConstructorParameterDiscovered();