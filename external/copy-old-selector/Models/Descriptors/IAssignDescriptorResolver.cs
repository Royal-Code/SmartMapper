using Microsoft.CodeAnalysis;

namespace RoyalCode.SmartSelector.Generators.Models.Descriptors;

internal interface IAssignDescriptorResolver
{
    public bool TryCreateAssignDescriptor(
        TypeDescriptor leftType,
        TypeDescriptor rightType,
        SemanticModel model,
        out AssignDescriptor? descriptor);
}
