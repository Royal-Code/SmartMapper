using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoyalCode.SmartCommands.Generators.Models.Descriptors;

public sealed class PropertyDescriptor : IEquatable<PropertyDescriptor>
{
    public static PropertyDescriptor Create(PropertyDeclarationSyntax syntax, SemanticModel model)
        => new(TypeDescriptor.Create(syntax.Type!, model), syntax.Identifier.Text);

    public static PropertyDescriptor Create(IPropertySymbol syntax, SemanticModel model)
        => new(TypeDescriptor.Create(syntax.Type!, model), syntax.Name);

    public PropertyDescriptor(TypeDescriptor type, string name)
    {
        Type = type;
        Name = name;
    }

    public TypeDescriptor Type { get; }

    public string Name { get; }

    public bool Equals(PropertyDescriptor? other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return Name == other.Name &&
            Equals(Type, other.Type);
    }

    public override bool Equals(object? obj)
    {
        return obj is ParameterDescriptor other && Equals(other);

    }

    public override int GetHashCode()
    {
        int hashCode = -1979447941;
        hashCode = hashCode * -1521134295 + Type.GetHashCode();
        hashCode = hashCode * -1521134295 + Name.GetHashCode();
        return hashCode;
    }
}
