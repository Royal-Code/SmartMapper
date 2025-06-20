using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoyalCode.SmartCommands.Generators.Models.Descriptors;

public sealed class EditTypeDescriptor : IEquatable<EditTypeDescriptor>
{
    #region Factory

    public static EditTypeDescriptor Create(AttributeSyntax editEntityAttr, SemanticModel semanticModel)
    {
        var syntax = (GenericNameSyntax)editEntityAttr.Name;

        // extrai os tipos para editar entidade
        var editEntitySyntaxType = syntax.TypeArgumentList.Arguments[0];
        var editIdSyntaxType = syntax.TypeArgumentList.Arguments[1];

        return new EditTypeDescriptor(
            TypeDescriptor.Create(editEntitySyntaxType, semanticModel),
            TypeDescriptor.Create(editIdSyntaxType, semanticModel));
    }

    #endregion

    public EditTypeDescriptor(TypeDescriptor entityType, TypeDescriptor idType)
    {
        EntityType = entityType;
        IdType = idType;
    }

    public TypeDescriptor EntityType { get; }

    public TypeDescriptor IdType { get; }

#nullable disable

    public ParameterDescriptor Parameter { get; internal set; }

#nullable enable

    public bool Equals(EditTypeDescriptor? other)
    {
        return other is not null &&
            Equals(EntityType, other.EntityType) &&
            Equals(IdType, other.IdType) &&
            Equals(Parameter, other.Parameter);
    }

    public override bool Equals(object? obj)
    {
        return obj is EditTypeDescriptor descriptor && Equals(descriptor);
    }

    public override int GetHashCode()
    {
        int hashCode = 1009432853;
        hashCode = hashCode * -1521134295 + EntityType.GetHashCode();
        hashCode = hashCode * -1521134295 + IdType.GetHashCode();
        hashCode = hashCode * -1521134295 + Parameter?.GetHashCode() ?? 0;
        return hashCode;
    }
}
