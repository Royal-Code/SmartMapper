using System.Text;

namespace RoyalCode.SmartCommands.Generators.Models;

public abstract class GeneratorNode
{
    public abstract void Write(StringBuilder sb, int ident = 0);
}
