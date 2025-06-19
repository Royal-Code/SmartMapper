using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace RoyalCode.SmartSelector.Generators.Models.Descriptors;

internal sealed class PropertyDescriptor : IEquatable<PropertyDescriptor>
{
    public static PropertyDescriptor Create(PropertyDeclarationSyntax syntax, SemanticModel model)
    {
        var symbol = model.GetDeclaredSymbol(syntax) as IPropertySymbol;

        return new(TypeDescriptor.Create(syntax.Type!, model), syntax.Identifier.Text, symbol);
    }

    public static PropertyDescriptor Create(IPropertySymbol symbol)
        => new(TypeDescriptor.Create(symbol.Type!), symbol.Name, symbol);

    public PropertyDescriptor(TypeDescriptor type, string name, IPropertySymbol? symbol)
    {
        Type = type;
        Name = name;
        Symbol = symbol;
    }

    public TypeDescriptor Type { get; }

    public string Name { get; }

    public IPropertySymbol? Symbol { get; }

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
