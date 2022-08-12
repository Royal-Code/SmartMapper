using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

/// <summary>
/// <para>
///     Options of a defined converter for converting a source type to a target type.
/// </para>
/// </summary>
/// <param name="SourceValueType">The type of the source value.</param>
/// <param name="TargetValueType">The type of the target value.</param>
/// <param name="Converter">
///     The converter expression. It is a lamdba function that takes a source value and returns a target value.
/// </param>
public record ConvertOptions(
    Type SourceValueType,
    Type TargetValueType,
    Expression Converter);