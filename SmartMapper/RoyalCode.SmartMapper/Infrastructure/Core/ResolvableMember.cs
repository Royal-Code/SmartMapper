
using System.Reflection;
using System.Text;

namespace RoyalCode.SmartMapper.Infrastructure.Core;

/// <summary>
/// <para>
///     Represents a member that can be resolved.
/// </para>
/// </summary>
public abstract class ResolvableMember
{
    /// <summary>
    /// <para>
    ///     Determine if the member was resolved.
    /// </para>
    /// </summary>
    public bool Resolved { get; protected set; }
    
}

/// <summary>
/// <para>
///     Represents a member that can be resolved.
/// </para>
/// </summary>
/// <typeparam name="TMember">The type of the member.</typeparam>
/// <typeparam name="TOption">The type of the options.</typeparam>
public abstract class ResolvableMember<TMember, TOption> : ResolvableMember
{
    /// <summary>
    /// Creates a new instance of <see cref="ResolvableMember{TMember, TOption}"/>.
    /// </summary>
    /// <param name="memberInfo">The member information.</param>
    /// <param name="options">The options.</param>
    /// <param name="preConfigured">Determine if the member is pre-configured.</param>
    protected ResolvableMember(TMember memberInfo, TOption options, bool preConfigured)
    {
        MemberInfo = memberInfo;
        Options = options;
        PreConfigured = preConfigured;
    }

    /// <summary>
    /// <para>
    ///     The member information, from reflection.
    /// </para>
    /// </summary>
    public TMember MemberInfo { get; }

    /// <summary>
    /// <para>
    ///     The options.
    /// </para>
    /// </summary>
    public TOption Options { get; }

    /// <summary>
    /// <para>
    ///     Determine if the member is pre-configured.
    /// </para>
    /// </summary>
    public bool PreConfigured { get; }

    /// <summary>
    /// <para>
    ///     The parent member.
    /// </para>
    /// <para>
    ///     Applyable only to properties (<see cref="PropertyInfo"/>).
    /// </para>
    /// </summary>
    public ResolvableMember<TMember, TOption>? Parent { get; private set; }

    /// <summary>
    /// <para>
    ///     The children members.
    /// </para>
    /// <para>
    ///     Applyable only to properties (<see cref="PropertyInfo"/>).
    /// </para>
    /// </summary>
    public IEnumerable<ResolvableMember<TMember, TOption>> Children { get; } = new List<ResolvableMember<TMember, TOption>>();

    /// <summary>
    /// <para>
    ///     Add a child member of the current member.
    /// </para>
    /// </summary>
    /// <param name="child">The child member.</param>
    /// <exception cref="InvalidOperationException">
    ///     The member is not a property or the child already has a parent.
    /// </exception>
    public void AddChild(ResolvableMember<TMember, TOption> child)
    {
        if (MemberInfo is not PropertyInfo)
            throw new InvalidOperationException("The member is not a property.");
        
        if (child.Parent is not null)
            throw new InvalidOperationException("The child already has a parent.");

        child.Parent = this;
        Children.Append(child);
    }

    /// <summary>
    /// <para>
    ///     Get the member name and, if there is a parent member,
    ///     concatenate the parent member name in front, separated by a dot.
    /// </para>
    /// </summary>
    /// <returns>The member name or property path when applyable.</returns>
    public string GetPropertyPathString()
    {
        var sb = new StringBuilder();
        var current = this;
        while (current is not null)
        {
            sb.Insert(0, current.GetMemberName());
            current = current.Parent;
            if (current is not null)
                sb.Insert(0, ".");
        }

        return sb.ToString();
    }

    /// <summary>
    /// <para>
    ///     Get the member name.
    /// </para>
    /// </summary>
    /// <returns>The member name.</returns>
    public abstract string GetMemberName();
}