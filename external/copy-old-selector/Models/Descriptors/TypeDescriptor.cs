using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RoyalCode.SmartSelector.Generators.Extensions;

namespace RoyalCode.SmartSelector.Generators.Models.Descriptors;

#pragma warning disable S3358 // Ternary operators should not be nested

internal sealed class TypeDescriptor : IEquatable<TypeDescriptor>
{
    #region Factories

    public static TypeDescriptor Create(TypeSyntax typeSyntax, SemanticModel model)
    {
        var name = typeSyntax.ToString();
        bool isNullable = false;

        var typeInfo = model.GetTypeInfo(typeSyntax);
        if (typeInfo.Type is null)
            return new(name, typeSyntax.GetNamespaces(model).ToArray(), null!, isNullable);

        var namedTypeSymbol = typeInfo.Type as INamedTypeSymbol;

        if (name[name.Length - 1] == '?')
        {
            isNullable = namedTypeSymbol?.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T;
        }

        return new(name, typeSyntax.GetNamespaces(model).ToArray(), namedTypeSymbol, isNullable);
    }

    public static TypeDescriptor Create(ITypeSymbol typeSymbol)
    {
        var name = typeSymbol.ToString();
        bool isNullable = false;

        var namedTypeSymbol = typeSymbol as INamedTypeSymbol;

        if (name[name.Length - 1] == '?')
        {
            isNullable = namedTypeSymbol?.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T;
        }

        var namespaces = typeSymbol.GetNamespaces().ToArray();

        if (namedTypeSymbol is not null)
        {
            if (isNullable && namedTypeSymbol.TypeArguments[0] is INamedTypeSymbol underlyingSymbol)
            {
                name = underlyingSymbol.GetName() + "?";
            }
            else
            {
                name = namedTypeSymbol.GetName();
            }
        }

        return new(name, namespaces, namedTypeSymbol, isNullable);
    }

    private static TypeDescriptor? cancellationToken;
    public static TypeDescriptor CancellationToken(SemanticModel model)
    {
        if (cancellationToken is not null)
            return cancellationToken;

        var name = "CancellationToken";
        var namespaces = new[] { "System.Threading" };
        var symbol = model.Compilation.GetTypeByMetadataName("System.Threading.CancellationToken");
        
        if (symbol is null)
            throw new InvalidOperationException($"Type '{name}' not found in the compilation.");

        cancellationToken = new(name, namespaces, symbol, false);
        return cancellationToken;
    }

    private static TypeDescriptor? voidTypeDescriptor;
    public static TypeDescriptor Void(SemanticModel model)
    {
        if (voidTypeDescriptor is not null)
            return voidTypeDescriptor;

        var name = "void";
        var namespaces = new[] { "System" };
        var symbol = model.Compilation.GetSpecialType(SpecialType.System_Void);

        if (symbol is null)
            throw new InvalidOperationException($"Type '{name}' not found in the compilation.");
        
        voidTypeDescriptor = new(name, namespaces, symbol, false);
        return voidTypeDescriptor;
    }

    #endregion

    private List<string>? hints;

    public TypeDescriptor(string name, string[] namespaces, INamedTypeSymbol? symbol, bool isNullable = false)
    {
        Name = name;
        Namespaces = namespaces;
        IsNullable = isNullable;
        Symbol = symbol;
    }

    public string Name { get; }

    public string[] Namespaces { get; }

    public INamedTypeSymbol? Symbol { get; }

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

    public bool IsEnumerable => Name.StartsWith("IEnumerable<") || Name.Equals("IEnumerable");

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

    public IReadOnlyList<PropertyDescriptor> CreateProperties(Func<IPropertySymbol, bool>? predicate)
    {
        if (Symbol is null)
            return [];

        var typeSymbols = new List<INamedTypeSymbol>();
        var typeSymbol = Symbol;

        // Collect all base types up to object, including the current type
        while (typeSymbol is INamedTypeSymbol namedType)
        {
            typeSymbols.Insert(0, namedType); // Insert at the beginning to reverse the order
            typeSymbol = namedType.BaseType;
        }

        var properties = new List<PropertyDescriptor>();

        // For each type, from base to derived, add its properties in declaration order
        foreach (var namedType in typeSymbols)
        {
            var props = namedType
                .GetMembers()
                .OfType<IPropertySymbol>()
                .Where(p => p.DeclaredAccessibility == Accessibility.Public && !p.IsStatic)
                .Where(p => predicate is null || predicate(p))
                .Select(PropertyDescriptor.Create);

            properties.AddRange(props);
        }

        // Remove duplicates by property name (derived class property hides base class property)
        var distinctProperties = properties
            .GroupBy(p => p.Name)
            .Select(g => g.Last()) // Now, the most derived property is last, so take Last()
            .ToList();

        return distinctProperties;
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

    //public bool HasValueType(out TypeDescriptor? type)
    //{
    //    string typeName = Name;

    //    if (IsVoid)
    //    {
    //        type = null;
    //        return false;
    //    }

    //    if (typeName.StartsWith("Task"))
    //    {
    //        if (typeName.Length == 4)
    //        {
    //            type = null;
    //            return false;
    //        }

    //        typeName = typeName.Substring(5, typeName.Length - 6);
    //    }

    //    if (typeName.StartsWith("Result"))
    //    {
    //        if (typeName.Length == 6)
    //        {
    //            type = null;
    //            return false;
    //        }

    //        typeName = typeName.Substring(7, typeName.Length - 8);
    //    }

    //    type = new TypeDescriptor(typeName, Namespaces);
    //    return true;
    //}

    //public TypeDescriptor Wrap(string typeName, string ns)
    //{
    //    return new TypeDescriptor($"{typeName}<{Name}>", [..Namespaces, ns]);
    //}

    //public TypeDescriptor MustBeTask()
    //{
    //    if (Name.StartsWith("Task"))
    //        return this;

    //    if (IsVoid)
    //        return new TypeDescriptor("Task", Namespaces);

    //    return new TypeDescriptor($"Task<{Name}>", Namespaces);
    //}

    //public TypeDescriptor MustBeResult()
    //{
    //    string typeName = Name;
    //    bool task = false;

    //    if (IsVoid)
    //        return new TypeDescriptor("Result", Namespaces);

    //    if (typeName == "Task")
    //    {
    //        return new TypeDescriptor("Task<Result>", [.. Namespaces, "RoyalCode.SmartProblems"]);
    //    }

    //    if (typeName.StartsWith("Task"))
    //    {
    //        typeName = typeName.Substring(5, typeName.Length - 6);
    //        task = true;
    //    }

    //    if (typeName.StartsWith("Result"))
    //        return this;

    //    typeName = $"Result<{typeName}>";

    //    if (task)
    //        typeName = $"Task<{typeName}>";

    //    return new TypeDescriptor(typeName, [.. Namespaces, "RoyalCode.SmartProblems"]);
    //}
}
