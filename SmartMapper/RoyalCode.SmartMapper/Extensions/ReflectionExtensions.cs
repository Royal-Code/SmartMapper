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

    public static string GetTypeName(this Type type)
    {
        throw new NotImplementedException();
    }

    public static PropertyInfo[] GetReadableProperties(this Type type)
    {
        return type.GetTypeInfo()
            .GetRuntimeProperties()
            .Where(t => t.CanRead)
            .ToArray();
    }
}