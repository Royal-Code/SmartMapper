using RoyalCode.SmartMapper.Configurations;
using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RoyalCode.SmartMapper.Infrastructure.Configurations;

/// <summary>
/// <para>
///     Contains all configurations used by <see cref="SmartMapper"/>.
/// </para>
/// </summary>
public class MappingConfiguration
{
    private readonly AdaptersOptions adaptersOptions;

    public MappingConfiguration()
    {
        adaptersOptions = new AdaptersOptions();
        Configure = new InternalConfigure(adaptersOptions);
    }

    /// <summary>
    /// <para>
    ///     Start configuration for <see cref="SmartMapper"/>.
    /// </para>
    /// </summary>
    public IConfigure Configure { get; }

    /// <summary>
    /// Conjunto de opções configuradas manualmente para adaptadores.
    /// </summary>
    public IAdaptersOptions AdaptersOptions => adaptersOptions;
    
    // TODO: Implementar ISelectorsOptions
    public ISelectorsOptions SelectorsOptions { get; }

    private class InternalConfigure : IConfigure
    {
        private readonly AdaptersOptions adaptersOptions;

        public InternalConfigure(AdaptersOptions adaptersOptions)
        {
            this.adaptersOptions = adaptersOptions;
            Adapter = new InternalConfigureAdapter(adaptersOptions);
        }

        public IConfigureAdapter Adapter { get; }
        
        public IConfigureMapper Mapper { get; }
        
        public IConfigureSelector Selector { get; }
        
        public IConfigureSpecifier Specifier { get; }
    }

    private class InternalConfigureAdapter : IConfigureAdapter
    {
        private readonly AdaptersOptions adaptersOptions;

        public InternalConfigureAdapter(AdaptersOptions adaptersOptions)
        {
            this.adaptersOptions = adaptersOptions;
        }

        public IConfigureAdapter<TSource> From<TSource>() => new InternalConfigureAdapter<TSource>(adaptersOptions);
    }

    private class InternalConfigureAdapter<TSource> : IConfigureAdapter<TSource>
    {
        private readonly AdaptersOptions adaptersOptions;

        public InternalConfigureAdapter(AdaptersOptions adaptersOptions)
        {
            this.adaptersOptions = adaptersOptions;
        }

        public IAdapterOptionsBuilder<TSource, TTarget> To<TTarget>() 
            => adaptersOptions.GetOptionsBuilder<TSource, TTarget>();
    }
}