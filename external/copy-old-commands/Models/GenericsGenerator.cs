using System.Text;

namespace RoyalCode.SmartCommands.Generators.Models;

public class GenericsGenerator : GeneratorNode
{
    private List<string>? generics;

    public void AddGeneric(string typeName)
    {
        generics ??= [];

        if (generics.Contains(typeName))
            return;

        generics.Add(typeName);
    }

    public override void Write(StringBuilder sb, int ident = 0)
    {
        if (generics is null)
            return;

        sb.Append("<");
        sb.Append(string.Join(", ", generics));
        sb.Append(">");
    }
}