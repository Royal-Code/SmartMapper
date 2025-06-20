using Microsoft.CodeAnalysis;

namespace RoyalCode.SmartCommands.Generators.Models;

#pragma warning disable S4035 // IEquatable
#pragma warning disable S2328 // GetHashCode

[Obsolete("REMOVER", true)]
public readonly struct TransformResult<TGenerator> : IEquatable<TransformResult<TGenerator>>
{

    public static implicit operator TransformResult<TGenerator>(TGenerator generator) => new(generator);

    public static implicit operator TransformResult<TGenerator>(Diagnostic diagnostic) => new(diagnostic);

    public static implicit operator TransformResult<TGenerator>(List<Diagnostic> diagnostic) => new(diagnostic);

    public TransformResult(TGenerator generator) : this()
    {
        Generator = generator;
    }

    public TransformResult(Diagnostic diagnostics) : this()
    {
        Diagnostics = [diagnostics];
    }

    public TransformResult(List<Diagnostic> diagnostics) : this()
    {
        Diagnostics = diagnostics;
    }

    public TransformResult(TGenerator generator, List<Diagnostic> diagnostics) : this()
    {
        Generator = generator;
        Diagnostics = diagnostics;
    }

    public readonly TGenerator? Generator { get; }

    public readonly List<Diagnostic>? Diagnostics { get; }

    public bool Equals(TransformResult<TGenerator> other)
    {
        return Equals(Generator, other.Generator)
            && (Diagnostics?.SequenceEqual(other.Diagnostics) ?? other.Diagnostics is null);
    }

    public override bool Equals(object? obj)
    {
        return obj is TransformResult<TGenerator> other && Equals(other);
    }

    public override int GetHashCode()
    {
        var hash = Generator?.GetHashCode() ?? 0;
        hash = hash * 31 + (Diagnostics?.GetHashCode() ?? 0);
        return hash;
    }
}
