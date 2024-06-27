
using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartMapper.Core.Extensions;

/// <summary>
/// Utils extensions.
/// </summary>
public static class UtilsExtensions
{
    /// <summary>
    /// Gets the first item in the collection, and returns whether the collection has a single item.
    /// </summary>
    /// <typeparam name="T">The item type.</typeparam>
    /// <param name="values">The collection.</param>
    /// <param name="value">The first item, if exists.</param>
    /// <returns>True if the collection has only one item, false otherwise.</returns>
    public static bool HasSingle<T>(this IEnumerable<T> values, [NotNullWhen(true)] out T? value)
        where T : class
    {
        value = null;
        foreach (var item in values)
        {
            if (value is not null)
                return false;

            value = item;
        }

        return value is not null;
    }
}
