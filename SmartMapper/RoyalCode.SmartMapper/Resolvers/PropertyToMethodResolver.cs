using System.Reflection;
using RoyalCode.SmartMapper.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RoyalCode.SmartMapper.Resolvers;

/// <summary>
/// <para>
///     This class must resolve the mapping of a property of a source type to a method of the target type. 
/// </para>
/// </summary>
public class PropertyToMethodResolver
{

    public void Resolve(ToMethodOptions options, Type targetType)
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

    public void Resolve(ToMethodOptions options, IEnumerable<PropertyInfo> properties)
    {
        var method = options.MethodOptions.Method 
            ?? throw new ArgumentException("Target method not resolved", nameof(options));
        var parameters = method.GetParameters();
        
        foreach (var property in properties)
        {
            var exists = options.MethodOptions.TryGetParameterOptions(property, out var parameterOptions);

            // for each property, try read the PropertyToParameterOptions from the options,
            // if exists, check if the parameter was specified and try resolve the parameter type.
            // otherwise, try find a parameter and then resolve the type.

            SourceNameHandler? matchNameHandler = null;
            
            if (parameterOptions.Parameter is null)
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
                                sourceNameHandler.Validate(options.ResolvedProperty!.Property, matchParameter.ParameterType);
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