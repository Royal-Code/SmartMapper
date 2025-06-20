using System.Text;

namespace RoyalCode.SmartCommands.Generators.Models;

public class GeneratorValueNode : ValueNode
{
    private readonly GeneratorNode generatorNode;
    private string? cachedValue;

    public GeneratorValueNode(GeneratorNode generatorNode)
    {
        this.generatorNode = generatorNode;
    }

    public override string GetValue(int ident)
    {
        if (cachedValue is not null)
            return cachedValue;

        var sb = new StringBuilder();
        generatorNode.Write(sb, ident);
        cachedValue = sb.ToString();

        return cachedValue;
    }
}