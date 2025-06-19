using Microsoft.CodeAnalysis;
using RoyalCode.SmartSelector.Generators.Models.Descriptors;

namespace RoyalCode.SmartSelector.Generators.Models;

internal class MatchSelection : IEquatable<MatchSelection>
{
    #region Factory

    public static MatchSelection Create(TypeDescriptor origin, TypeDescriptor target, SemanticModel model)
    {
        var originProperties = origin.CreateProperties(p => p.SetMethod is not null);
        var targetProperties = target.CreateProperties(p => p.GetMethod is not null);

        return Create(origin, originProperties, target, targetProperties, model);
    }

    public static MatchSelection Create(
        TypeDescriptor origin, IReadOnlyList<PropertyDescriptor> originProperties,
        TypeDescriptor target, IReadOnlyList<PropertyDescriptor> targetProperties,
        SemanticModel model)
    {
        List<PropertyMatch> matches = [];

        var targetType = new MatchTypeInfo(target, targetProperties);

        foreach (var originProperty in originProperties)
        {
            // para cada propriedade, seleciona a propriedade correspondente no target
            var targetSelection = PropertySelection.Select(originProperty, targetType);

            // se a propriedade for encontrada, avalia os tipos entre elas e a forma de atribuíção.
            AssignDescriptor? assignDescriptor = targetSelection is not null
                ? AssignDescriptorFactory.Create(originProperty.Type, targetSelection.PropertyType.Type, model)
                : null;

            // por fim, cria o match entre as propriedades, mesmo que não tenha sido encontrado.
            matches.Add(new PropertyMatch(originProperty, targetSelection, assignDescriptor));
        }

        return new MatchSelection(origin, target, matches);
    }

    #endregion

    private readonly TypeDescriptor originType;
    private readonly TypeDescriptor targetType;
    private readonly IReadOnlyList<PropertyMatch> propertyMatches;

    public MatchSelection(TypeDescriptor originType, TypeDescriptor targetType, IReadOnlyList<PropertyMatch> propertyMatches)
    {
        this.originType = originType;
        this.targetType = targetType;
        this.propertyMatches = propertyMatches;
    }

    public TypeDescriptor OriginType => originType;

    public TypeDescriptor TargetType => targetType;

    public IReadOnlyList<PropertyMatch> PropertyMatches => propertyMatches;

    public bool HasMissingProperties(out IReadOnlyList<PropertyDescriptor> missingProperties)
    {
        var missing = propertyMatches.Where(m => m.IsMissing).Select(m => m.Origin).ToList();
        missingProperties = missing.AsReadOnly();
        return missing.Count > 0;
    }

    public bool HasNotAssignableProperties(out IReadOnlyList<PropertyMatch> notAssignableProperties)
    {
        var notAssignable = propertyMatches.Where(m => !m.IsMissing && !m.CanAssign).ToList();
        notAssignableProperties = notAssignable.AsReadOnly();
        return notAssignable.Count > 0;
    }

    public void AddParentProperty(PropertySelection parent)
    {
        foreach (var propertyMatch in propertyMatches)
        {
            propertyMatch.Target?.WithParent(parent);
        }
    }

    public bool Equals(MatchSelection other)
    {
        if (other is null)
            return false;
        
        if (ReferenceEquals(this, other))
            return true;

        return originType.Equals(other.originType) &&
               propertyMatches.SequenceEqual(other.propertyMatches);
    }

    public override bool Equals(object? obj)
    {
        return obj is MatchSelection other && Equals(other);
    }

    public override int GetHashCode()
    {
        int hashCode = -1794252460;
        hashCode = hashCode * -1521134295 + EqualityComparer<TypeDescriptor>.Default.GetHashCode(originType);
        hashCode = hashCode * -1521134295 + EqualityComparer<IReadOnlyList<PropertyMatch>>.Default.GetHashCode(propertyMatches);
        return hashCode;
    }
}
