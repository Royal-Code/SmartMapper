using RoyalCode.SmartMapper.Resolvers;

namespace RoyalCode.SmartMapper.Configurations;

/// <summary>
/// <para>
///     Properties that define how the values will be assigned between the source and target types.
/// </para>
/// </summary>
public class AssignmentOptions : OptionsBase
{
    public ValueAssignmentStrategy Strategy { get; internal set; }
}