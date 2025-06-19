using System.Text;

namespace RoyalCode.SmartSelector.Generators.Models.Generators.Commands;

internal class CompileLambdaGenerator : GeneratorNode
{
    private readonly string targetTypeName;
    private readonly string paramName;

    public CompileLambdaGenerator(string targetTypeName, string paramName)
    {
        this.targetTypeName = targetTypeName;
        this.paramName = paramName;
    }

    public override void Write(StringBuilder sb, int ident = 0)
    {
        // (select{Target}Func ??= Select{Target}Expression.Compile())({paramName})

        sb.Append("(select").Append(targetTypeName).Append("Func")
            .Append(" ??= Select").Append(targetTypeName).Append("Expression.Compile())")
            .Append('(').Append(paramName).Append(')').AppendLine(";");
    }
}
