namespace RoyalCode.SmartCommands.Generators.Models;

public abstract class ValueNode
{
    public static implicit operator ValueNode(string v) => new StringValueNode(v);
    public static implicit operator ValueNode(GeneratorNode g) => new GeneratorValueNode(g);

    public abstract string GetValue(int ident);
}