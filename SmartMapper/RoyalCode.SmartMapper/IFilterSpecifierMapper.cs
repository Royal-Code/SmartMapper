namespace RoyalCode.SmartMapper;

public interface IFilterSpecifierMapper
{
    IQueryable<TModel> Specify<TModel>(IQueryable<TModel> query, object filter, Type? type = null);
    
    IQueryable<TModel> Specify<TModel, TFilter>(IQueryable<TModel> query, TFilter filter);
}