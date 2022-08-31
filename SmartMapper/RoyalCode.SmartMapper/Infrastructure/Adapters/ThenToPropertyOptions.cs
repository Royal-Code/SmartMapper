// using System.Reflection;
//
// namespace RoyalCode.SmartMapper.Infrastructure.Adapters;
//
// public class ThenToPropertyOptions : ToPropertyTargetRelatedOptionsBase
// {
//     public ThenToPropertyOptions(TargetRelatedOptionsBase parentOptions, PropertyInfo propertyInfo)
//     {
//         ParentOptions = parentOptions;
//         PropertyInfo = propertyInfo;
//         PropertyRelated = parentOptions.PropertyRelated 
//             ?? throw new ArgumentException("The parent options must have a property related.", nameof(parentOptions));
//     }
//     
//     public TargetRelatedOptionsBase ParentOptions { get; }
//     
//     public PropertyInfo PropertyInfo { get; }
// }