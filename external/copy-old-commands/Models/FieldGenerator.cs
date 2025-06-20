using System.Text;
using RoyalCode.SmartCommands.Generators.Models.Descriptors;

namespace RoyalCode.SmartCommands.Generators.Models;

public class FieldGenerator : GeneratorNode
{
    private ModifiersGenerator? modifiers;

    public ModifiersGenerator Modifiers => modifiers ??= new();

    public TypeDescriptor Type { get; set; }

    public string Name { get; set; }

    public ValueNode? Value { get; set; }

    public FieldGenerator(TypeDescriptor type, string name, bool privateReadonly)
    {
        Type = type;
        Name = name;
        if (privateReadonly)
        {
            modifiers = new();
            modifiers.Private();
            modifiers.Readonly();
        }
    }

    public override void Write(StringBuilder sb, int ident = 0)
    {
        sb.Ident(ident);
        modifiers?.Write(sb);
        sb.Append(Type.Name).Append(' ').Append(Name);

        if (Value is not null)
            sb.Append(" = ").Append(Value.GetValue(ident));

        sb.AppendLine(";");
    }
}