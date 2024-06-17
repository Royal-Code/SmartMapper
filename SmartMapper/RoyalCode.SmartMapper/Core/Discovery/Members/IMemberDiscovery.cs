using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Core.Extensions;
using RoyalCode.SmartMapper.Mapping.Resolvers.Availables;

namespace RoyalCode.SmartMapper.Core.Discovery.Members;

#pragma warning disable CS1591 // XML doc.

public class MemberResolution
{

}

public abstract class MemberResolver
{
    public abstract MemberResolution CreateResolution(MapperConfigurations configurations);
}

public sealed class CallMethodResolver : MemberResolver
{
    public static CallMethodResolver Create(PropertyInfo sourceProperty, IEnumerable<AvailableMethod> availableMethods)
    {
        return new(sourceProperty, availableMethods);
    }

    private CallMethodResolver(PropertyInfo sourceProperty, IEnumerable<AvailableMethod> availableMethods)
    {
        SourceProperty = sourceProperty;
        AvailableMethods = availableMethods;
    }

    public PropertyInfo SourceProperty { get; }

    public IEnumerable<AvailableMethod> AvailableMethods { get; }

    public override MemberResolution CreateResolution(MapperConfigurations configurations)
    {
        throw new NotImplementedException();
    }
}


public interface INameHandler
{
    public bool Handle(MemberDiscoveryRequest request, string[] names, int index, [NotNullWhen(true)] out MemberResolver? resolver);
}

public class DirectMethodNameHandler : INameHandler
{
    public bool Handle(MemberDiscoveryRequest request, string[] names, int index, [NotNullWhen(true)] out MemberResolver? resolver)
    {
        resolver = null;

        if (index >= names.Length)
            return false;

        var name = index is 0 ? request.SourceProperty.Name : names.Concat(index);
        var methods = request.TargetMethods.ListAvailableMethods()
            .Where(m => m.Info.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (methods.Count is 0) 
            return false;

        resolver = CallMethodResolver.Create(request.SourceProperty, methods);
        return true;
    }
}

public class PropertyNameHandler : INameHandler
{
    public bool Handle(MemberDiscoveryRequest request, string[] names, int index, [NotNullWhen(true)] out MemberResolver? resolver)
    {
        resolver = null;

        if (index >= names.Length)
            return false;

        for (var end = names.Length - 1; end >= index; end--)
        {
            var name = names.Concat(index, end);

            
        }

        throw new NotImplementedException();
    }
}

public sealed class MemberDiscovery : IMemberDiscovery
{
    private readonly INameHandler[] nameHandlers = [];

    public MemberDiscoveryResult Discover(MemberDiscoveryRequest request)
    {
        var names = request.SourceProperty.Name.SplitUpperCase();
        if (names is null)
            return new();

        foreach(var handler in nameHandlers)
        {
            if (handler.Handle(request, names, 0, out var resolver))
                return new();
        }

        throw new NotImplementedException();
    }

    
}

public interface IMemberDiscovery
{
    MemberDiscoveryResult Discover(MemberDiscoveryRequest request);
}

public readonly struct MemberDiscoveryRequest(
    MapperConfigurations configurations,
    PropertyInfo sourceProperty,
    AvailableTargetMethods targetMethods,
    AvailableTargetProperties targetProperties)
{
    public MapperConfigurations Configurations { get; } = configurations;
    public PropertyInfo SourceProperty { get; } = sourceProperty;
    public AvailableTargetMethods TargetMethods { get; } = targetMethods;
    public AvailableTargetProperties TargetProperties { get; } = targetProperties;
}

public readonly struct MemberDiscoveryResult
{

    public AvailableMethod? Method { get; }

    public AvailableProperty? Property { get; }
}

