using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolvers;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

namespace RoyalCode.SmartMapper.Mapping.Resolutions;

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