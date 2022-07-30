using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

/// <summary>
/// <para>
///     Options for define the target construtor.
/// </para>
/// </summary>
public class ConstructorOptions : OptionsBase
{
    /// <summary>
    /// <para>
    ///     A value for select the constructor.
    /// </para>
    /// <para>
    ///     Defines a number of parameters for the constructor.
    /// </para>
    /// </summary>
    public int? NumberOfParameters { get; internal set; }

    /// <summary>
    /// <para>
    ///     A value for select the constructor.
    /// </para>
    /// <para>
    ///     Defines the parameter types for the constructor.
    /// </para>
    /// </summary>
    public Type[]? ParameterTypes { get; internal set; }
}