
using RoyalCode.SmartMapper.Exceptions;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

/// <summary>
/// Options containing configuration for the mapping of a source type to a destination type method.
/// </summary>
public class SourceToMethodOptions
{
    private readonly AdapterOptions adapterOptions;

    /// <summary>
    /// Creates a new <see cref="SourceToMethodOptions"/> instance with the specified adapter options
    /// and the method options.
    /// </summary>
    /// <param name="adapterOptions">The adapter options.</param>
    /// <param name="methodOptions">The method options.</param>
    public SourceToMethodOptions(AdapterOptions adapterOptions, MethodOptions methodOptions)
    {
        this.adapterOptions = adapterOptions;
        MethodOptions = methodOptions;
    }
    
    /// <summary>
    /// The method options.
    /// </summary>
    public MethodOptions MethodOptions { get; }
    
    /// <summary>
    /// The strategy used to mapping the properties of the source to the target method.
    /// </summary>
    public SourceToMethodStrategy Strategy { get; internal set; }

    
}