using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace RoyalCode.SmartMapper.Extensions;

internal static class ExpressionExtensions
{
    public static bool TryGetMember(this Expression expression, [NotNullWhen(true)] out MemberInfo? member)
    {
        if (expression.NodeType is ExpressionType.Lambda)
            return TryGetMember((LambdaExpression)expression, out member);

        if (expression.NodeType is not ExpressionType.MemberAccess)
        {
            member = null;
            return false;
        }

        member = ((MemberExpression)expression).Member;
        return true;
    }

    public static bool TryGetMember(this LambdaExpression expression, [NotNullWhen(true)] out MemberInfo? member)
    {
        return TryGetMember(expression.Body, out member);
    }

    public static PropertyInfo GetSelectedProperty(this LambdaExpression expression)
    {
        if (!expression.TryGetMember(out var memberInfo))
            throw new ArgumentException("The property selection is not a valid expression!");

        if (memberInfo is PropertyInfo propertyInfo)
            return propertyInfo;

        throw new ArgumentException("The selected member is not a property!");
    }
}