using RoyalCode.SmartMapper.Extensions;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

/// <summary>
/// <para>
///     Arch Context for the adapter resolution process.
/// </para>
/// </summary>
public class AdapterResolutionContext
{
    private readonly AdapterOptions adapterOptions;
    private readonly SourceProperty[] properties;

    public AdapterResolutionContext(AdapterOptions adapterOptions, ResolutionConfiguration configurations)
    {
        this.adapterOptions = adapterOptions;
        Configuration = configurations;

        var infos = adapterOptions.SourceType.GetReadableProperties();
        properties = new SourceProperty[infos.Length];
        for (int i = 0; i < infos.Length; i++)
        {
            var info = infos[i];
            var preConfigured = adapterOptions.SourceOptions.TryGetPropertyOptions(info.Name, out var option);
            properties[i] = new(info, preConfigured, option ?? new PropertyOptions(info));
        }
    }

    public ResolutionConfiguration Configuration { get; }

    public Type SourceType => adapterOptions.SourceType;

    public Type TargetType => adapterOptions.TargetType;

    public ConstructorOptions GetConstructorOptions() => adapterOptions.TargetOptions.GetConstructorOptions();

    public IEnumerable<SourceProperty> GetPropertiesByStatus(params ResolutionStatus[] statuses)
    {
        return properties
            .Where(p => !p.Resolved)    
            .Where(p => statuses.Contains(p.Options.ResolutionStatus));
    }

    public IEnumerable<SourceProperty> GetPropertiesUnresolved()
        => properties.Where(p => p.Options.ResolutionStatus == ResolutionStatus.Undefined);

    public IEnumerable<SourceProperty> GetProperties() => properties;

    public bool Validate(out IEnumerable<string> failures)
    {
        // Valida se todas propriedades estão concluídas.
        
        // Em caso de algumas propriedades não poderem ser resolvidas, gerar falhas.
        
        // OBS.: em vez de validar, poderia ter um GetResolution igualmente ao ConstructorResolutionContext
        
        throw new NotImplementedException();
    }
}