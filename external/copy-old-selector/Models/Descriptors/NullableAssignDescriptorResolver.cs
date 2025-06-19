using Microsoft.CodeAnalysis;

namespace RoyalCode.SmartSelector.Generators.Models.Descriptors;

internal class NullableAssignDescriptorResolver : IAssignDescriptorResolver
{
    public bool TryCreateAssignDescriptor(
        TypeDescriptor leftType,
        TypeDescriptor rightType,
        SemanticModel model,
        out AssignDescriptor? descriptor)
    {
        // check if the rightType property is nullable and the leftType property is not nullable,
        // and the NullableUnderlyingType of rightType property type is the same of the leftType property type.

        if (!rightType.IsNullable || leftType.IsNullable || rightType.Symbol is not INamedTypeSymbol rightSymbol)
        {
            descriptor = null;
            return false;
        }

        var nullUnderType = rightSymbol.TypeArguments[0];

        if (SymbolEqualityComparer.Default.Equals(nullUnderType, leftType.Symbol))
        {
            descriptor = new AssignDescriptor
            {
                AssignType = AssignType.NullableTernary,
            };
            return true;
        }

        // if types are not the same, check if they are enums and if they are equivalent enums.
        var areEquivalentEnums = CastAssignDescriptorResolver.Enums.AreEquivalentEnums(leftType.Symbol, nullUnderType);

        if (areEquivalentEnums)
        {
            descriptor = new AssignDescriptor
            {
                AssignType = AssignType.NullableTernaryCast,
            };
            return true;
        }

        descriptor = null;
        return false;
    }
}
