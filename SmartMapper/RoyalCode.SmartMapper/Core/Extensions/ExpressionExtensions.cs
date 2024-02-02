using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace RoyalCode.SmartMapper.Core.Extensions;

/// <summary>
/// Extensions methods for <see cref="Expression"/>.
/// </summary>
public static class ExpressionExtensions
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
    
    public static bool TryGetMethod(this Expression expression, [NotNullWhen(true)] out MethodInfo? method)
    {
        if (expression.NodeType is ExpressionType.Lambda)
            return TryGetMethod(((LambdaExpression)expression).Body, out method);
        if (expression.NodeType is ExpressionType.Convert)
            return TryGetMethod(((UnaryExpression)expression).Operand, out method);

        if (expression.NodeType is not ExpressionType.Call)
        {
            method = null;
            return false;
        }

        var methodExpression = ((MethodCallExpression) expression).Object;
        if (methodExpression?.NodeType is ExpressionType.Constant)
        {
            method = ((ConstantExpression)methodExpression).Value as MethodInfo;
            return method is not null;
        }

        method = null;
        return false;
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