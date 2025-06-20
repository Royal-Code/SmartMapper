using System.Text;

namespace RoyalCode.SmartCommands.Generators.Models.Commands;

public sealed class Command : GeneratorNode
{
    private readonly GeneratorNode generatorNode;

    public Command(GeneratorNode generatorNode)
    {
        this.generatorNode = generatorNode;
    }

    public bool Await { get; set; } = false;

    public bool NewLine { get; set; } = true;

    public bool InLine { get; set; }

    public override void Write(StringBuilder sb, int ident = 0)
    {
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