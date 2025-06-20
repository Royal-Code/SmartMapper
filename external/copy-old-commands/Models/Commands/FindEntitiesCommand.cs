using System.Text;
using RoyalCode.SmartCommands.Generators.Models.Descriptors;

namespace RoyalCode.SmartCommands.Generators.Models.Commands;

public class FindEntitiesCommand: GeneratorNode
{
    private readonly ParameterDescriptor parameter;
    private readonly PropertyDescriptor property;
    private readonly string accessorVarName;
    private readonly string modelVarName;

    public FindEntitiesCommand(
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
        // Obtém o tipo da entidade, a qual vem do parâmetro,
        // como é coleção, o parâmetro pode ser um array ou uma lista.
        var entityType = parameter.Type.IsArray
            ? parameter.Type.ArrayType
            : parameter.Type.GenericType;
        var entityVarName = $"{entityType[0].ToString().ToLower()}{entityType.Substring(1)}";

        // Da mesma forma, obtém o tipo da propriedade, a qual será utilizada para buscar a entidade.
        var propertyType = property.Type.IsArray
            ? property.Type.ArrayType
            : property.Type.GenericType;

        // declaração da variável que armazenará as entidades
        sb.Ident(ident);
        sb.Append("List<").Append(entityType).Append("> ").Append(parameter.Name).AppendLine(" = [];");


        // quando a propriedade do comando pode ser nula,
        // deve ser feito um if para verificar se se deve executar o find.
        if (property.Type.MayBeNull)
        {
            // declaração do if
            sb.Ident(ident);
            sb.Append("if (").Append(modelVarName).Append('.').Append(property.Name).Append(" is not null)").AppendLine();
            sb.Ident(ident);
            sb.AppendLine("{");

            ident++;
        }

        var idVarName = $"{entityVarName}Id";

        // foreach para buscar as entidades
        sb.Ident(ident);
        sb.Append("foreach (var ").Append(idVarName).Append(" in ").Append(modelVarName).Append('.').Append(property.Name).AppendLine(")");
        sb.Ident(ident);
        sb.AppendLine("{");
        ident++;

        sb.Ident(ident);
        sb.Append("var ").Append(entityVarName).Append("Entry = ")
            .Append("await this.").Append(accessorVarName).Append(".FindEntityAsync<")
            .Append(entityType).Append(", ")
            .Append(propertyType).Append(">(")
            .Append(idVarName)
            .Append(", ct);")
            .AppendLine();

        sb.Ident(ident);
        sb.Append("if (").Append(entityVarName).Append("Entry.NotFound(out notFoundProblem))").AppendLine();

        sb.IdentPlus(ident);
        sb.AppendLine("return notFoundProblem;");

        sb.Ident(ident);
        sb.Append(parameter.Name).Append(".Add(").Append(entityVarName).AppendLine("Entry.Entity);");

        // finaliza o foreach
        ident--;
        sb.Ident(ident);
        sb.AppendLine("}");

        if (property.Type.MayBeNull)
        {
            ident--;
            sb.Ident(ident);
            sb.AppendLine("}");
        }

        sb.AppendLine();
    }
}