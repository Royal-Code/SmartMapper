using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using RoyalCode.SmartSelector.Generators.Extensions;

namespace RoyalCode.SmartSelector.Generators.Models.Descriptors;

internal class EnumerableAssignDescriptorResolver : IAssignDescriptorResolver
{
    public bool TryCreateAssignDescriptor(
        TypeDescriptor leftType,
        TypeDescriptor rightType,
        SemanticModel model,
        out AssignDescriptor? descriptor)
    {
        if (leftType.Symbol is null || rightType.Symbol is null ||
            leftType.Symbol.TryGetEnumerableGenericType(out var leftGenericSymbol) is false ||
            rightType.Symbol.TryGetEnumerableGenericType(out var rightGenericSymbol) is false)
        {
            descriptor = null;
            return false;
        }

        TypeDescriptor leftGenericType = TypeDescriptor.Create(leftGenericSymbol!);
        TypeDescriptor rightGenericType = TypeDescriptor.Create(rightGenericSymbol!);

        bool requireSelect = false;
        AssignDescriptor? genericAssignment = null;
        bool requireToList;

        // se os tipos genéricos são iguais, não é necessário fazer a conversão
        // cas contrário, é necessário fazer a conversão
        if (!leftGenericType.Equals(rightGenericType))
        {
            requireSelect = true;
            genericAssignment = AssignDescriptorFactory.Create(leftGenericType, rightGenericType, model);

            // se a conversão dos tipos genéricos não for possível, não é possível fazer a conversão desta propriedade
            if (genericAssignment is null)
            {
                descriptor = null;
                return false;
            }
        }

        // se o leftType não for um IEnumerable<T>, deve ser assinável por List<T>.
        if (leftType.IsEnumerable)
        {
            requireToList = false;
        }
        else
        {
            requireToList = true;

            // checa se o leftType symbol pode receber um List<T>
            var listType = model.Compilation.GetTypeByMetadataName("System.Collections.Generic.List`1");
            if (listType is null)
            {
                descriptor = null;
                return false;
            }

            var conversion = model.Compilation.ClassifyConversion(
                listType,
                leftType.Symbol);

            if (!conversion.Exists)
            {
                descriptor = null;
                return false;
            }
        }

        descriptor = new AssignDescriptor()
            {
                AssignType = requireSelect ? AssignType.Select : AssignType.Direct,
                RequireToList = requireToList,
                InnerSelection = genericAssignment?.InnerSelection,
            };
        return true;
    }
}
