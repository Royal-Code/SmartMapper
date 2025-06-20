using System.Text;
using RoyalCode.SmartCommands.Generators.Models.Descriptors;

namespace RoyalCode.SmartCommands.Generators.Models.Commands;

public class CompleteUnitOfWorkCommand : GeneratorNode
{
    private readonly GeneratorNode methodInvoke;
    private readonly bool invokeIsAsync;
    private readonly TypeDescriptor commandReturnType;
    private readonly string accessorVarName;
    private readonly string commandResultVarName;
    private readonly bool produceNewEntity;
    private readonly bool hasDecorators;

    public CompleteUnitOfWorkCommand(
        GeneratorNode methodInvoke,
        bool invokeIsAsync,
        TypeDescriptor commandReturnType,
        string accessorVarName,
        string commandResultVarName,
        bool produceNewEntity = false,
        bool hasDecorators = false)
    {
        this.methodInvoke = methodInvoke;
        this.invokeIsAsync = invokeIsAsync;
        this.commandReturnType = commandReturnType;
        this.accessorVarName = accessorVarName;
        this.commandResultVarName = commandResultVarName;
        this.produceNewEntity = produceNewEntity;
        this.hasDecorators = hasDecorators;
    }

    public override void Write(StringBuilder sb, int ident = 0)
    {
        // se não tem decorators e se for void, executa o método e completa o UnitOfWork
        if (!hasDecorators && (commandReturnType.IsVoid || commandReturnType.IsVoidTask))
        {
            new Command(methodInvoke) 
            { 
                Await = commandReturnType.IsVoidTask,
                NewLine = true,
            }.Write(sb, ident);
            
            var invokeCompleteAsync = new MethodInvokeGenerator($"this.{accessorVarName}", "CompleteAsync", "ct")
            {
                Await = true
            };

            new ReturnCommand(invokeCompleteAsync).Write(sb, ident);

            return;
        }

        var commandReturnResult = commandReturnType.Name.StartsWith("Result") ||
                                  commandReturnType.Name.StartsWith("Task<Result");

        AssignValueCommand? assignValueCommand = null;
        MethodInvokeGenerator? invokeAddEntityAsync = null;
        GeneratorNode final;

        // se não retorna result, então deve ser criada uma variável para armazenar o resultado
        // e passar por MapAsync no retorno do CompleteAsync
        if (!commandReturnResult)
        {
            assignValueCommand = new AssignValueCommand($"var {commandResultVarName}", methodInvoke);

            if (produceNewEntity)
            {
                invokeAddEntityAsync = new MethodInvokeGenerator($"this.{accessorVarName}", "AddEntityAsync");
                invokeAddEntityAsync.AddArgument(commandResultVarName);
                invokeAddEntityAsync.AddArgument("ct");
                invokeAddEntityAsync.Await = true;
            }

            var invokeCompleteAsync = new MethodInvokeGenerator($"this.{accessorVarName}", "CompleteAsync", "ct")
            {
                Await = true
            };

            final = new MethodInvokeGenerator(
                invokeCompleteAsync, "MapAsync", commandResultVarName);
        }
        else
        {
            ValueNode identifier = methodInvoke;
            if (produceNewEntity)
            {
                var addEntityAsync = new MethodInvokeGenerator(methodInvoke, "ContinueAsync");
                addEntityAsync.AddArgument($"this.{accessorVarName}");
                addEntityAsync.AddArgument($"async (e, a) => await a.AddEntityAsync(e, ct)");
                addEntityAsync.LineIdent = true;
                identifier = addEntityAsync;
            }

            // verifica parâmetro da expressão lambda,
            // se retorna um Result<T> deve ser (_, a)
            // senão deve ser (a)
            var lambdaParam = commandReturnType.Name.StartsWith("Task<Result<") ||
                              commandReturnType.Name.StartsWith("Result<")
                ? "_, a"
                : "a";

            var invokeContinueAsync = new MethodInvokeGenerator(identifier, "ContinueAsync");
            invokeContinueAsync.AddArgument($"this.{accessorVarName}");
            invokeContinueAsync.AddArgument($"async ({lambdaParam}) => await a.CompleteAsync(ct)");
            invokeContinueAsync.LineIdent = produceNewEntity;

            if (!invokeIsAsync)
                invokeContinueAsync.Await = true;

            final = invokeContinueAsync;
        }

        if (assignValueCommand is not null)
        {
            assignValueCommand.Write(sb, ident);
            sb.AppendLine();

            if (invokeAddEntityAsync is not null)
            {
                // invoke não é comando, então não gera ident, nem new line, nem ';'
                // então é necessário escrever aqui
                sb.Ident(ident);
                invokeAddEntityAsync.Write(sb, ident);
                sb.AppendLine(";").AppendLine();
            }
        }

        new ReturnCommand(final).Write(sb, ident);
    }
}