using System.Text;

namespace RoyalCode.SmartCommands.Generators.Models;

#pragma warning disable S2376 // Set method

public class HierarchyGenerator : GeneratorNode
{
    private string? extends;
    private List<string>? implements;

    public string Extends { set => extends = value; }

    public void AddImplements(string typeName)
    {
        implements ??= [];

        if (implements.Contains(typeName))
            return;

        implements.Add(typeName);
    }

    public override void Write(StringBuilder sb, int ident = 0)
    {
        if (extends is null && implements is null)
            return;

        sb.Append(" : ");

        if (extends is not null)
            sb.Append(extends);

        if (implements is null)
            return;

        if (extends is not null)
            sb.Append(", ");

        sb.Append(string.Join(", ", implements));
    }
}