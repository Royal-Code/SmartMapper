using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

/// <summary>
/// <para>
///     Options of value processor used to process the source value and convert it to the target value.
/// </para>
/// </summary>
/// <param name="SourceValueType">Type of the source value.</param>
/// <param name="TargetValueType">Type of the target value.</param>
/// <param name="ServiceType">Type of the service that is used to process the value.</param>
/// <param name="processor">
/// <para>
///     Function expression that is used to process the value.
/// </para>
/// <para>
///     It is a lambda expression that takes two parameters: the service and the source value,
///     and returns the target value.
/// </para>
/// </param>
public record ProcessorOptions(
    Type SourceValueType,
    Type TargetValueType,
    Type ServiceType,
    Expression processor);