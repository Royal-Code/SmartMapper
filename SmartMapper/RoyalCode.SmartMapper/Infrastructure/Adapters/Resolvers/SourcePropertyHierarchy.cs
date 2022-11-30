using System.Text;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

public class SourcePropertyHierarchy
{
    public SourcePropertyHierarchy(SourceProperty current)
    {
        Current = current;
    }

    public SourceProperty Current { get; }

    public SourceProperty? Parent { get; private set; }
    
    public IEnumerable<SourceProperty> Children { get; } = new List<SourceProperty>();

    public void AddChild(SourceProperty child)
    {
        if (child.Hierarchy.Parent is not null)
            throw new InvalidOperationException("The child already has a parent.");

        child.Hierarchy.Parent = Current;
        Children.Append(child);
    }

    internal string GetPropertyPathString()
    {
        var sb = new StringBuilder();
        var current = Current;
        while (current is not null)
        {
            sb.Insert(0, current.PropertyInfo.Name);
            current = current.Hierarchy.Parent;
            if (current is not null)
                sb.Insert(0, ".");
        }

        return sb.ToString();
    }
}