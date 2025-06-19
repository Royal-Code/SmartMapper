using System.Text;

namespace RoyalCode.SmartSelector.Generators.Models.Generators.Commands;

internal sealed class Command : GeneratorNode
{
    private readonly GeneratorNode generatorNode;

    public Command(GeneratorNode generatorNode)
    {
        this.generatorNode = generatorNode;
    }

    public bool Await { get; set; } = false;

    public bool NewLine { get; set; } = true;

    public bool InLine { get; set; }

    public bool Idented { get; set; } = true;

    public override void Write(StringBuilder sb, int ident = 0)
    {
        if (Idented)
            sb.Ident(ident);

        if (Await)
            sb.Append("await ");

        generatorNode.Write(sb, ident);

        if (InLine)
        {
            sb.Append(";");
        }
        else
        {
            sb.AppendLine(";");
            if (NewLine)
                sb.AppendLine();
        }
    }
}