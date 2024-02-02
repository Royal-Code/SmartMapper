using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Infrastructure.Selectors.Resolutions;

namespace RoyalCode.SmartMapper.Infrastructure.Selectors.Resolvers;

public class SelectorResolver
{
    public SelectorResolution Resolve(SelectorContext context)
    {
        throw new NotImplementedException();
    }
    
    public bool TryResolve(SelectorContext context, [NotNullWhen(true)] out SelectorResolution? resolution)
    {
        // deve ser executado uma série de try para cada etapa ?
        
        throw new NotImplementedException();
    }
}