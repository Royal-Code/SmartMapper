using System.Text;

namespace RoyalCode.SmartCommands.Generators.Models;

public class ModifiersGenerator : GeneratorNode
{
    private List<string>? modifiers;
    
    public void Add(string modifier)
    {
        modifiers ??= [];
        
        if (modifiers.Contains(modifier))
            return;
        
        modifiers.Add(modifier);
    }
    
    public void Public()
    {
        Add("public");
    }
    
    public void Private()
    {
        Add("private");
    }
    
    public void Protected()
    {
        Add("protected");
    }
    
    public void Internal()
    {
        Add("internal");
    }
    
    public void Static()
    {
        Add("static");
    }
    
    public void Virtual()
    {
        Add("virtual");
    }
    
    public void Abstract()
    {
        Add("abstract");
    }
    
    public void Sealed()
    {
        Add("sealed");
    }
    
    public void Override()
    {
        Add("override");
    }
    
    public void New()
    {
        Add("new");
    }
    
    public void Readonly()
    {
        Add("readonly");
    }
    
    public void Const()
    {
        Add("const");
    }
    
    public void Volatile()
    {
        Add("volatile");
    }
    
    public void Unsafe()
    {
        Add("unsafe");
    }
    
    public void Extern()
    {
        Add("extern");
    }
    
    public void Partial()
    {
        Add("partial");
    }
    
    public void Async()
    {
        Add("async");
    }

    public ModifiersGenerator CloneForMethodImplementation()
    {
        var newModifiers = new ModifiersGenerator();
        if (modifiers is not null)
            foreach(var modifier in modifiers)
            {
                if (modifier == "abstract")
                    newModifiers.Add("override");
                else
                    newModifiers.Add(modifier);
            }

        return newModifiers;

    }

    public override void Write(StringBuilder sb, int ident = 0)
    {
        if (modifiers is null or { Count: 0 })
            return;
        
        sb.Append(string.Join(" ", modifiers));
        sb.Append(" ");
    }
}