using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Configurations;
using RoyalCode.SmartMapper.Infrastructure.Resolvers.Invocables;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.Constructors;

/// <summary>
/// <para>
///     Implementation of <see cref="IParameterRequest"/> for constructor parameters.
/// </para>
/// </summary>
public class ConstrutorParameterRequest : IParameterRequest
{
    private readonly ConstructorContext constructorContext;

    /// <summary>
    /// Creates a new instance of <see cref="ConstrutorParameterRequest"/>.
    /// </summary>
    /// <param name="constructorContext">The context of the constructor resolution.</param>
    /// <param name="parameter">The target parameter.</param>
    public ConstrutorParameterRequest(ConstructorContext constructorContext, TargetParameter parameter)
    {
        this.constructorContext = constructorContext;
        Parameter = parameter;
    }

    /// <inheritdoc/>
    public TargetParameter Parameter { get; }

    /// <inheritdoc/>
    public ResolutionConfiguration Configuration => constructorContext.Configuration;

    /// <inheritdoc/>
    public bool TryGetAvailableSourceProperty(
        PropertyInfo propertyInfo,
        [NotNullWhen(true)] out AvailableSourceProperty? property)
    {
        property = constructorContext.AvailableSourceProperties
            .Where(p => !p.Resolved && p.SourceProperty.MemberInfo == propertyInfo)
            .FirstOrDefault();
        return property is not null;
    }

    /// <inheritdoc/>
    public bool TryGetAvailableSourceProperty(
        string propertyName, 
        [NotNullWhen(true)] out AvailableSourceProperty? property)
    {
        property = constructorContext.AvailableSourceProperties
            .Where(p => !p.Resolved && p.SourceProperty.MemberInfo.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase))
            .FirstOrDefault();
        return property is not null;
    }

    /// <inheritdoc/>
    public bool TryGetParameterOptionsByName(
        string name, 
        [NotNullWhen(true)] out ToParameterOptionsBase? options)
    {
        var found = constructorContext.ConstructorOptions.TryGetParameterOptions(name, out var ctorParamOptions);
        options = ctorParamOptions;
        return found;
    }
}
