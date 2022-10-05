using RoyalCode.SmartMapper.Infrastructure.Core;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

public class SourcePropertyResolution
{
    public ResolutionBase? Resolution { get; private set; }
    
    public bool Resolved { get; private set; }

    public void ResolvedBy(ResolutionBase resolution)
    {
        Resolution = resolution;
        Resolved = true;
    }
}