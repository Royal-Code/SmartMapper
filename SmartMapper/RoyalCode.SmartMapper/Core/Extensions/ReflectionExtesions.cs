
using System.Reflection;

namespace RoyalCode.SmartMapper.Core.Extensions;

/// <summary>
/// Static class with extension methods for reflection.
/// </summary>
public static class ReflectionExtesions
{
    /// <summary>
    /// Get the source properties of a type that should be mapped.
    /// </summary>
    /// <param name="sourceType">The source type.</param>
    /// <returns>A collection of properties of the source type.</returns>
    public static ICollection<PropertyInfo> GetSourceProperties(this Type sourceType)
    {
        return sourceType.GetProperties()
            .Where(p => p.CanRead)
            .ToList();
    }
}
