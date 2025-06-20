using System.Text;
using RoyalCode.SmartCommands.Generators.Models.Descriptors;

namespace RoyalCode.SmartCommands.Generators.Models.Commands;

public class FindEditEntityCommand : GeneratorNode
{
    private readonly EditTypeDescriptor descriptor;
    private readonly string accessorVarName;

    public FindEditEntityCommand(
        EditTypeDescriptor descriptor, 
        string accessorVarName)
    {
        this.descriptor = descriptor;
        this.accessorVarName = accessorVarName;
    }

    /// <summary>
    /// <code>
    /// var personaEntry = this.accessor.FindEntityAsync{Persona, Guid}(model.PersonaId, ct);
    /// if (personaEntry.NotFound(out notFoundProblems))
    ///     return notFound;
    /// var persona = personaEntry.Entity;
    /// </code>
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="ident"></param>
    public override void Write(StringBuilder sb, int ident = 0)
    {
        bool entityVarDeclared = false;
        var idParamName = $"{descriptor.Parameter.Name}Id";

        // quando a propriedade do comando pode ser nula,
        // deve ser feito um if para verificar se se deve executar o find.
        if (descriptor.IdType.MayBeNull)
        {
            // a variável do parâmetro deve ser declarada antes do if
            sb.Ident(ident);
            sb.Append(descriptor.Parameter.Name).Append(' ').Append(descriptor.Parameter.Name).Append(" = null;").AppendLine();
            entityVarDeclared = true;

            // declaração do if
            sb.Ident(ident);
            sb.Append("if (").Append(idParamName).Append(" is not null)").AppendLine();
            sb.Ident(ident);
            sb.AppendLine("{");

            ident++;
        }

        sb.Ident(ident);
        sb.Append("var ").Append(descriptor.Parameter.Name).Append("Entry = ")
            .Append("await this.").Append(accessorVarName).Append(".FindEntityAsync<")
            .Append(descriptor.Parameter.Type.UnderlyingType).Append(", ")
            .Append(descriptor.IdType.UnderlyingType).Append(">(")
            .Append(idParamName);

        if (descriptor.IdType.IsNullable)
            sb.Append(".Value");

        sb.Append(", ct);")
            .AppendLine();

        sb.Ident(ident);
        sb.Append("if (").Append(descriptor.Parameter.Name).Append("Entry.NotFound(out notFoundProblem))").AppendLine();

        sb.IdentPlus(ident);
        sb.AppendLine("return notFoundProblem;");

        sb.Ident(ident);
        if (!entityVarDeclared)
            sb.Append("var ");
        sb.Append(descriptor.Parameter.Name).Append(" = ").Append(descriptor.Parameter.Name).AppendLine("Entry.Entity;");

        if (descriptor.IdType.MayBeNull)
        {
            ident--;
            sb.Ident(ident);
            sb.AppendLine("}");
        }

        sb.AppendLine();
    }
}