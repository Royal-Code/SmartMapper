using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

public class AssignmentStrategyOptions : OptionsBase
{ 
    public AssignmentStrategyOptions()
    {
        Strategy = ValueAssignmentStrategy.Undefined;
    }
    
    public ValueAssignmentStrategy Strategy { get; internal set; }
}