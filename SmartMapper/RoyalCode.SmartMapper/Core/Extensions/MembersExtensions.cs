using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace RoyalCode.SmartMapper.Core.Extensions;

/// <summary>
/// Extensions for members and for member discovery.
/// </summary>
public static class MembersExtensions
{
    /// <summary>
    /// Splits pascal case, so "FooBar" would become [ "Foo", "Bar" ].
    /// </summary>
    /// <param name="name">A string, thats represents a name of something, to be splited.</param>
    /// <param name="parts">The parts of the name splited by upper case</param>
    public static bool SplitUpperCase(this string? name, [NotNullWhen(true)] out string[]? parts)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            parts = null;
            return false;
        }

        var partsList = new LinkedList<string>();
        var sb = new StringBuilder();

        for (int i = 0; i < name.Length; ++i)
        {
            var currentChar = name[i];
            if (char.IsUpper(currentChar) && i > 1
                && (!char.IsUpper(name[i - 1]) || (i + 1 < name.Length && !char.IsUpper(name[i + 1]))))
            {
                partsList.AddLast(sb.ToString());
                sb.Clear();
            }
            sb.Append(currentChar);
        }

        partsList.AddLast(sb.ToString());

        if (partsList.Count <= 1)
        {
            parts = null;
            return false;
        }

        parts = [.. partsList];
        return true;
    }

    /// <summary>
    /// Concatenates names from the start index to the end index.
    /// </summary>
    /// <param name="names">The names.</param>
    /// <param name="start">The start index.</param>
    /// <param name="end">The end index.</param>
    /// <returns>The concatenated name.</returns>
    public static string Concat(this string[] names, int start, int end = -1)
    {
        StringBuilder sb = new StringBuilder();
        if (end == -1)
            end = names.Length - 1;

        for (int i = start; i <= end; ++i)
        {
            sb.Append(names[i]);
        }

        return sb.ToString();
    }
}
