namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

public record ConstructorRequest(
    ActivationContext ActivationContext,
    EligibleConstructor Constructor);