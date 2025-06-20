using System.Text;

namespace RoyalCode.SmartCommands.Generators.Models;

public class BaseParametersGenerator : GeneratorNode
{
    private List<string>? parameters;

    public void AddParamter(string parameter)
    {
        parameters ??= [];
        parameters.Add(parameter);
    }

    public override void Write(StringBuilder sb, int ident = 0)
    {
        if (parameters is null)
            return;

        sb.AppendLine()
            .IdentPlus(ident)
            .Append(" : base (");

        bool first = true;
        foreach(var p in parameters)
        {
            if (first)
                first = false;
            else
                sb.Append(", ");
            sb.Append(p);
        }

        sb.Append(")");
    }
}