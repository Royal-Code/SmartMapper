using RoyalCode.SmartMapper.Core.Resolutions;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;
using RoyalCode.SmartMapper.Mapping.Resolvers.Items;

namespace RoyalCode.SmartMapper.Mapping.Resolutions;

public class InnerPropertiesResolution : ResolutionBase
{
    private readonly SourceProperty sourceItem;
    public AvailableInnerSourceProperties InnerProperties { get; }

    public InnerPropertiesResolution(
        SourceProperty sourceItem,
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