
using System.Reflection;

namespace RoyalCode.SmartMapper.Core.Extensions;

/// <summary>
/// Static class with extension methods for reflection.
/// </summary>
public static class ReflectionExtensions
{
    /// <summary>
    /// Get the source properties of a type that should be mapped.
    /// </summary>
    /// <param name="sourceType">The source type.</param>
    /// <returns>A collection of properties of the source type.</returns>
    public static ICollection<PropertyInfo> GetSourceProperties(this Type sourceType)
    {
        return sourceType.GetTypeInfo()
            .GetRuntimeProperties()
            .Where(p => p.CanRead)
            .ToList();
    }

    /// <summary>
    /// Checks whether the <paramref name="type"/> implements the <paramref name="genericType"/> 
    /// in a generic or closed way.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <param name="genericType">The generic type.</param>
    /// <returns>
    ///     True if implements, false otherwise.
    /// </returns>
    public static bool ImplementsGenericType(this Type type, Type genericType)
    {
        if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
            return true;

        return Array.Exists(type.GetInterfaces(), i => i.IsGenericType && i.GetGenericTypeDefinition() == genericType);
    }
}
