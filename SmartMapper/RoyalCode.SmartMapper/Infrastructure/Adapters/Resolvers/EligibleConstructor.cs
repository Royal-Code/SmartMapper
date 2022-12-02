using RoyalCode.SmartMapper.Infrastructure.Adapters.Options;
using RoyalCode.SmartMapper.Infrastructure.Adapters.Resolutions;
using RoyalCode.SmartMapper.Infrastructure.Core;
using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

/// <summary>
/// <para>
///     Represents a target type constructor that is eligible for mapping.
/// </para>
/// </summary>
public class EligibleConstructor : ResolvableMember<ConstructorInfo, ConstructorOptions>
{
    private ConstrutorResolution? resolution;

    /// <summary>
    /// <para>
    ///     Creates a new instance of <see cref="EligibleConstructor"/>.
    /// </para>
    /// </summary>
    /// <param name="memberInfo">The constructor information.</param>
    /// <param name="options">The options.</param>
    /// <param name="preConfigured">Indicates whether the options were pre-configured.</param>
    public EligibleConstructor(
        ConstructorInfo memberInfo,
        ConstructorOptions options, 
        bool preConfigured) 
        : base(memberInfo, options, preConfigured)
    { }

    /// <summary>
    /// Not supported.
    /// </summary>
    /// <returns>Not supported.</returns>
    /// <exception cref="NotSupportedException">Not supported.</exception>
    public override string GetMemberName() => throw new NotSupportedException();

    /// <summary>
    /// <para>
    ///     Gets the resolution of the constructor.
    /// </para>
    /// </summary>
    public ConstrutorResolution Resolution
    {
        get => resolution ?? new ConstrutorResolution(MemberInfo, new string[] { "The resolution is not defined." });
        set => resolution = value;
    }
}