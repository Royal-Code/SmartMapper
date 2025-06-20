namespace RoyalCode.SmartCommands.Generators.Models;

public sealed class StringValueNode : ValueNode
{
    private readonly string value;
    
    public StringValueNode(string value)
    {
        this.value = value;
    }

    public override string GetValue(int ident) => value;
}