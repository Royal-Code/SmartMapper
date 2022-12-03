using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Core;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Activations;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.Adapters;

/// <summary>
/// <para>
///     Arch Context for the adapter resolution process.
/// </para>
/// </summary>
public class AdapterContext
{
    private readonly SourceProperty[] properties;

    public AdapterContext(AdapterOptions adapterOptions, ResolutionConfiguration configurations)
    {
        Options = adapterOptions;
        Configuration = configurations;

        properties = adapterOptions.CreateSourceProperties();
    }

    public AdapterOptions Options { get; }

    public ResolutionConfiguration Configuration { get; }

    [Obsolete("Use the property 'Options' instead.")]
    public Type SourceType => Options.SourceType;

    [Obsolete("Use the property 'Options' instead.")]
    public Type TargetType => Options.TargetType;

    [Obsolete("Use the property 'Options' instead.")]
    public ConstructorOptions GetConstructorOptions() => Options.TargetOptions.GetConstructorOptions();

    public IEnumerable<SourceProperty> GetPropertiesByStatus(params ResolutionStatus[] statuses)
    {
        return properties
            .Where(p => !p.Resolved)
            .Where(p => statuses.Contains(p.Options.ResolutionStatus));
    }

    public IEnumerable<SourceProperty> GetPropertiesUnresolved()
        => properties.Where(p => p.Options.ResolutionStatus == ResolutionStatus.Undefined);

    public IEnumerable<SourceProperty> GetProperties() => properties;

    public void UseActivator(ActivationResolution resolution)
    {
        throw new NotImplementedException();
    }


    public bool Validate(out IEnumerable<string> failures)
    {
        // Valida se todas propriedades estão concluídas.

        // Em caso de algumas propriedades não poderem ser resolvidas, gerar falhas.

        // OBS.: em vez de validar, poderia ter um GetResolution igualmente ao ConstructorResolutionContext

        throw new NotImplementedException();
    }
}