using System.Text;

namespace RoyalCode.SmartSelector.Generators.Models.Generators;

internal class GeneratorNodeList : GeneratorNode
{
    private List<GeneratorNode>? nodes;

    public bool InLine { get; set; }

    public void Add(GeneratorNode generator)
    {
        nodes ??= [];
        nodes.Add(generator);
    }

    public IEnumerable<T> Enumerate<T>() => nodes?.OfType<T>() ?? [];

    public override void Write(StringBuilder sb, int ident = 0)
    {
        if (nodes is null)
            return;

        bool first = true;

        foreach (var node in nodes)
        {
            if (InLine)
            {
                if (!first)
                    sb.Append(" ");
                first = false;

                node.Write(sb, 0);
            }
            else
            {
                node.Write(sb, ident);
            }
        }
    }
}
