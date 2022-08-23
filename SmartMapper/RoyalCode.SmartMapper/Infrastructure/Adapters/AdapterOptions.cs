// using System.Diagnostics.CodeAnalysis;
// using System.Reflection;
// using RoyalCode.SmartMapper.Infrastructure.Core;
//
// namespace RoyalCode.SmartMapper.Infrastructure.Adapters;
//
// /// <summary>
// /// <para>
// ///     Contains all the options for a single mapping between a source and target type.
// /// </para>
// /// </summary>
// public class AdapterOptions : SourceOptionsBase
// {
//     private ICollection<SourceToMethodOptions>? sourceToMethodOptions;
//     private ConstructorOptions? constructorOptions;
//
//     public AdapterOptions(Type sourceType, Type targetType)
//         : base (sourceType)
//     {
//         TargetType = targetType;
//     }
//
//     public Type TargetType { get; }
//
//     /// <summary>
//     /// <para>
//     ///     Gets the options for mapping a source type to a method.
//     /// </para>
//     /// </summary>
//     /// <returns>
//     ///     All options for mapping a source type to a method or an empty collection if no options have been set.
//     /// </returns>
//     public IEnumerable<SourceToMethodOptions> GetSourceToMethodOptions()
//     {
//         return sourceToMethodOptions ?? Enumerable.Empty<SourceToMethodOptions>();
//     }
//
//     /// <summary>
//     /// <para>
//     ///     Adds an option for mapping a source type to a method.
//     /// </para>
//     /// </summary>
//     /// <param name="options">The options for mapping a source type to a method.</param>
//     public void AddToMethod(SourceToMethodOptions options)
//     {
//         sourceToMethodOptions ??= new List<SourceToMethodOptions>();
//         sourceToMethodOptions.Add(options);
//     }
//
//     /// <summary>
//     /// <para>
//     ///     Gets the options for the constructor of the target type.
//     /// </para>
//     /// </summary>
//     /// <returns>
//     ///     The options for the constructor of the target type.
//     /// </returns>
//     public ConstructorOptions GetConstructorOptions()
//     {
//         constructorOptions ??= new ConstructorOptions(TargetType);
//         return constructorOptions;
//     }
// }