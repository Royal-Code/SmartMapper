using RoyalCode.SmartSelector.Generators.Models.Descriptors;
using System.Text;

namespace RoyalCode.SmartSelector.Generators.Models.Generators;

internal class ParameterGenerator : GeneratorNode
{
    private readonly ParameterDescriptor parameter;
    private readonly ValueNode? defaultValue;
    
    public ParameterGenerator(ParameterDescriptor parameter, ValueNode? defaultValue = null)
    {
        this.parameter = parameter;
        this.defaultValue = defaultValue;
    }

    public bool ThisModifier { get; set; }

    public ParameterDescriptor ParameterDescriptor => parameter;

    public ParameterGenerator Clone()
    {
        return new ParameterGenerator(parameter, defaultValue)
        {
            ThisModifier = ThisModifier
        };
    }

    public void AddUsings(UsingsGenerator usings)
    {
        usings.AddNamespaces(parameter);
    }

    public override void Write(StringBuilder sb, int ident = 0)
    {
        if (ThisModifier)
            sb.Append("this ");

        sb.Append(parameter.Type.Name).Append(' ').Append(parameter.Name);
        if (defaultValue is not null)
            sb.Append(" = ").Append(defaultValue.GetValue(ident));
    }
}