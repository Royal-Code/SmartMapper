using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

public record ConstructorContext(
    AdapterResolutionContext ResolutionContext,
    ConstructorInfo Constructor);