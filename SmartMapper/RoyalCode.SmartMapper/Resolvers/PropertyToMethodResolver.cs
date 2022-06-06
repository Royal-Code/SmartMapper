using System.Reflection;
using RoyalCode.SmartMapper.Configurations;

namespace RoyalCode.SmartMapper.Resolvers;

/// <summary>
/// <para>
///     This class must resolve the mapping of a property of a source type to a method of the target type. 
/// </para>
/// </summary>
public class PropertyToMethodResolver
{

    public void Resolve(PropertyToMethodOptions options, Type targetType)
    {
        
    }
}

public class PropertyToParameterResolver
{
    private readonly IResolversManager resolversManager;

    public PropertyToParameterResolver(IResolversManager resolversManager)
    {
        this.resolversManager = resolversManager;
    }

    public void Resolve(PropertyToMethodOptions options, IEnumerable<PropertyInfo> properties)
    {
        var method = options.TargetMethod 
            ?? throw new ArgumentException("Target method not resolved", nameof(options));
        var parameters = method.GetParameters();
        
        foreach (var property in properties)
        {
            PropertyToParameterOptions parameterOptions = options.GetParameterOptions(property);

            // for each property, try read the PropertyToParameterOptions from the options,
            // if exists, check if the parameter was specified and try resolve the parameter type.
            // otherwise, try find a parameter and then resolve the type.

            SourceNameHandler? matchNameHandler = null;
            
            if (parameterOptions.ParameterInfo is null)
            {
                var matchParameter = parameters.FirstOrDefault(p => p.Name == property.Name);
                if (matchParameter is null)
                {
                    // match with the source name handler
                    foreach (var sourceNameHandler in resolversManager.SourceNameHandlers)
                    {
                        foreach (var name in sourceNameHandler.GetNames(property.Name))
                        {
                            matchParameter = parameters.FirstOrDefault(p => p.Name == property.Name);
                            if (matchParameter is not null)
                            {
                                // here the handler is validated, and it can fail.
                                // how check this?
                                sourceNameHandler.Validate(options.SourceProperty, matchParameter.ParameterType);
                                matchNameHandler = sourceNameHandler;
                                break;
                            }
                        }
                        if (matchParameter is not null)
                        {
                            break;
                        }
                    }
                }
                
                // here we have the method parameter and maybe the match name handler
                // now is time to try resolve the type set kind.
            }
        }
    }
}