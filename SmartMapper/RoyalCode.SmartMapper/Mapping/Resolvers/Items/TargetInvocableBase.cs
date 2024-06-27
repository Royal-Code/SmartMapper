namespace RoyalCode.SmartMapper.Mapping.Resolvers.Items;

/// <summary>
/// A container for target members that have parameters, such as constructors and methods.
/// </summary>
public abstract class TargetInvocableBase : TargetBase
{
    /// <summary>
    /// Base constructor.
    /// </summary>
    /// <param name="parameters">The parameters.</param>
    protected TargetInvocableBase(IReadOnlyCollection<TargetParameter> parameters)
    {
        Parameters = parameters;
    }

    /// <summary>
    /// The member (constructor or method) parameters.
    /// </summary>
    public IReadOnlyCollection<TargetParameter> Parameters { get; }
}
