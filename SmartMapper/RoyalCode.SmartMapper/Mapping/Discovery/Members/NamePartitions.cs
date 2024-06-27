using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Core.Extensions;

namespace RoyalCode.SmartMapper.Mapping.Discovery.Members;

/// <summary>
/// <para>
///     Parts of a given name for mapping an orgim property to some property or method of the target type.
/// </para>
/// <para>
///     The name of the source property is split into parts using CamelCase/PascalCase to do so.
/// </para>
/// </summary>
public sealed class NamePartitions
{
    /// <summary>
    /// Creates a new name partitions.
    /// </summary>
    /// <param name="name">The source property name.</param>
    /// <exception cref="ArgumentException">If null or empty.</exception>
    public NamePartitions(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));

        Name = name;

        HasParts = name.SplitUpperCase(out var parts);
        Parts = HasParts ? parts! : [name];
    }

    /// <summary>
    /// The source property name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// If the name has parts, separating the names by upper case.
    /// </summary>
    public bool HasParts { get; }

    /// <summary>
    /// Parts of the name, separated by upper case.
    /// </summary>
    public string[] Parts { get; }

    /// <summary>
    /// Obtains a name by concatenating the parts from an initial index.
    /// </summary>
    /// <param name="index">Initial index.</param>
    /// <param name="name">The result name.</param>
    /// <returns>
    ///     True if it was possible to obtain a name, false if the index exceeds the number of names.
    /// </returns>
    public bool GetName(int index, [NotNullWhen(true)] out string? name)
    {
        if (index == 0)
        {
            name = Name;
            return true;
        }

        if (index >= Parts.Length)
        {
            name = null;
            return false;
        }

        name = Parts.Concat(index);
        return true;
    }

    /// <summary>
    /// Obtains a name by concatenating the parts from an initial index and a reverse end index.
    /// </summary>
    /// <param name="index">Initial index.</param>
    /// <param name="end">Reverse end index.</param>
    /// <param name="name">The result name.</param>
    /// <returns>True if it was possible to obtain a name, false if the index exceeds the number of names.</returns>
    public bool GetName(int index, int end, [NotNullWhen(true)] out string? name)
    {
        if (index == 0 && end == 0)
        {
            name = Name;
            return true;
        }

        if (index + end >= Parts.Length)
        {
            name = null;
            return false;
        }

        name = Parts[index..^end].Concat(0);
        return true;
    }
}

