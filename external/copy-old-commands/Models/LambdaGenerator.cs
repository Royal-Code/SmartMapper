using System.Text;

namespace RoyalCode.SmartCommands.Generators.Models;

public class LambdaGenerator : GeneratorNode
{
    private GeneratorNodeList? commands;

    public ArgumentsGenerator Parameters { get; } = new();

    public GeneratorNodeList Commands => commands ??= new();

    public bool Async { get; set; }

    public bool Block { get; set; }

    public bool InLine { get; set; }

    public override void Write(StringBuilder sb, int ident = 0)
    {
        if (Async)
            sb.Append("async ");

        Parameters.Write(sb, ident);

        sb.Append(" => ");

        if (commands is null)
        {
            sb.Append("{ }");
            return;
        }

        if (Block)
        {
            if (InLine)
            {
                sb.Append("{ ");
                commands.InLine = true;
                commands.Write(sb);
                sb.Append(" }");
            }
            else
            {
                sb.AppendLine();
                sb.Ident(ident).AppendLine("{");
                commands.Write(sb, ident + 1);
                sb.Ident(ident).AppendLine("}");
            }
        }
        else
        {
            commands.Write(sb, ident);
        }
    }
}