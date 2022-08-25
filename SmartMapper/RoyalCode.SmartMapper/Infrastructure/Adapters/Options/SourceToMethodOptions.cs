
namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Options;

/// <summary>
/// Options containing configuration for the mapping of a source type to a destination type method.
/// </summary>
public class SourceToMethodOptions
{
    private readonly AdapterOptions adapterOptions;
    private SourceToMethodStrategy strategy;
    private ICollection<ToMethodParameterOptions>? parametersSequence;

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
    public SourceToMethodStrategy Strategy
    {
        get => strategy;
        internal set
        {
            if (strategy != SourceToMethodStrategy.Default)
                throw new InvalidOperationException("The method has been set up before");
            strategy = value;
        }
    }

     /// <summary>
     /// Adds the property to parameter options to the selected property to parameter sequence.
     /// </summary>
     /// <param name="options">The property to parameter options.</param>
     public void AddParameterSequence(ToMethodParameterOptions options)
     {
         if (strategy != SourceToMethodStrategy.SelectedParameters)
             throw new InvalidOperationException(
                 "Invalid strategy, this method requires the strategy 'SelectedParameters' and it has not been assigned.");
         
         parametersSequence ??= new List<ToMethodParameterOptions>();
         parametersSequence.Add(options);
     }
     
     /// <summary>
     /// <para>
     ///     Gets the selected property to parameter sequence.
     /// </para>
     /// </summary>
     /// <returns>The selected property to parameter sequence.</returns>
     public IEnumerable<ToMethodParameterOptions> GetAllParameterSequence()
     {
         return parametersSequence ?? Enumerable.Empty<ToMethodParameterOptions>();
     }
}