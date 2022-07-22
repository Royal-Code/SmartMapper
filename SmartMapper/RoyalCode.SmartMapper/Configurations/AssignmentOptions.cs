using RoyalCode.SmartMapper.Infrastructure.Options;
using RoyalCode.SmartMapper.Resolvers;
using RoyalCode.SmartMapper.Resolvers.Assigners;

namespace RoyalCode.SmartMapper.Configurations;

/// <summary>
/// <para>
///     Properties that define how the values will be assigned between the source and target types.
/// </para>
/// </summary>
public class AssignmentOptions : OptionsBase
{
    public ValueAssignmentStrategy Strategy => Assigner?.Strategy ?? ValueAssignmentStrategy.Undefined;


    public IValueAssigner? Assigner { get; internal set; }
}