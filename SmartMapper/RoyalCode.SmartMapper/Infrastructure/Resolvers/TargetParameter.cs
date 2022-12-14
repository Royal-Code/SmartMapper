using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Core;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Parameters;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers;

public class TargetParameter : ResolvableMember<ParameterInfo>
{
    public TargetParameter(ParameterInfo parameterInfo) : base(parameterInfo) { }
   
    public ParameterResolution? Resolution { get; private set; }

    public bool Unresolved => Resolution is null;

    public bool HasFailure => !Resolution?.Resolved ?? false;

    public void ResolvedBy(ParameterResolution resolution) => Resolution = resolution;
}