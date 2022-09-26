using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartMapper.Infrastructure.Core;

public abstract class ResolutionBase
{
    [MemberNotNullWhen(false, nameof(FailureMessages))]
    public bool Resolved { get; init; }
    
    public IEnumerable<string>? FailureMessages { get; init; }
}