using System.Text;

namespace RoyalCode.SmartCommands.Generators.Models;

public class ArgumentsGenerator : GeneratorNode
{
    private List<ValueNode>? arguments;

    public bool InLine { get; set; } = true;

    public void AddArgument(ValueNode argument)
    {
        arguments ??= [];
        arguments.Add(argument);
    }
    
    public void AddArguments(ValueNode[] arguments)
    {
        this.arguments ??= [];
        this.arguments.AddRange(arguments);
    }

    public override void Write(StringBuilder sb, int ident = 0)
    {
        sb.Append("(");
        if (arguments is null)
        {
            sb.Append(")");
            return;
        }

        bool first = true;
        foreach (var arg in arguments)
        {
            if (first)
            {
                if (!InLine)
                    sb.AppendLine().IdentPlus(ident);
                first = false;
            }
            else
            {
                if (!InLine)
                    sb.AppendLine(",").IdentPlus(ident);
                else
                    sb.Append(", ");
            }

            sb.Append(arg.GetValue(ident));
        }

        sb.Append(")");
    }
}