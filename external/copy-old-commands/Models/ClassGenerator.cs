using System.Text;
using Microsoft.CodeAnalysis;
using RoyalCode.SmartCommands.Generators.Generators;

namespace RoyalCode.SmartCommands.Generators.Models;

public class ClassGenerator : ITransformationGenerator
{
    private UsingsGenerator? usings;
    private GeneratorNodeList? attributes;
    private ModifiersGenerator? modifiers;
    private GenericsGenerator? generics;
    private HierarchyGenerator? hierarchy;
    private GeneratorNodeList? where;
    private GeneratorNodeList? fields;
    private GeneratorNodeList? constructors;
    private GeneratorNodeList? properties;
    private GeneratorNodeList? methods;

    public ClassGenerator(string name, string ns, string typeType = "class")
    {
        Name = name;
        Namespace = ns;
        TypeType = typeType;
    }

    public UsingsGenerator Usings => usings ??= new();

    public string Namespace { get; set; }

    public GeneratorNodeList Attributes => attributes ??= new();

    public ModifiersGenerator Modifiers => modifiers ??= new();

    public string TypeType { get; }

    public string Name { get; set; }

    public GenericsGenerator Generics => generics ??= new();

    public HierarchyGenerator Hierarchy => hierarchy ??= new();

    public GeneratorNodeList Where => where ??= new();

    public GeneratorNodeList Fields => fields ??= new();

    public GeneratorNodeList Constructors => constructors ??= new();

    public GeneratorNodeList Properties => properties ??= new();

    public GeneratorNodeList Methods => methods ??= new();

    /// <summary>
    /// Nome do arquivo que será gerado, opcional, será utilizado o padrão: <c>"{Name}.g.cs"</c>.
    /// </summary>
    public string? FileName { get; set; }

    public event Action<SourceProductionContext, StringBuilder>? Generating;
    
    public event Action<SourceProductionContext, StringBuilder>? Generated;

    public MethodGenerator CreateImplementation(MethodGenerator abstractMethod)
    {
        var impl = MethodGenerator.CreateImplementation(abstractMethod);
        impl.AddUsings(Usings);
        Methods.Add(impl);
        return impl;
    }

    public void Generate(SourceProductionContext spc)
    {
        var builder = new StringBuilder();

        Generating?.Invoke(spc, builder);

        Write(builder);

        Generated?.Invoke(spc, builder);

        var source = builder.ToString();

        spc.AddSource(FileName ?? $"{Name}.g.cs", source);
    }

    public bool HasErrors(SourceProductionContext spc, SyntaxToken mainToken) => false;

    protected void Write(StringBuilder sb)
    {
        usings?.ExcludeNamespace(Namespace);
        usings?.Write(sb);

        sb.AppendLine();
        sb.Append("namespace ").Append(Namespace).AppendLine(";");
        sb.AppendLine();

        attributes?.Write(sb);
        modifiers?.Write(sb);
        sb.Append(TypeType).Append(" ").Append(Name);

        generics?.Write(sb);
        hierarchy?.Write(sb);
        where?.Write(sb, 1);

        sb.AppendLine();
        sb.Append("{");

        if (fields is not null)
        {
            sb.AppendLine();
            fields.Write(sb, 1);
        }

        constructors?.Write(sb, 1);
        properties?.Write(sb, 1);
        methods?.Write(sb, 1);

        sb.AppendLine("}");
    }
}