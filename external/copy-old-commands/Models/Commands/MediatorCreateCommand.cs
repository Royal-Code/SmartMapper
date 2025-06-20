using System.Text;

namespace RoyalCode.SmartCommands.Generators.Models.Commands;

#pragma warning disable S4035 // IEquatable
#pragma warning disable S2328 // GetHashCode

public class MediatorCreateCommand : GeneratorNode
{
    private readonly string varIdentifier;
    private readonly string modelType;
    private readonly string resultType;
    private readonly ValueNode decorators;
    private readonly ValueNode handler;
    private readonly string modelIdentifier;
    private readonly string ctIdentifier;

    public MediatorCreateCommand(
        string varIdentifier,
        string modelType,
        string resultType,
        ValueNode decorators,
        ValueNode handler,
        string modelIdentifier,
        string ctIdentifier)
    {
        this.varIdentifier = varIdentifier;
        this.modelType = modelType;
        this.resultType = resultType;
        this.decorators = decorators;
        this.handler = handler;
        this.modelIdentifier = modelIdentifier;
        this.ctIdentifier = ctIdentifier;
    }

    public MethodInvokeGenerator CreateInvokeNextAsync()
    {
        var invoke = new MethodInvokeGenerator(varIdentifier, "NextAsync")
        {
            Await = true
        };
        return invoke;
    }

    public AssignValueCommand CreateAssignInvoke(string varName)
    {
        var invoke = new MethodInvokeGenerator(varIdentifier, "NextAsync")
        {
            Await = true
        };
        return new AssignValueCommand(varName, invoke);
    }

    public override void Write(StringBuilder sb, int ident = 0)
    {
        sb.Ident(ident).Append("var ").Append(varIdentifier).Append(" = ")
            .Append("new Mediator<")
            .Append(modelType)
            .Append(", ")
            .Append(resultType.TryGetInnerTaskType())
            .Append(">")
            .AppendLine("(");

        sb.IdentPlus(ident).Append(decorators.GetValue(ident)).AppendLine(",");
        sb.IdentPlus(ident).Append(handler.GetValue(ident)).AppendLine(",");
        sb.IdentPlus(ident).Append(modelIdentifier).AppendLine(",");
        sb.IdentPlus(ident).Append(ctIdentifier).AppendLine(");");

        sb.AppendLine();
    }
}
