using RoyalCode.SmartSelector.Generators.Models.Descriptors;
using System.Text;

namespace RoyalCode.SmartSelector.Generators.Models.Generators;

internal class PropertyGenerator : GeneratorNode
{
    private ModifiersGenerator? modifiers;

    public PropertyGenerator(TypeDescriptor type, string name, bool canGet = true, bool canSet = true)
    {
        Type = type;
        Name = name;
        CanGet = canGet;
        CanSet = canSet;
    }

    public ModifiersGenerator Modifiers => modifiers ??= new();

    public TypeDescriptor Type { get; set; }

    public string Name { get; set; }

    public bool CanGet { get; set; } = true;

    public bool CanSet { get; set; } = true;

    public ValueNode? Value { get; set; }

    public override void Write(StringBuilder sb, int ident = 0)
    {
        sb.AppendLine();
        sb.Ident(ident);
        
        modifiers?.Write(sb);
        sb.Append(Type.Name).Append(' ').Append(Name).Append(" { ");
        
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