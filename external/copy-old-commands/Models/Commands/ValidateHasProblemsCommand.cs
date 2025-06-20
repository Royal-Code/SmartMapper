using System.Text;

namespace RoyalCode.SmartCommands.Generators.Models.Commands;

public class ValidateHasProblemsCommand : GeneratorNode
{
    private readonly string identifier;

    public ValidateHasProblemsCommand(string identifier)
    {
        this.identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
    }

    public override void Write(StringBuilder sb, int ident = 0)
    {
        sb.Ident(ident);
        sb.Append("if (");
        sb.Append(identifier).Append(".HasProblems(out var validationProblems)");
        sb.AppendLine(")");
        sb.IdentPlus(ident);
        sb.AppendLine("return validationProblems;");
        sb.AppendLine();
    }
}
