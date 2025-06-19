using System.Text;

namespace RoyalCode.SmartSelector.Generators.Models.Generators.Commands;

internal class ReturnCommand : GeneratorNode
{
    private readonly ValueNode valueNode;

    public bool AppendLine { get; set; } = true;

    public ReturnCommand(ValueNode valueNode)
    {
        this.valueNode = valueNode ?? throw new ArgumentNullException(nameof(valueNode));
    }

    public override void Write(StringBuilder sb, int ident = 0)
    {
        sb.Ident(ident).Append("return ").Append(valueNode.GetValue(ident)).Append(";");

        if (AppendLine)
            sb.AppendLine();
    }
}
