
using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Resolvers.Avaliables;
using RoyalCode.SmartMapper.Core.Configurations;

namespace RoyalCode.SmartMapper.Adapters.Resolutions.Contexts;

internal sealed class SourceToMethodContext
{
    public static SourceToMethodContext Create(
        SourceToMethodOptions Options,
        AvailableTargetMethods availableTargetMethods)
    {
        return new SourceToMethodContext()
        {
            Options = Options,
            AvailableMethods = availableTargetMethods
        };
    }

    public SourceToMethodOptions Options { get; private init; }

    public AvailableTargetMethods AvailableMethods { get; private init; }

    public SourceToMethodResolution CreateResolution(MapperConfigurations configurations)
    {
        // event: resolution started. here the interceptor can be called in future versions


        var availableMethod = AvailableMethods.ListAvailableMethods();

        if (Options.MethodOptions.Method is not null)
        {
            availableMethod = availableMethod.Where(a => a.Info == Options.MethodOptions.Method);
        }
        else if (Options.MethodOptions.MethodName is not null)
        {
            availableMethod = availableMethod.Where(a => a.Info.Name == Options.MethodOptions.MethodName);
        }
        
        if (Options.Strategy == SourceToMethodStrategy.SelectedParameters)
        {
            var sourceParameters = Options.GetAllParameterSequence();

            availableMethod = availableMethod.Where(a => a.Info.GetParameters().Length == Options.CountParameterSequence());
        }


        // 1. resolve the method, resolving the parameters and the method itself.
        // 1.1 if all parameters are resolved, generate the resolution.
        // 1.2 if one parameter fails, generate a failure.

        throw new NotImplementedException();
    }
}
