using System.Text;

namespace RoyalCode.SmartCommands.Generators.Models;

public class AttributeGenerator : GeneratorNode
{
    private readonly string name;
    private ArgumentsGenerator? arguments;

    public AttributeGenerator(string name)
    {
        this.name = name;
    }
    
    public AttributeGenerator(string name, ValueNode argument)
    {
        this.name = name;
        Arguments.AddArgument(argument);
    }
    
    public AttributeGenerator(string name, params ValueNode[] arguments)
    {
        this.name = name;
        Arguments.AddArguments(arguments);
    }

    public ArgumentsGenerator Arguments => arguments ??= new();

    public override void Write(StringBuilder sb, int ident = 0)
    {
        sb.Ident(ident)
            .Append('[')
            .Append(name);
        
        arguments?.Write(sb);
        
        sb.Append(']').AppendLine();
    }
}