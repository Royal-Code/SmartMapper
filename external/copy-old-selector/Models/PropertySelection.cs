using RoyalCode.SmartSelector.Generators.Extensions;
using RoyalCode.SmartSelector.Generators.Models.Descriptors;
using System.Text;

namespace RoyalCode.SmartSelector.Generators.Models;

internal class PropertySelection : IEquatable<PropertySelection>
{
    private readonly PropertyDescriptor property;

    public PropertySelection(PropertyDescriptor property)
    {
        this.property = property;
    }

    /// <summary>
    /// The current selected property type.
    /// </summary>
    public PropertyDescriptor PropertyType => property;

    /// <summary>
    /// The declaring class type of the root selected property.
    /// if this selection does not have a parent, this selection will be the root.
    /// </summary>
    public PropertyDescriptor RootDeclaringType => Parent != null ? Parent.RootDeclaringType : property;

    /// <summary>
    /// The parent <see cref="PropertySelection"/>. Can be null.
    /// </summary>
    public PropertySelection? Parent { get; private set; }

    /// <summary>
    /// Writes the property path to a <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="sb"></param>
    public void WritePropertyPath(StringBuilder sb)
    {
        if (Parent is not null)
        {
            Parent.WritePropertyPath(sb);
            sb.Append('.');
        }
        sb.Append(property.Name);
    }

    public static PropertySelection? Select(PropertyDescriptor property, MatchTypeInfo targetType)
    {
        PropertySelection? ps =null;

        var targetProperty = targetType.Properties.FirstOrDefault(p => p.Name == property.Name);
        
        if(targetProperty != null)
        {
            return new PropertySelection(targetProperty);
        }

        var parts = property.Name.SplitUpperCase();
        if (parts is not null)
        {
            ps = SelectPart(parts, targetType);
        }

        return ps;
    }

    private static PropertySelection? SelectPart(
        string[] parts,
        MatchTypeInfo targetType,
        int position = 0,
        PropertySelection? parent = null)
    {
        var currentProperty = string.Empty;

        for (int i = position; i < parts.Length; i++)
        {
            currentProperty += parts[i];
            var property = targetType.Properties.FirstOrDefault(p => p.Name == currentProperty);
            if (property is not null)
            {
                var ps = parent == null ? new PropertySelection(property) : parent.SelectChild(property);
                if (i + 1 < parts.Length)
                {
                    // creates the next MatchTypeInfo
                    var nextTypeInfo = new MatchTypeInfo(
                        property.Type,
                        property.Type.CreateProperties(p => p.GetMethod is not null));

                    var nextPs = SelectPart(parts, nextTypeInfo, i + 1, ps);
                    if (nextPs is not null)
                        return nextPs;
                }
                else
                {
                    return ps;
                }
            }
        }

        return null;
    }

    private PropertySelection SelectChild(PropertyDescriptor property)
    {
        if (property is null)
            throw new ArgumentNullException(nameof(property));

        var newSelection = new PropertySelection(property)
        {
            Parent = this
        };

        return newSelection;
    }

    public void WithParent(PropertySelection parent)
    {
        if (Parent is null)
            Parent = parent;
        else
            Parent.WithParent(parent);
    }

    public bool Equals(PropertySelection other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return property.Equals(other.property) &&
            Equals(Parent, other.Parent);
    }

    public override bool Equals(object? obj)
    {
        return obj is PropertySelection other && Equals(other);
    }

    public override int GetHashCode()
    {
        int hashCode = 1750517797;
        hashCode = hashCode * -1521134295 + EqualityComparer<PropertyDescriptor>.Default.GetHashCode(property);
        hashCode = hashCode * -1521134295 + EqualityComparer<PropertySelection?>.Default.GetHashCode(Parent);
        return hashCode;
    }

    
}
