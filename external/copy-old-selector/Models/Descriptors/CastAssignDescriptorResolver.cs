using Microsoft.CodeAnalysis;

namespace RoyalCode.SmartSelector.Generators.Models.Descriptors;

internal class CastAssignDescriptorResolver : IAssignDescriptorResolver
{
    public bool TryCreateAssignDescriptor(
        TypeDescriptor leftType,
        TypeDescriptor rightType, 
        SemanticModel model, 
        out AssignDescriptor? descriptor)
    {
        var areEquivalentEnums = Enums.AreEquivalentEnums(leftType.Symbol, rightType.Symbol);

        if (!areEquivalentEnums)
        {
            descriptor = null;
            return false;
        }

        descriptor = new AssignDescriptor
        {
            AssignType = AssignType.SimpleCast,
        };

        return true;
    }

    public static class Enums
    {
        public static bool AreEquivalentEnums(ITypeSymbol? left, ITypeSymbol? right)
        {
            if (left is not INamedTypeSymbol leftSymbol || right is not INamedTypeSymbol rightSymbol)
            {
                return false;
            }

            // check if left are nullable
            if (leftSymbol.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T)
            {
                // get the nullable underlying type of the enum leftSymbol
                if (leftSymbol.TypeArguments[0] is INamedTypeSymbol leftUnderlyingSymbol)
                    leftSymbol = leftUnderlyingSymbol;

                // check if right are nullable
                if (rightSymbol.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T)
                {
                    // get the nullable underlying type of the enum rightSymbol
                    if (rightSymbol.TypeArguments[0] is INamedTypeSymbol rightUnderlyingSymbol)
                        rightSymbol = rightUnderlyingSymbol;
                }
            }

            // check if the rightType property is an enum and the leftType property is a enum.
            var isEnums = leftSymbol.TypeKind == TypeKind.Enum && rightSymbol.TypeKind == TypeKind.Enum;

            // check if the value type of the enums are the same.
            var isSameValueType = isEnums && SymbolEqualityComparer.Default.Equals(
                leftSymbol.EnumUnderlyingType,
                rightSymbol.EnumUnderlyingType);

            if (!isSameValueType)
            {
                return false;
            }

            // check if they have the same values.

            var leftEnumValues = leftSymbol.GetMembers().OfType<IFieldSymbol>().Select(x => x.Name).ToList();
            var rightEnumValues = rightSymbol.GetMembers().OfType<IFieldSymbol>().Select(x => x.Name).ToList();

            var hasSameValues = leftEnumValues.Count == rightEnumValues.Count &&
                                leftEnumValues.All(rightEnumValues.Contains) &&
                                rightEnumValues.All(leftEnumValues.Contains);

            return hasSameValues;
        }
    }
}
