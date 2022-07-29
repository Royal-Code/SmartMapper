using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Configurations;

[Obsolete]
public class Resolution<TFrom, TTo>
{
    private Expression<Func<TFrom, TTo>>? adapterExpression;
        
    public Expression<Func<TFrom, TTo>>? TryGetAdapterExpression()
    {
        return adapterExpression;
    }

    public void AddAdapterExpression(Expression<Func<TFrom, TTo>> expr)
    {
        adapterExpression = expr;
    }
}