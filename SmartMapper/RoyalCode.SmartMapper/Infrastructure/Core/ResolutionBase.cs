namespace RoyalCode.SmartMapper.Infrastructure.Core;

public abstract class ResolutionBase
{
    public bool Resolved { get; init; }
    
    public IEnumerable<string>? FailureMessages { get; init; }
}