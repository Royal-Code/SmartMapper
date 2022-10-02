using System.Reflection;

namespace RoyalCode.SmartMapper.Extensions;

public static class ReflectionExtensions
{
    public static bool IsATargetMethod(this MethodInfo method, Type targetType)
    {
        return method.DeclaringType == targetType && method.IsPublic && !method.IsStatic;
    }

    public static string GetPathName(this PropertyInfo property)
    {
        return $"{property.DeclaringType?.Name}.{property.Name}";
    }
}