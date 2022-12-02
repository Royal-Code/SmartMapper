using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;
using RoyalCode.SmartMapper.Infrastructure.Attributes;
using RoyalCode.SmartMapper.Infrastructure.Core;
using RoyalCode.SmartMapper.Resolutions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

/// <summary>
/// <para>
///     Arch Resolver for Adapters.
/// </para>
/// <para>
///     Responsible for resolving the adapter type mapping between the source type and the destination type.
/// </para>
/// <para>
///     Once solved, the result is stored internally.
/// </para>
/// </summary>
public class AdapterResolver
{
    private readonly Dictionary<MapKey, object> resolutions = new();
    
    public AdapterResolution Resolve(AdapterRequest request)
    {
        if (request.Configuration.Cache.TryGetAdapter(request.Key, out var resolution))
            return resolution;

        var context = request.CreateContext();

        var activationResolver = request.Configuration.GetResolver<ActivationResolver>();
        var activationRequest = context.CreateActivationRequest();
        var activationResolution = activationResolver.Resolve(activationRequest);

        if (!activationResolution.Resolved)
            return CreateFailure(request, activationResolution.FailureMessages);
        else
            context.UseActivator(activationResolution);

        var callerResolver = request.Configuration.GetResolver<CallerResolver>();
        // seguir com a lógica de caller


        var setterResolver = request.Configuration.GetResolver<SetterResolver>();
        // seguir com a lógica de setter;


        if(context.Validate(out var failures))
            return CreateFailure(request, failures);
        
        // criar resolução de sucesso
        
        throw new NotImplementedException();
    }

    public bool TryResolve(AdapterRequest context, [NotNullWhen(true)] out AdapterResolution? resolution)
    {
        // deve ser executado uma série de try para cada etapa ?
        
        throw new NotImplementedException();
    }

    private AdapterResolution CreateFailure(AdapterRequest context, IEnumerable<string> failures)
    {
        var resolution = new AdapterResolution()
        {
            Resolved = false,
            FailureMessages = new[] 
                    { $"Adapter cannot be created from type {context.Key.SourceType} to type {context.Key.TargetType}" }
                .Concat(failures)
        };
        
        context.Configuration.Cache.Store(context.Key, resolution);

        return resolution;
    }
    
    // TODO: Daqui para baixo não deverá ficar por aqui
    public IAdapterResolution<TSource, TTarget> GetResolution<TSource, TTarget>()
    {
        var key = new MapKey(typeof(TSource), typeof(TTarget));
        
        if (resolutions.TryGetValue(key, out var resolution))
            return (IAdapterResolution<TSource, TTarget>) resolution;
        
        resolution = CreateResolution(key);
        resolutions.Add(key, resolution);
        
        return (IAdapterResolution<TSource, TTarget>)resolution;
    }

    private object CreateResolution(MapKey key)
    {
        var context = new AdapterRequest(key, null);
        
        
        throw new NotImplementedException();
    }
}