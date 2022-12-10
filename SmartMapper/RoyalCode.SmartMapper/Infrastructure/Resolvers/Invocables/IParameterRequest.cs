
using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.Invocables;

public interface IParameterRequest
{
    bool TryGetParameterOptionsByName(string name, [NotNullWhen(true)] out ToParameterOptionsBase? options);

    bool TryGetAvailableSourceProperty(PropertyInfo propertyInfo,
        [NotNullWhen(true)] out AvailableSourceProperty? property);
        
    TargetParameter Parameter { get; }

    /// <summary>
    /// <para>
    ///     The configuration used to resolve the parameter.
    /// </para>
    /// </summary>
    ResolutionConfiguration Configuration { get; }
}
