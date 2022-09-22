using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;

public class ActivationResolver
{
    public ActivationResolution Resolve(AdapterResolutionContext context)
    {
        var ctorElegible = GetElegibleConstructors(context.GetConstructorOptions());

        // deve ser validado se existe algum ctor elegivel.
        
        var construtorResolutions = ctorElegible.Select(c => new ConstrutorResolver(c))
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

public class ActivationResolution
{
    
}

/// <summary>
/// <para>
///     Esta resolução tenta resolver um construtor, parâmetro por parâmetro.
/// </para>
/// </summary>
public class ConstrutorResolver
{
    private readonly ConstructorInfo constructorInfo;
    private readonly List<ConstructorParameterResolver> parameterResolvers;

    public ConstrutorResolver(ConstructorInfo constructorInfo)
    {
        this.constructorInfo = constructorInfo;

        parameterResolvers = constructorInfo.GetParameters()
            .Select(p => new ConstructorParameterResolver(p))
            .ToList();
    }

    public ConstrutorResolution Resolve(AdapterResolutionContext context)
    {
        var properties = context.GetPropertiesByStatus(
            ResolutionStatus.Undefined,
            ResolutionStatus.MappedToConstructor,
            ResolutionStatus.MappedToConstructorParameter);

        var ctorContext = new ConstructorResolutionContext(properties, context);

        parameterResolvers.ForEach(r => r.Resolve(ctorContext));
        
        throw new NotImplementedException();
    }
}

public class ConstructorResolutionContext
{
    private readonly AdapterResolutionContext adapterResolutionContext;
    private readonly IEnumerable<SourceProperty> properties;
    private readonly ConstructorOptions constructorOptions;
    public ConstructorResolutionContext(
        IEnumerable<SourceProperty> properties,
        AdapterResolutionContext adapterResolutionContext)
    {
        this.properties = properties;
        this.adapterResolutionContext = adapterResolutionContext;
        constructorOptions = adapterResolutionContext.GetConstructorOptions();
    }

    public bool TryGetParameterOptionsByName(string name, [NotNullWhen(true)] out ToConstructorParameterOptions? options)
    {
        return constructorOptions.TryGetParameterOptions(name, out options);
    }

    public SourceProperty GetSourceProperty(PropertyInfo propertyInfo)
    {
        var sourceProperty = properties.FirstOrDefault(p => p.PropertyInfo == propertyInfo);
        if (sourceProperty is null)
            throw new InvalidOperationException($"The property '{propertyInfo.Name}' is not a valid source property");

        return sourceProperty;
    }

    public AssignmentStrategyResolver GetAssignmentStrategyResolver()
        => adapterResolutionContext.GetAssignmentStrategyResolver();

}

public class ConstrutorResolution
{
    
}

public class ConstructorParameterResolver
{
    private readonly ParameterInfo parameterInfo;

    public ConstructorParameterResolver(ParameterInfo parameterInfo)
    {
        this.parameterInfo = parameterInfo;
    }

    public ParameterResolution Resolve(ConstructorResolutionContext context)
    {
        if(context.TryGetParameterOptionsByName(parameterInfo.Name!, out var toParameterOptions))
        {
            var propertyOptions = toParameterOptions.ResolvedProperty!;

            var sourceProperty = context.GetSourceProperty(propertyOptions.Property);

            var assignmentResolver = context.GetAssignmentStrategyResolver();
            var assignmentResolution = assignmentResolver.Resolve(
                propertyOptions.Property.PropertyType,
                toParameterOptions.TargetType,
                propertyOptions.AssignmentStrategy);
        }

        throw new NotImplementedException();
    }
}

public class ParameterResolution
{

}

public class SourceProperty
{
    public PropertyInfo PropertyInfo { get; init; }
    
    public bool PreConfigured { get; init; }

    public PropertyOptions Options { get; init; }
    
    public bool Resolved { get; internal set; }
}