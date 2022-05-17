using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace RoyalCode.SmartMapper.Extensions;

internal static class ExpressionExtensions
{
    public static bool TryGetMember(this Expression expression, [NotNullWhen(true)] out MemberInfo? member)
    {
        if (expression.NodeType is ExpressionType.Lambda)
            return TryGetMember((LambdaExpression) expression, out member);

        if (expression.NodeType is not ExpressionType.MemberAccess)
        {
            member = null;
            return false;
        }
        
        member = ((MemberExpression) expression).Member;
        return true;
    }

    public static bool TryGetMember(this LambdaExpression expression, [NotNullWhen(true)] out MemberInfo? member)
    {
        return TryGetMember(expression.Body, out member);
    }
}