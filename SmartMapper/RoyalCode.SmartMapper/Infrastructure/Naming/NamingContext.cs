using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using RoyalCode.SmartMapper.Infrastructure.AssignmentStrategies;
using RoyalCode.SmartMapper.Infrastructure.Configurations;

namespace RoyalCode.SmartMapper.Infrastructure.Naming;

/// <summary>
/// A context to validate a NameHandler and set the <see cref="AssignmentResolution"/> of match.
/// </summary>
public class NamingContext
{
    /// <summary>
    /// Creates a new context.
    /// </summary>
    /// <param name="sourceProperty">The source property.</param>
    /// <param name="targetType">The target type.</param>
    /// <param name="configuration">The configurations.</param>
    public NamingContext(PropertyInfo sourceProperty, Type targetType, ResolutionConfiguration configuration)
    {
        SourceProperty = sourceProperty;
        TargetType = targetType;
        Configuration = configuration;
    }

    /// <summary>
    /// The source property.
    /// </summary>
    public PropertyInfo SourceProperty { get; }
    
    /// <summary>
    /// The target type.
    /// </summary>
    public Type TargetType { get; }
    
    /// <summary>
    /// The configurations.
    /// </summary>
    public ResolutionConfiguration Configuration { get; }

    /// <summary>
    /// Determines whether the source and destination types are compatible.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Resolution))]
    public bool IsValid { get; private set; }
    
    /// <summary>
    /// The <see cref="AssignmentResolution"/> for when source and destination types are matched.
    /// </summary>
    public AssignmentResolution? Resolution { get; private set; }

    /// <summary>
    /// It determines that the context is valid by informing the <see cref="AssignmentResolution"/>.
    /// </summary>
    /// <param name="resolution">The <see cref="AssignmentResolution"/>.</param>
    public void Validated(AssignmentResolution resolution)
    {
        IsValid = true;
        Resolution = resolution;
    }
}