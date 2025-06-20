using System.Text;

namespace RoyalCode.SmartCommands.Generators.Models;

public class PropertyGenerator : GeneratorNode
{
    private ModifiersGenerator? modifiers;

    public PropertyGenerator(string type, string name)
    {
        Type = type;
        Name = name;
    }

    public ModifiersGenerator Modifiers => modifiers ??= new();

    public string Type { get; set; }

    public string Name { get; set; }

    public bool CanGet { get; set; } = true;

    public bool CanSet { get; set; } = true;

    public ValueNode? Value { get; set; }

    public override void Write(StringBuilder sb, int ident = 0)
    {
        sb.AppendLine();
        sb.Ident(ident);
        
        modifiers?.Write(sb);
        sb.Append(Type).Append(' ').Append(Name).Append(" { ");
        
        if (CanGet)
            sb.Append("get; ");
        
        if (CanSet)
            sb.Append("set; ");
        
        sb.Append("}");

        if (Value is not null)
        {
            sb.Append(" = ").Append(Value.GetValue(ident));
            sb.AppendLine(";");
        }
        else
        {
            sb.AppendLine();
        }
    }
}