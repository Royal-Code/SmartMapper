using System.Text;

namespace RoyalCode.SmartCommands.Generators.Models;

public class WhereGenerator : GeneratorNode
{
    public WhereGenerator(string argument, string type)
    {
        Argument = argument;
        Type = type;
    }

    public string Argument { get; set; }

    public string Type { get; set; }

    public override void Write(StringBuilder sb, int ident = 0)
    {
        sb.AppendLine()
            .Ident(ident)
            .Append("where ").Append(Argument).Append(" : ").Append(Type);
    }
}