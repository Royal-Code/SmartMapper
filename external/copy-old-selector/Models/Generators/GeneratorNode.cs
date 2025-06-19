using System.Text;

namespace RoyalCode.SmartSelector.Generators.Models.Generators;

internal abstract class GeneratorNode
{
    public abstract void Write(StringBuilder sb, int ident = 0);
}
