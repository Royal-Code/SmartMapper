using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;

/// <summary>
/// <para>
///     Esta resolução tenta obter o melhor construtor possível para a classe de destino.
/// </para>
/// <para>
///     Ela terá uma resolution <see cref="ConstrutorParametersResolution"/>
///     para construtor elegível da classe de destino.
/// </para>
/// <para>
///     Uma construtor elegível deve ser público e atender os requisitos confirados nas opções
///     <see cref="ConstructorOptions"/>
/// </para>
/// </summary>
public class BestConstrutorResolution
{
    private readonly ConstructorOptions options;

    public BestConstrutorResolution(ConstructorOptions options)
    {
        this.options = options;
    }

    public void Resolve()
    {
        var constructors = GetElegibleConstructors();

        if (constructors.Length == 0)
        {
            // registra mensagem de erro
            return;
        }

        var resolutions = constructors.Select(c => new ConstrutorParametersResolution(options, c)).ToList();
       
    }

    internal ConstructorInfo[] GetElegibleConstructors()
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

/// <summary>
/// <para>
///     Esta resolução tenta resolver um construtor, parâmetro por parâmetro.
/// </para>
/// </summary>
public class ConstrutorParametersResolution
{
    private readonly ConstructorOptions options;
    private readonly ConstructorInfo constructorInfo;

    public ConstrutorParametersResolution(ConstructorOptions options, ConstructorInfo constructorInfo)
    {
        this.options = options;
        this.constructorInfo = constructorInfo;
    }
}

public class ConstructorResolver
{
    public void Resolve(AdapterOptions adapterOptions)
    {
        
    }
    
}