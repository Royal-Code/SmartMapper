using RoyalCode.SmartMapper.Configurations.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

namespace RoyalCode.SmartMapper.Configurations;

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

    // talvez essa interface pode ser removida, porque podemos criar um método para obter as opções de adaptadores.
    public IAdaptersOptions AdaptersOptions => adaptersOptions;

    public interface IConfigure
    {
        IConfigureAdapter Adapter { get; }
    }

    public interface IConfigureAdapter
    {
        IConfigureAdapter<TSource> From<TSource>();
    }

    public interface IConfigureAdapter<TSource>
    {
        IAdapterOptionsBuilder<TSource, TTarget> To<TTarget>();
    }

    private class InternalConfigure : IConfigure
    {
        private readonly AdaptersOptions adaptersOptions;

        public InternalConfigure(AdaptersOptions adaptersOptions)
        {
            this.adaptersOptions = adaptersOptions;
            Adapter = new InternalConfigureAdapter(adaptersOptions);
        }

        public IConfigureAdapter Adapter { get; }
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