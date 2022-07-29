
namespace RoyalCode.SmartMapper;

public class SmartMapper : IAdaptationMapper, ISelectionMapper, IFilterSpecifierMapper
{
    public TTo Map<TTo>(object from, Type? type = null)
    {
        throw new NotImplementedException();
    }

    public TTo Map<TFrom, TTo>(TFrom from)
    {
        throw new NotImplementedException();
    }

    public void Map<TSource, TTarget>(TSource source, TTarget target)
    {
        throw new NotImplementedException();
    }

    public TTo Select<TTo>(object from, Type? type = null)
    {
        throw new NotImplementedException();
    }

    public TTo Select<TFrom, TTo>(TFrom from)
    {
        throw new NotImplementedException();
    }

    public void Select<TSource, TTarget>(TSource source, TTarget target)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TModel> Specify<TModel>(IQueryable<TModel> query, object filter, Type? type = null)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TModel> Specify<TModel, TFilter>(IQueryable<TModel> query, TFilter filter)
    {
        throw new NotImplementedException();
    }
}