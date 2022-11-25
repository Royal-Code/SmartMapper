using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace RoyalCode.SmartMapper.Extensions;

public static class LinqExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (T item in source)
        {
            action(item);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> AsSpanMarshal<T>(this List<T> source)
        where T : class
    {
        return CollectionsMarshal.AsSpan(source);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> ToSpanMarshal<T>(this IEnumerable<T> source)
        where T : class
    {
        return CollectionsMarshal.AsSpan(source.ToList());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ForEach<T>(this Span<T> source, Action<T> action)
    {
        for (int i = 0; i < source.Length; i++)
        {
            action(source[i]);
        }
    }
}