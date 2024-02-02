namespace RoyalCode.SmartMapper;

public interface ISelectionMapper
{
    TTo Select<TTo>(object from, Type? type = null);

    TTo Select<TFrom, TTo>(TFrom from);

    //void Select<TSource, TTarget>(TSource source, TTarget target);
}