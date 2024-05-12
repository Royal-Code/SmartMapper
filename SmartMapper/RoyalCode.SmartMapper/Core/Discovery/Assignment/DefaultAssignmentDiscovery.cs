using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Numerics;
using RoyalCode.SmartMapper.Core.Extensions;
using RoyalCode.SmartMapper.Core.Resolutions;

namespace RoyalCode.SmartMapper.Core.Discovery.Assignment;

/// <summary>
/// Default implementation of <see cref="IAssignmentDiscovery"/>.
/// </summary>
public sealed class DefaultAssignmentDiscovery : IAssignmentDiscovery
{
    private readonly IStrategyDiscovery[] strategyDiscoveries =
    [
        new DirectStrategyDiscovery(),
        new EnumCastStrategyDiscovery(),
        new NumberCastStrategyDiscovery(),
        new NullableConverterStrategyDiscovery()
    ];
    
    /// <inheritdoc />
    public AssignmentDiscoveryResult Discover(AssignmentDiscoveryRequest request)
    {
        // ReSharper disable once ForCanBeConvertedToForeach
        // ReSharper disable once SuggestVarOrType_BuiltInTypes
        for (int i = 0; i < strategyDiscoveries.Length; i++)
            if (strategyDiscoveries[i].TryDiscover(request, out var resolution))
            {
                return new AssignmentDiscoveryResult
                {
                    IsResolved = true,
                    Resolution = resolution
                };
            }
        
        return new AssignmentDiscoveryResult();
    }

    private interface IStrategyDiscovery
    {
        bool TryDiscover(
            AssignmentDiscoveryRequest request,
            [NotNullWhen(true)] out AssignmentStrategyResolution? resolution);
    }

    private sealed class DirectStrategyDiscovery : IStrategyDiscovery
    {
        public bool TryDiscover(
            AssignmentDiscoveryRequest request,
            [NotNullWhen(true)] out AssignmentStrategyResolution? resolution)
        {
            if (request.TargetType.IsAssignableFrom(request.SourceType))
            {
                resolution = new AssignmentStrategyResolution(ValueAssignmentResolution.Direct);
                return true;
            }

            resolution = null;
            return false;
        }
    }

    private sealed class EnumCastStrategyDiscovery : IStrategyDiscovery
    {
        public bool TryDiscover(AssignmentDiscoveryRequest request, 
            [NotNullWhen(true)] out AssignmentStrategyResolution? resolution)
        {
            resolution = null;
            
            // check if both types are enum
            if (!request.SourceType.IsEnum || !request.TargetType.IsEnum)
                return false;
            
            // check if both types have same members or target has more members
            var sourceMembers = request.SourceType.GetEnumNames();
            var targetMembers = request.TargetType.GetEnumNames();
            
            if (sourceMembers.Length > targetMembers.Length)
                return false;
            
            for (var i = 0; i < sourceMembers.Length; i++)
                if (sourceMembers[i] != targetMembers[i])
                    return false;
            
            // check if the underlying type is the same
            if (request.SourceType.GetEnumUnderlyingType() != request.TargetType.GetEnumUnderlyingType())
                return false;
            
            resolution = new AssignmentStrategyResolution(ValueAssignmentResolution.Cast);
            return true;
        }
    }
    
    private sealed class NumberCastStrategyDiscovery : IStrategyDiscovery
    {
        /// <summary>
        /// Cast if source is a number type and target is a number type.
        /// It can cast from smaller to bigger types.
        /// like int to long, float to double, etc.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="resolution"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool TryDiscover(AssignmentDiscoveryRequest request,
            [NotNullWhen(true)] out AssignmentStrategyResolution? resolution)
        {
            resolution = null;
            
#if NET6
            // check if both types are primitive
            if (!request.SourceType.IsPrimitive || !request.TargetType.IsPrimitive)
                return false;
#endif            

#if NET7_0_OR_GREATER
            // check if are both number types (if implemented INumber<TSelf>)
            if (!request.SourceType.ImplementsGenericType(typeof(INumber<>))
                || !request.TargetType.ImplementsGenericType(typeof(INumber<>)))
                return false;
#endif
            
            // check if the target type is bigger than the source type
            var sourceSize = System.Runtime.InteropServices.Marshal.SizeOf(request.SourceType);
            var targetSize = System.Runtime.InteropServices.Marshal.SizeOf(request.TargetType);
            
            if (sourceSize >= targetSize)
                return false;
            
            resolution = new AssignmentStrategyResolution(ValueAssignmentResolution.Cast);
            return true;
        }
    }
    
    private sealed class NullableConverterStrategyDiscovery : IStrategyDiscovery
    {
        public bool TryDiscover(AssignmentDiscoveryRequest request,
            [NotNullWhen(true)] out AssignmentStrategyResolution? resolution)
        {
            resolution = null;
            
            // check if the source type is nullable and the target is the underlying type
            if (!request.SourceType.IsGenericType 
                || request.SourceType.GetGenericTypeDefinition() != typeof(Nullable<>))
                return false;
            
            var underlyingType = request.SourceType.GetGenericArguments()[0];
            if (underlyingType != request.TargetType)
                return false;
            
            resolution = new AssignmentStrategyResolution(ValueAssignmentResolution.Convert,
                new ValueAssignmentConverter(
                    request.SourceType,
                    request.TargetType,
                    BuildExpression(request.TargetType)));
            return true;
        }

        private Expression BuildExpression(Type type)
        {
            // Example: Expression<Func<int?, int>> expression = x => x.HasValue ? x.Value : default(int)
            
            var nullableType = typeof(Nullable<>).MakeGenericType(type);
            var parameter = Expression.Parameter(nullableType, "x");
            var hasValue = Expression.Property(parameter, "HasValue");
            var value = Expression.Property(parameter, "Value");
            var defaultValue = Expression.Default(type);
            
            var condition = Expression.Condition(hasValue, value, defaultValue);
            return Expression.Lambda(condition, parameter);
        }
    }
    
}