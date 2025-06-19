using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace RoyalCode.SmartSelector.Generators.Models.Descriptors;

internal sealed class DirectAssignDescriptorResolver : IAssignDescriptorResolver
{
    public bool TryCreateAssignDescriptor(
        TypeDescriptor leftType,
        TypeDescriptor rightType,
        SemanticModel model,
        out AssignDescriptor? descriptor)
    {
        if (!CanBeAssigned(leftType, rightType, model))
        {
            descriptor = null;
            return false;
        }

        descriptor = new AssignDescriptor
        {
            AssignType = AssignType.Direct,
        };
        return true;
    }

    private bool CanBeAssigned(TypeDescriptor leftType, TypeDescriptor rightType, SemanticModel model)
    {
        if (leftType.Equals(rightType))
            return true;

        if (leftType.Symbol is null || rightType.Symbol is null)
            return false;

        var conversion = model.Compilation.ClassifyConversion(
                    rightType.Symbol!,
                    leftType.Symbol!);

        return conversion.Exists && conversion.IsImplicit;
    }
}