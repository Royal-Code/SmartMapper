using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

public record ConstructorContext(
    AdapterContext ResolutionContext,
    ConstructorInfo Constructor);
