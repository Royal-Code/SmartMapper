using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

public class TargetParameter
{
    public TargetParameter(ParameterInfo parameterInfo)
    {
        ParameterInfo = parameterInfo;
    }

    public ParameterInfo ParameterInfo { get; }

    public ParameterResolution? Resolution { get; private set; }

    public bool Unresolved => Resolution is null;

    public bool HasFailure => !Resolution?.Resolved ?? false;
    
    public void ResolvedBy(ParameterResolution resolution) => Resolution = resolution;
}