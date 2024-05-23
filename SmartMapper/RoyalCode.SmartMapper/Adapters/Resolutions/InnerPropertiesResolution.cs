using RoyalCode.SmartMapper.Adapters.Resolvers.Available;
using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Adapters.Resolutions;

public class InnerPropertiesResolution : ResolutionBase
{
    private readonly SourceItem sourceItem;
    public AvailableInnerSourceProperties InnerProperties { get; }

    public InnerPropertiesResolution(
        SourceItem sourceItem,
        AvailableInnerSourceProperties innerProperties)
    {
        this.sourceItem = sourceItem;
        InnerProperties = innerProperties;
    }

    /// <inheritdoc />
    public override void Completed()
    {
        sourceItem.ResolvedBy(this);
    }
}