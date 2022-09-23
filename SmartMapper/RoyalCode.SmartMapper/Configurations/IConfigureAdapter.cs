using RoyalCode.SmartMapper.Configurations.Adapters;

namespace RoyalCode.SmartMapper.Configurations;

public interface IConfigureAdapter<TSource>
{
    IAdapterOptionsBuilder<TSource, TTarget> To<TTarget>();
}

public interface IConfigureAdapter
{
    IConfigureAdapter<TSource> From<TSource>();
}