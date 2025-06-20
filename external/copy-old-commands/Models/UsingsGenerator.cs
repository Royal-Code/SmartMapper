using System.Text;
using Microsoft.CodeAnalysis;
using RoyalCode.SmartCommands.Generators.Models.Descriptors;

namespace RoyalCode.SmartCommands.Generators.Models;

public class UsingsGenerator : GeneratorNode
{
    // By Design: considera-se sempre usar implicit usings.
    private readonly List<string> toExcludeNamespaces = [
        "System",
        "System.Collections.Generic",
        "System.IO",
        "System.Linq",
        "System.Net.Http",
        "System.Threading",
        "System.Threading.Tasks"
    ];
    
    private readonly List<string> usings = [];
   
    public void ExcludeNamespace(string @namespace)
    {
        toExcludeNamespaces.Add(@namespace);
    }
    
    public void AddNamespace(string @namespace)
    {
        usings.Add(@namespace);
    }

    public void AddNamespace(INamedTypeSymbol symbol)
    {
        AddNamespace(symbol.ContainingNamespace.ToDisplayString());
    }

    public void AddNamespaces(TypeDescriptor descriptor)
        => AddNamespaces(descriptor.Namespaces);

    public void AddNamespaces(ParameterDescriptor descriptor)
        => AddNamespaces(descriptor.Type.Namespaces);

    public void AddNamespaces(IEnumerable<string> namespaces)
    {
        foreach(var ns in namespaces)
            usings.Add(ns);
    }

    public void AddNamespaces(MethodGenerator method) => method.AddUsings(this);

    public override void Write(StringBuilder sb, int ident = 0)
    {
        var toWrite = usings
            .Where(ns => ns is not null && ns.Length > 0)
            .Where(ns => ns[0] != '<')
            .Where(ns => !toExcludeNamespaces.Contains(ns))
            .Distinct()
            .OrderBy(ns => ns)
            .ToList();

        foreach (var ns in toWrite)
        {
            sb.AppendLine($"using {ns};");
        }
    }
}