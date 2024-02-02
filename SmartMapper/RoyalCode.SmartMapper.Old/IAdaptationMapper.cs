namespace RoyalCode.SmartMapper;

public interface IAdaptationMapper
{
    TTo Map<TTo>(object from, Type? type = null);

    TTo Map<TFrom, TTo>(TFrom from);

    void Map<TSource, TTarget>(TSource source, TTarget target);
}