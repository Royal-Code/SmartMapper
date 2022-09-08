using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;

public class ConstructorResolver
{
    public ConstrutorResolution Resolve(AdapterResolutionContext context)
    {
        var ctorElegible = GetElegibleConstructors(context.GetConstructorOptions());

        // deve ser validado se existe algum ctor elegivel.
        
        var parametersResolutions = ctorElegible.Select(c => new ConstrutorParametersResolver(c))
            .Select(r => r.Resolve(context))
            .ToList();
        
        // se não tiver resoluções com sucesso, registra o erro e encerra.
        
        // filtra as resoluções com sucesso e ordena ao estilo best constructor selector.
        
        // agora deve ser validada as resoluções, se as opções pré-configuradas está atendendo parametros do ctor resolvido?
        
        // elimina os ctor inválidos.
        
        // caso não sobre nenhum, registra o erro de configuração.
        
        // por fim, pega o melhor construtor e gera um sucesso.
        // o resolver do adaptador deverá pegar a resolução do ctor e aplicar os valores corretos nas opções.
        
        throw new NotImplementedException();
    }
    
    internal ConstructorInfo[] GetElegibleConstructors(ConstructorOptions options)
    {
        if (options.ParameterTypes is not null)
        {
            var constructor = options.TargetType.GetTypeInfo().GetConstructor(
                BindingFlags.Instance | BindingFlags.Public,
                null,
                options.ParameterTypes!,
                null);
            
            return constructor is null
                ? Array.Empty<ConstructorInfo>()
                : new[] { constructor };
        }
        
        var constructors = options.TargetType.GetTypeInfo().DeclaredConstructors;
        if (options.NumberOfParameters.HasValue)
            constructors = constructors.Where(c => c.GetParameters().Length == options.NumberOfParameters.Value);

        return constructors.ToArray();
    }
}

public class ConstrutorResolution
{
    
}

/// <summary>
/// <para>
///     Esta resolução tenta resolver um construtor, parâmetro por parâmetro.
/// </para>
/// </summary>
public class ConstrutorParametersResolver
{
    private readonly ConstructorInfo constructorInfo;

    public ConstrutorParametersResolver(ConstructorInfo constructorInfo)
    {
        this.constructorInfo = constructorInfo;
    }

    public ConstrutorParametersResolution Resolve(AdapterResolutionContext context)
    {
        var constructorOptions = context.GetConstructorOptions();
        var properties = context.GetPropertyOptions(
            ResolutionStatus.MappedToConstructor,
            ResolutionStatus.MappedToConstructorParameter);
            

        foreach (var propertyOption in properties)
        {
            
        }
        
        throw new NotImplementedException();
    }
}

public class ConstrutorParametersResolution
{
    
}

public class ParameterResolver
{
    private readonly ParameterInfo parameterInfo;

    public ParameterResolver(ParameterInfo parameterInfo)
    {
        this.parameterInfo = parameterInfo;
    }

    public ParameterResolution Resolve(ToParameterOptionsBase options)
    {
        
        throw new NotImplementedException();
    }
}

public class ParameterResolution
{

}

public class AdapterResolutionContext
{
    private readonly AdapterOptions adapterOptions;
    private readonly PropertyInfo[] propertyInfos;

    public AdapterResolutionContext(AdapterOptions adapterOptions)
    {
        this.adapterOptions = adapterOptions;
        propertyInfos = adapterOptions.SourceType.GetTypeInfo().GetRuntimeProperties().ToArray();
    }

    public ConstructorOptions GetConstructorOptions() => adapterOptions.TargetOptions.GetConstructorOptions();

    public IEnumerable<PropertyOptions> GetPropertyOptions(params ResolutionStatus[] statuses) 
        => adapterOptions.SourceOptions.GetPropertiesByStatus();

    public PropertyInfo[] GetProperties() => propertyInfos;
}