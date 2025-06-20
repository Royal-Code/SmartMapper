using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoyalCode.SmartCommands.Generators.Models.Descriptors;

#pragma warning disable S3358 // Ternary operators should not be nested

public sealed class TypeDescriptor : IEquatable<TypeDescriptor>
{
    #region Factories

    public static TypeDescriptor Create(TypeSyntax typeSyntax, SemanticModel model)
    {
        var name = typeSyntax.ToString();
        bool isNullable = false;

        if (name[name.Length - 1] == '?')
        {
            var typeInfo = model.GetTypeInfo(typeSyntax);
            var namedTypeSymbol = typeInfo.Type as INamedTypeSymbol;
            isNullable = namedTypeSymbol?.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T;
        }

        return new(name, typeSyntax.GetNamespaces(model).ToArray(), isNullable);
    }

    public static TypeDescriptor Create(ITypeSymbol typeSymbol, SemanticModel model)
    {
        var name = typeSymbol.ToString();
        bool isNullable = false;

        if (name[name.Length - 1] == '?')
        {
            var namedTypeSymbol = typeSymbol as INamedTypeSymbol;
            isNullable = namedTypeSymbol?.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T;
        }

        return new(name, typeSymbol.GetNamespaces().ToArray(), isNullable);
    }

    public static readonly TypeDescriptor CancellationToken = new("CancellationToken", []);

    public static readonly TypeDescriptor Void = new("void", []);

    #endregion

    private List<string>? hints;

    public TypeDescriptor(string name, string[] namespaces, bool isNullable = false)
    {
        Name = name;
        Namespaces = namespaces;
        IsNullable = isNullable;
    }

    public string Name { get; }

    public string[] Namespaces { get; }

    public bool IsNullable { get; }

    public bool IsVoid => Name == "void";

    public bool IsVoidTask => Name == "Task";

    public bool IsCancellationToken => Name == "CancellationToken";

    public bool Is(ParameterDescriptor parameter) => Equals(parameter.Type);

    public void AddHint(string hint)
    {
        hints ??= [];
        hints.Add(hint);
    }

    public bool HasHint(string hint) => hints is not null && hints.Contains(hint);

    public void MarkAsEntity() => AddHint("IsEntity");

    public bool IsEntity => HasHint("IsEntity");

    public void MarkAsCollectionOfEntities() => AddHint("IsCollectionOfEntities");

    public bool IsCollectionOfEntities => HasHint("IsCollectionOfEntities");

    public void MarkAsContext() => AddHint("IsContext");

    public bool IsContext => HasHint("IsContext");

    public void MarkAsHandlerParameter() => AddHint("IsHandlerParameter");

    public bool IsHandlerParameter => HasHint("IsHandlerParameter");

    public bool MayBeNull => IsNullable || Name[Name.Length - 1] == '?';

    public bool IsArray => Name.EndsWith("[]") || Name.EndsWith("[]?");

    public string UnderlyingType => IsNullable || Name[Name.Length - 1] == '?' ? Name.Substring(0, Name.Length - 1) : Name;

    public string ArrayType => Name.EndsWith("[]")
        ? Name.Substring(0, Name.Length - 2)
        : Name.EndsWith("[]?")
            ? Name.Substring(0, Name.Length - 3)
            : UnderlyingType;

    public string GenericType
    {
        get
        {
            var index = Name.IndexOf('<');
            if (index == -1)
                return Name;

            // foo<bar> => index of < is 3, length is 8, so start is 3 + 1 = 4, length is 8 - 3 - 2 = 3 => bar
            return Name.Substring(index + 1, Name.Length - index - 2);
        }
    }

    public bool Equals(TypeDescriptor? other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return Name == other.Name && 
               Namespaces.SequenceEqual(other.Namespaces);
    }

    public override bool Equals(object? obj)
    {
        return obj is TypeDescriptor other && Equals(other);
    }

    public override int GetHashCode()
    {
        int hashCode = -353132481;
        hashCode = hashCode * -1521134295 + Name.GetHashCode();
        hashCode = hashCode * -1521134295 + Namespaces.GetHashCode();
        return hashCode;
    }

    public bool HasValueType(out TypeDescriptor? type)
    {
        string typeName = Name;

        if (IsVoid)
        {
            type = null;
            return false;
        }

        if (typeName.StartsWith("Task"))
        {
            if (typeName.Length == 4)
            {
                type = null;
                return false;
            }

            typeName = typeName.Substring(5, typeName.Length - 6);
        }

        if (typeName.StartsWith("Result"))
        {
            if (typeName.Length == 6)
            {
                type = null;
                return false;
            }

            typeName = typeName.Substring(7, typeName.Length - 8);
        }

        type = new TypeDescriptor(typeName, Namespaces);
        return true;
    }

    public TypeDescriptor Wrap(string typeName, string ns)
    {
        return new TypeDescriptor($"{typeName}<{Name}>", [..Namespaces, ns]);
    }

    public TypeDescriptor MustBeTask()
    {
        if (Name.StartsWith("Task"))
            return this;

        if (IsVoid)
            return new TypeDescriptor("Task", Namespaces);

        return new TypeDescriptor($"Task<{Name}>", Namespaces);
    }

    public TypeDescriptor MustBeResult()
    {
        string typeName = Name;
        bool task = false;

        if (IsVoid)
            return new TypeDescriptor("Result", Namespaces);

        if (typeName == "Task")
        {
            return new TypeDescriptor("Task<Result>", [.. Namespaces, "RoyalCode.SmartProblems"]);
        }

        if (typeName.StartsWith("Task"))
        {
            typeName = typeName.Substring(5, typeName.Length - 6);
            task = true;
        }

        if (typeName.StartsWith("Result"))
            return this;

        typeName = $"Result<{typeName}>";

        if (task)
            typeName = $"Task<{typeName}>";

        return new TypeDescriptor(typeName, [.. Namespaces, "RoyalCode.SmartProblems"]);
    }
}
