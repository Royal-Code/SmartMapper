
using RoyalCode.SmartMapper.Infrastructure.Adapters;

namespace RoyalCode.SmartMapper.Infrastructure.Core;

public abstract class WithAssignmentOptionsBase : OptionsBase
{
    internal PropertyOptions? PropertyRelated { get; set; }
    
    internal void Reset()
    {
        PropertyRelated?.ResetMapping();
        PropertyRelated = null;
    }
}