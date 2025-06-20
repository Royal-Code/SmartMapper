using System.Text;

namespace RoyalCode.SmartCommands.Generators.Models;

public class MethodInvokeGenerator : GeneratorNode
{
    private readonly ValueNode identifier;
    private readonly string method;
    private readonly ArgumentsGenerator arguments;

    public MethodInvokeGenerator(ValueNode identifier, string method, ArgumentsGenerator? arguments = null)
    {
        this.identifier = identifier;
        this.method = method;
        this.arguments = arguments ?? new();
    }

    public MethodInvokeGenerator(ValueNode identifier, string method, ValueNode argument)
    {
        this.identifier = identifier;
        this.method = method;
        
        arguments = new();
        arguments.AddArgument(argument);
    }

    public bool Await { get; set; }

    public bool LineIdent { get; set; }

    public void AddArgument(ValueNode arg) => arguments.AddArgument(arg);

    public void UseArgumentPerLine() => arguments.InLine = false;

    public override void Write(StringBuilder sb, int ident = 0)
    {
        if (Await)
            sb.Append("await ");

        sb.Append(identifier.GetValue(ident));

        if (LineIdent)
            sb.AppendLine().IdentPlus(ident);

        sb.Append(".").Append(method);

        arguments.Write(sb, ident + (LineIdent ? 1: 0));
    }
}