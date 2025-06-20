using System.Text;
using RoyalCode.SmartCommands.Generators.Models.Descriptors;

namespace RoyalCode.SmartCommands.Generators.Models.Commands;

public class FindEntityCommand : GeneratorNode
{
    private readonly ParameterDescriptor parameter;
    private readonly PropertyDescriptor property;
    private readonly string accessorVarName;
    private readonly string modelVarName;

    public FindEntityCommand(
        ParameterDescriptor parameter, 
        PropertyDescriptor property, 
        string accessorVarName,
        string modelVarName)
    {
        this.parameter = parameter;
        this.property = property;
        this.accessorVarName = accessorVarName;
        this.modelVarName = modelVarName;
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

        // quando a propriedade do comando pode ser nula,
        // deve ser feito um if para verificar se se deve executar o find.
        if (property.Type.MayBeNull)
        {
            // a variável do parâmetro deve ser declarada antes do if
            sb.Ident(ident);
            sb.Append(parameter.Type.Name).Append(' ').Append(parameter.Name).Append(" = null;").AppendLine();
            entityVarDeclared = true;

            // declaração do if
            sb.Ident(ident);
            sb.Append("if (").Append(modelVarName).Append('.').Append(property.Name).Append(" is not null)").AppendLine();
            sb.Ident(ident);
            sb.AppendLine("{");

            ident++;
        }

        sb.Ident(ident);
        sb.Append("var ").Append(parameter.Name).Append("Entry = ")
            .Append("await this.").Append(accessorVarName).Append(".FindEntityAsync<")
            .Append(parameter.Type.UnderlyingType).Append(", ")
            .Append(property.Type.UnderlyingType).Append(">(")
            .Append(modelVarName).Append('.').Append(property.Name);

        if (property.Type.IsNullable)
            sb.Append(".Value");

        sb.Append(", ct);")
            .AppendLine();

        sb.Ident(ident);
        sb.Append("if (").Append(parameter.Name).Append("Entry.NotFound(out notFoundProblem))").AppendLine();

        sb.IdentPlus(ident);
        sb.AppendLine("return notFoundProblem;");

        sb.Ident(ident);
        if (!entityVarDeclared)
            sb.Append("var ");
        sb.Append(parameter.Name).Append(" = ").Append(parameter.Name).AppendLine("Entry.Entity;");

        if (property.Type.MayBeNull)
        {
            ident--;
            sb.Ident(ident);
            sb.AppendLine("}");
        }

        sb.AppendLine();
    }
}