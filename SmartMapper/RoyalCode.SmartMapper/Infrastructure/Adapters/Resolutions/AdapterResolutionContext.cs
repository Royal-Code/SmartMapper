using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;

public class AdapterResolutionContext
{
    private readonly AdapterOptions adapterOptions;
    private readonly AdapterConfigurations configurations;
    private readonly SourceProperty[] properties;

    public AdapterResolutionContext(AdapterOptions adapterOptions, AdapterConfigurations configurations)
    {
        this.adapterOptions = adapterOptions;
        this.configurations = configurations;

        var infos = adapterOptions.SourceType.GetTypeInfo().GetRuntimeProperties().ToArray();
        properties = new SourceProperty[infos.Length];
        for (int i = 0; i < infos.Length; i++)
        {
            var info = infos[i];
            var preConfigured = adapterOptions.SourceOptions.TryGetPropertyOptions(info.Name, out var option);
            properties[i] = new SourceProperty()
            {
                PropertyInfo = info,
                PreConfigured = preConfigured,
                Options = option ?? new PropertyOptions(info)
            };
        }
        
    }

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

    public AssignmentStrategyResolver GetAssignmentStrategyResolver()
    {
        return configurations.GetResolver<AssignmentStrategyResolver>();
    }
}