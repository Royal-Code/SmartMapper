using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RoyalCode.SmartSelector.Generators.Models;
using RoyalCode.SmartSelector.Generators.Models.Descriptors;
using System.Runtime.CompilerServices;
using System.Text;

namespace RoyalCode.SmartSelector.Generators.Extensions;

internal static class ExtensionMethods
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGetAttribute(this MemberDeclarationSyntax node,
        string attributeSimpleName,
        out AttributeSyntax? attributeSyntax)
    {
        return node.AttributeLists.TryGetAttribute(attributeSimpleName, out attributeSyntax);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGetAttribute(this ParameterSyntax node,
        string attributeSimpleName,
        out AttributeSyntax? attributeSyntax)
    {
        return node.AttributeLists.TryGetAttribute(attributeSimpleName, out attributeSyntax);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGetAttribute(this SyntaxList<AttributeListSyntax> attributesLists,
        string attributeSimpleName,
        out AttributeSyntax? attributeSyntax)
    {
        foreach (var attributeList in attributesLists)
        {
            foreach (AttributeSyntax attribute in attributeList.Attributes)
            {
                if (CheckName(attribute, attributeSimpleName))
                {
                    attributeSyntax = attribute;
                    return true;
                }
            }
        }

        attributeSyntax = null;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<AttributeSyntax> GetAttributes(this MemberDeclarationSyntax node,
        string attributeSimpleName)
    {
        var attributesLists = node.AttributeLists;
        foreach (var attributeList in attributesLists)
        {
            foreach (AttributeSyntax attribute in attributeList.Attributes)
            {
                if (CheckName(attribute, attributeSimpleName))
                    yield return attribute;
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool CheckName(AttributeSyntax attribute, string attributeSimpleName)
    {
        var name = attribute.Name;
        if (name is SimpleNameSyntax simpleName)
        {
            return simpleName.Identifier.Text.FastStartWith(attributeSimpleName);
        }
        else if (name is GenericNameSyntax genericName)
        {
            return genericName.Identifier.Text.FastStartWith(attributeSimpleName);
        }
        else if (name is QualifiedNameSyntax qualifiedName)
        {
            return qualifiedName.Right.Identifier.Text.FastEndWith($"{attributeSimpleName}Attribute");
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool FastStartWith(this string text, string value)
    {
        if (text.Length < value.Length)
            return false;
        for (int i = 0; i < value.Length; i++)
        {
            if (text[i] != value[i])
                return false;
        }

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool FastEndWith(this string text, string value)
    {
        if (text.Length < value.Length)
            return false;
        for (int i = 0; i < value.Length; i++)
        {
            if (text[text.Length - i - 1] != value[value.Length - i - 1])
                return false;
        }

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetNamespace(this MemberDeclarationSyntax node)
    {
        var parent = node.Parent;
        while (parent != null)
        {
            if (parent is BaseNamespaceDeclarationSyntax namespaceDeclaration)
                return namespaceDeclaration.Name.ToString();

            parent = parent.Parent;
        }

        return string.Empty;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<string> GetNamespaces(this TypeSyntax typeSyntax, SemanticModel semanticModel)
    {
        // namespace do tipo
        var typeInfo = semanticModel.GetTypeInfo(typeSyntax);
        var namedTypeSymbol = typeInfo.Type as INamedTypeSymbol;

        var ns = namedTypeSymbol?.ContainingNamespace.ToDisplayString();
        if (ns is not null)
            yield return ns;

        // se for generic, namespace dos tipos genéricos
        if (typeSyntax is not GenericNameSyntax genericName)
            yield break;

        foreach (var typeArgument in genericName.TypeArgumentList.Arguments)
        {
            var namespaces = typeArgument.GetNamespaces(semanticModel);
            foreach (var n in namespaces)
                yield return n;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<string> GetNamespaces(this ITypeSymbol typeSymbol)
    {
        var ns = typeSymbol.ContainingNamespace.ToDisplayString();
        yield return ns;

        // se for generic, namespace dos tipos genéricos
        if (typeSymbol is not INamedTypeSymbol namedTypeSymbol)
            yield break;

        foreach (var typeArgument in namedTypeSymbol.TypeArguments)
        {
            if (typeArgument is INamedTypeSymbol namedTypeArgument)
            {
                var namespaces = namedTypeArgument.GetNamespaces();
                foreach (var n in namespaces)
                    yield return n;
            }
        }
    }

    public static string GetName(this INamedTypeSymbol symbol)
    {
        // se for genérico, monta os tipos genéricos entre <>
        if (symbol.IsGenericType)
        {
            StringBuilder sb = new StringBuilder(symbol.Name);
            sb.Append('<');

            foreach(var typeArgument in symbol.TypeArguments)
            {
                if (typeArgument is INamedTypeSymbol namedTypeArgument)
                {
                    sb.Append(namedTypeArgument.GetName() + ",");
                }
            }
            sb.Length--;
            sb.Append('>');
            return sb.ToString();
        }
        else
        {
            return symbol.Name;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEntity(this ParameterSyntax syntax, SemanticModel model)
    {
        // verifica os atributos do parâmetro, se houver um chamado "IsEntity" então é entidade.
        foreach (var attrList in syntax.AttributeLists)
        {
            foreach (var attr in attrList.Attributes)
            {
                if (CheckName(attr, "IsEntity"))
                    return true;
            }
        }

        // verifica os tipos herdados do tipo do parâmetro, se algum ter o nome Entity ou IEntity então é entidade.

        // obtém o símbolo do tipo do parâmetro
        var parameterTypeInfo = model.GetTypeInfo(syntax.Type!);
        // valida se o símbolo é um named typed symbol, o que nos interessa
        if (parameterTypeInfo.Type is INamedTypeSymbol parameterTypeSymbol)
        {
            return parameterTypeSymbol.Extends("Entity") || parameterTypeSymbol.Implements("IEntity");
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCollectionOfEntities(this ParameterSyntax syntax, SemanticModel model)
    {
        // uma coleção de entidade será um IEnumerable<T>, ou T[], ou List<T>, ou outro tipo de coleção
        // onde T é uma entidade.

        // Obtém o símbolo do tipo do parâmetro
        var parameterTypeInfo = model.GetTypeInfo(syntax.Type!);

        // Verifica se o tipo é uma coleção, como IEnumerable<T>
        if (parameterTypeInfo.Type is INamedTypeSymbol parameterTypeSymbol &&
            parameterTypeSymbol.IsGenericType &&
            parameterTypeSymbol.Implements("IEnumerable") &&
            parameterTypeSymbol.TypeArguments.Length == 1 &&
            parameterTypeSymbol.TypeArguments[0] is INamedTypeSymbol namedTypeSymbol &&
            (namedTypeSymbol.Extends("Entity") || namedTypeSymbol.Implements("IEntity")))
        {
            return true;
        }

        // Verifica se o tipo é um array de entidades
        if (parameterTypeInfo.Type is IArrayTypeSymbol arrayTypeSymbol &&
            arrayTypeSymbol.ElementType is INamedTypeSymbol elementTypeSymbol &&
            (elementTypeSymbol.Extends("Entity") || elementTypeSymbol.Implements("IEntity")))
        {
            return true;
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Extends(this INamedTypeSymbol typeSymbol, string typeName)
    {
        // percorre todos os tipos-base verificando se um deles tem o nome Entity
        INamedTypeSymbol? current = typeSymbol;
        while (current is not null)
        {
            if (current.Name == typeName)
                return true;
            current = current.BaseType;
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Implements(this INamedTypeSymbol typeSymbol, string typeName)
    {
        if (typeSymbol.Name == typeName)
            return true;

        // percorre todas interfaces e verifica se alguma tem o nome IEntity
        // Verifica se algum tipo base ou interface implementada tem o nome Entity ou IEntity
        foreach (var interfaceType in typeSymbol.AllInterfaces)
        {
            if (interfaceType.Name == typeName)
                return true;
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PropertyDescriptor? GetIdsProperty(
        this ClassDeclarationSyntax classDeclarationSyntax,
        string parameterName,
        SemanticModel semanticModel)
    {
        var requiredLength = parameterName.Length + 3;
        var last = requiredLength - 1;
        var secondLast = requiredLength - 2;
        var thirdLast = requiredLength - 3;

        var nameToCompare = parameterName.ToLowerInvariant();

        var properties = classDeclarationSyntax.Members.OfType<PropertyDeclarationSyntax>();
        foreach (var prop in properties)
        {
            var name = prop.Identifier.Text.ToLowerInvariant();
            if (name == nameToCompare)
                return PropertyDescriptor.Create(prop, semanticModel);

            if (name.Length != requiredLength)
                continue;

            int start, end;
            if (name[thirdLast] == 'i' && name[secondLast] == 'd' && name[last] == 's')
            {
                start = 0;
                end = thirdLast;
            }
            else if (name[0] == 'i' && name[1] == 'd' && name[2] == 's')
            {
                start = 3;
                end = requiredLength;
            }
            else
                continue;

            int j = 0;
            for (var i = start; i < end; i++)
            {
                if (name[i] != nameToCompare[j])
                    break;
                j++;
            }

            if (j != parameterName.Length)
                continue;

            return PropertyDescriptor.Create(prop, semanticModel);
        }

        return null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PropertyDescriptor? GetIdProperty(
        this ClassDeclarationSyntax classDeclarationSyntax,
        string parameterName,
        SemanticModel semanticModel)
    {
        var requiredLength = parameterName.Length + 2;
        var last = requiredLength - 1;
        var secondLast = parameterName.Length;

        var nameToCompare = parameterName.ToLowerInvariant();

        var properties = classDeclarationSyntax.Members.OfType<PropertyDeclarationSyntax>();
        foreach (var prop in properties)
        {
            var name = prop.Identifier.Text.ToLowerInvariant();
            if (name == nameToCompare)
                return PropertyDescriptor.Create(prop, semanticModel);

            if (name.Length != requiredLength)
                continue;

            int start, end;
            if (name[secondLast] == 'i' && name[last] == 'd')
            {
                start = 0;
                end = secondLast;
            }
            else if (name[0] == 'i' && name[1] == 'd')
            {
                start = 2;
                end = requiredLength;
            }
            else
                continue;

            int j = 0;
            for (var i = start; i < end; i++)
            {
                if (name[i] != nameToCompare[j])
                    break;
                j++;
            }

            if (j != parameterName.Length)
                continue;

            return PropertyDescriptor.Create(prop, semanticModel);
        }

        return null;
    }

    public static TypeSyntax GetInnerType(this TypeSyntax typeSyntax)
    {
        if (typeSyntax is GenericNameSyntax genericNameSyntax)
            return genericNameSyntax.TypeArgumentList.Arguments[0];

        return typeSyntax;
    }

    public static TypeSyntax GetValueReturnType(this TypeSyntax typeSyntax)
    {
        var type = typeSyntax;

        // se for um Task<T> então obtém o T
        if (type is GenericNameSyntax { Identifier.Text: "Task" } taskSyntax)
            type = taskSyntax.TypeArgumentList.Arguments[0];

        // se for um ValueTask<T> então obtém o T
        if (type is GenericNameSyntax { Identifier.Text: "ValueTask" } valueTaskSyntax)
            type = valueTaskSyntax.TypeArgumentList.Arguments[0];

        // se for um Result<T> então obtém o T
        if (type is GenericNameSyntax { Identifier.Text: "Result" } resultSyntax)
            type = resultSyntax.TypeArgumentList.Arguments[0];

        return type;
    }

    public static IEnumerable<ISymbol> GetAllMembers(this ITypeSymbol? typeSymbol)
    {
        while (typeSymbol is not null)
        {
            foreach (var member in typeSymbol.GetMembers())
                yield return member;

            typeSymbol = typeSymbol.BaseType;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string TryGetInnerTaskType(this string typeDeclaration)
    {
        if (typeDeclaration is "void" || typeDeclaration is "Task")
            return "Result";

        return typeDeclaration.StartsWith("Task<")
            ? typeDeclaration.Substring(5, typeDeclaration.Length - 6)
            : typeDeclaration;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Each<T>(this IEnumerable<T> values, Action<T> action)
    {
        foreach (var v in values)
            action(v);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToLowerCamelCase(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        return char.ToLowerInvariant(value[0]) + value.Substring(1);
    }

    public static string ToPascalCase(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        // exemplo, "my-group-name" -> "MyGroupName"

        // separa as palavras pelo hífen e por '/'
        var words = value.Split('-', '/');
        // converte a primeira letra de cada palavra para maiúscula
        for (var i = 0; i < words.Length; i++)
        {
            words[i] = char.ToUpperInvariant(words[i][0]) + words[i].Substring(1);
        }

        // junta as palavras
        return string.Join("", words);
    }

    // método para remover aspas duplas (") de ums string que representa um literal
    public static string RemoveQuotes(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        if (value.Length < 2)
            return value;

        if (value[0] == '"' && value[value.Length - 1] == '"')
            return value.Substring(1, value.Length - 2);

        return value;
    }

    /// <summary>
    /// Splits pascal case, so "FooBar" would become [ "Foo", "Bar" ].
    /// </summary>
    /// <param name="name">A string, thats represents a name of something, to be splited.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string[]? SplitUpperCase(this string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        var parts = new LinkedList<string>();
        var sb = new StringBuilder();

        for (int i = 0; i < name.Length; ++i)
        {
            var currentChar = name[i];
            if (char.IsUpper(currentChar) && i > 1
                && (!char.IsUpper(name[i - 1]) || i + 1 < name.Length && !char.IsUpper(name[i + 1])))
            {
                parts.AddLast(sb.ToString());
                sb.Clear();
            }
            sb.Append(currentChar);
        }

        parts.AddLast(sb.ToString());

        return parts.Count > 1 ? parts.ToArray() : null;
    }

    /// <summary>
    /// Attempts to get the generic argument type of an <see cref="IEnumerable{T}"/>-like type.
    /// </summary>
    /// <param name="typeSymbol">The symbol to inspect.</param>
    /// <param name="underlyingType">The generic type argument <c>T</c>.</param>
    /// <returns>
    ///     Returns <c>true</c> if the symbol represents or implements <see cref="IEnumerable{T}"/>,
    ///     otherwise <c>false</c>.
    /// </returns>
    public static bool TryGetEnumerableGenericType(this ITypeSymbol typeSymbol, out ITypeSymbol? underlyingType)
    {
        if (TryGetEnumerableGenericTypeCore(typeSymbol, out underlyingType))
            return true;

        foreach (var @interface in typeSymbol.AllInterfaces)
        {
            if (TryGetEnumerableGenericTypeCore(@interface, out underlyingType))
                return true;
        }

        underlyingType = null;
        return false;
    }

    private static bool TryGetEnumerableGenericTypeCore(ITypeSymbol symbol, out ITypeSymbol? underlyingType)
    {
        if (symbol is INamedTypeSymbol namedType &&
            namedType.IsGenericType &&
            namedType.ConstructedFrom.SpecialType == SpecialType.System_Collections_Generic_IEnumerable_T)
        {
            underlyingType = namedType.TypeArguments[0];
            return true;
        }

        underlyingType = null;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static StringBuilder AppendPropertyPath(this StringBuilder sb, PropertySelection property)
    {
        property.WritePropertyPath(sb);
        return sb;
    }
}