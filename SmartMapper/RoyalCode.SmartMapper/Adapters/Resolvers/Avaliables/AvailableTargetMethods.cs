namespace RoyalCode.SmartMapper.Adapters.Resolvers.Avaliables;

/// <summary>
/// Contains all <see cref="AvailableMethod"/> that are available to be mapped.
/// </summary>
public class AvailableTargetMethods
{
    private ICollection<AvailableMethod> availableMethod;
    

    public AvailableTargetMethods(Type targetType)
    {
        availableMethod = AvailableMethod.Create(targetType);
    }


}
