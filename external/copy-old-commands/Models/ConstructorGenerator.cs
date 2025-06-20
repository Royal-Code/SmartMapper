using System.Text;

namespace RoyalCode.SmartCommands.Generators.Models;

public class ConstructorGenerator : GeneratorNode
{
    private ModifiersGenerator? modifiers;
    private ParametersGenerator? parameters;
    private GeneratorNodeList? commands;
    private BaseParametersGenerator? baseParameters;

    public ConstructorGenerator(string name)
    {
        Name = name;
    }

    public ModifiersGenerator Modifiers => modifiers ??= new();

    public ParametersGenerator Parameters => parameters ??= new();

    public BaseParametersGenerator BaseParameters => baseParameters ??= new();

    public GeneratorNodeList Commands => commands ??= new();

    public string Name { get; set; }

    public override void Write(StringBuilder sb, int ident = 0)
    {
        sb.AppendLine();
        sb.Ident(ident);

        modifiers?.Write(sb);
        
        sb.Append(Name);
        parameters?.Write(sb);
        
        baseParameters?.Write(sb, ident);
        
        sb.AppendLine();
        sb.Ident(ident).Append('{');
        sb.AppendLine();
        
        int commandsIdent = ident + 1;
        commands?.Write(sb, commandsIdent);
        
        sb.Ident(ident).Append('}');
        sb.AppendLine();
    }
}