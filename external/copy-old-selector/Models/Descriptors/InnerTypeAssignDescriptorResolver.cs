using Microsoft.CodeAnalysis;

namespace RoyalCode.SmartSelector.Generators.Models.Descriptors;

internal class InnerTypeAssignDescriptorResolver : IAssignDescriptorResolver
{
    public bool TryCreateAssignDescriptor(
        TypeDescriptor leftType, 
        TypeDescriptor rightType,
        SemanticModel model, 
        out AssignDescriptor? descriptor)
    {
        descriptor = null;

        // check if the left type is a class or struct and has a public parameterless constructor
        if (leftType.Symbol is null || rightType.Symbol is null
            || !leftType.Symbol.Constructors.Any(c => c.Parameters.Length == 0))
            return false;

        // obtém propriedades do tipo de origem
        var leftProperties = leftType.CreateProperties(p => p.SetMethod is not null);

        // valida se tem propriedades
        if (leftProperties.Count == 0)
            return false;

        // obtém propriedades do tipo de destino
        var rightProperties = rightType.CreateProperties(p => p.GetMethod is not null);

        // valida se tem propriedades
        if (rightProperties.Count == 0)
            return false;

        // faz o match entre as propriedades
        // match das propriedades da classe com o attributo e a classe definida no TFrom.
        var matchSelection = MatchSelection.Create(leftType, leftProperties, rightType, rightProperties, model);

        // se tem problemas, não é possível fazer o match.
        if (matchSelection.HasMissingProperties(out _) || matchSelection.HasNotAssignableProperties(out _))
        {
            return false;
        }

        descriptor = new AssignDescriptor()
        {
            AssignType = AssignType.NewInstance,
            InnerSelection = matchSelection
        };
        
        return true;
    }
}
