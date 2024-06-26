﻿using RoyalCode.SmartMapper.Adapters.Discovery.PropertyToMethods;
using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Options.Resolutions;
using RoyalCode.SmartMapper.Adapters.Resolvers.Available;
using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Core.Discovery.Assignment;
using RoyalCode.SmartMapper.Core.Resolutions;
using System.Diagnostics.CodeAnalysis;
using RoyalCode.SmartMapper.Adapters.Discovery.Properties;

namespace RoyalCode.SmartMapper.Adapters.Resolutions.Contexts;

internal class PropertyContext
{
    public static PropertyContext Create(
        AdapterContext adapterContext,
        AvailableSourceProperty property)
    {
        return new PropertyContext()
        {
            AdapterContext = adapterContext,
            Property = property
        };
    }
    
    private PropertyContext() { }
    
    public AdapterContext AdapterContext { get; private init; }
    
    public AvailableSourceProperty Property { get; private init; }

    public PropertyResolution CreateResolution(MapperConfigurations configurations)
    {
        // event: resolution started. here the interceptor can be called in future versions

        var availableProperties = AdapterContext.AvailableTargetProperties;
        
        // 2.1 property can have a resolution option.
        if (Property.SourceItem.Options.ResolutionOptions is ToPropertyResolutionOptions resolutionOptions
            && availableProperties.TryFindProperty(resolutionOptions.TargetProperty.TargetProperty, out var available))
        {
            // 2.1.1 check the resolution options e try to resolve the property.
            if (resolutionOptions.Strategy == ToPropertyResolutionStrategy.SetValue)
            {
                if (!ResolveStrategy(configurations, resolutionOptions.AssignmentStrategy, Property, available,
                    out var assignmentStrategyResolution, out var resolutionFailure))
                {
                    return new PropertyResolution(resolutionFailure);
                }

                var resolution = new PropertyResolution(Property, available.Info, assignmentStrategyResolution);
                available.ResolvedBy(resolution);
                return resolution;
            }
            else
            {
                // must be called other context 
                throw new NotImplementedException();
            }
        }

        // 2.2 try to resolve the property by name.
        // 2.2.1 try map the property by name to an available target method by name.
        var discoveryPropertyToMethodResolution = configurations.Discovery.PropertyToMethod.Discover(
            new PropertyToMethodRequest(configurations, Property.SourceItem, AdapterContext.AvailableTargetMethods));
        if (discoveryPropertyToMethodResolution.IsResolved)
        {
            return discoveryPropertyToMethodResolution.Resolution;
        }

        // 2.2.2 try map the property by name to an available target property.
        var discoveryPropertyToPropertyResolution = configurations.Discovery.Property.Discover(
            new PropertyRequest(configurations, Property.SourceItem, AdapterContext.AvailableTargetProperties));
        if (discoveryPropertyToPropertyResolution.IsResolved)
        {
            return discoveryPropertyToPropertyResolution.Resolution;
        }
        
        var failure = new ResolutionFailure(
            $"Failed to resolve the source property {Property.GetPropertyPathString()}. " +
            $"The property cannot be mapped to any target property or method of the target type.");
        
        failure.AddMessages(discoveryPropertyToMethodResolution.Failure.Messages);

        return new(failure);
    }

    private bool ResolveStrategy(
        MapperConfigurations configurations,
        AssignmentStrategyOptions? options,
        AvailableSourceProperty sourceProperty,
        AvailableProperty targetProperty,
        [NotNullWhen(true)] out AssignmentStrategyResolution? resolution,
        [NotNullWhen(false)] out ResolutionFailure? failure)
    {
        resolution = null;
        failure = null;

        // check if the assignment was pre-configured
        if (options is not null && options.Resolution != ValueAssignmentResolution.Undefined)
        {
            // use the pre-configured assignment strategy.
            resolution = new AssignmentStrategyResolution(options.Resolution, options.Converter);
            return true;
        }

        // discover the assignment strategy.
        var request = new AssignmentDiscoveryRequest(
            configurations, 
            sourceProperty.SourceItem.Options.Property.PropertyType,
            targetProperty.Info.PropertyType);

        var result = configurations.Discovery.Assignment.Discover(request);

        // when the assignment strategy is resolved, return the resolution.
        if (result.IsResolved)
        {
            resolution = result.Resolution;
            return true;
        }
        
        // when the assignment strategy is not resolved, return the failure,
        // adding the messages from the resolution failure.
        failure = new(
            "The assignment strategy between the source property " +
            $"({sourceProperty.GetPropertyPathString()}) " +
            $"and the target property ({targetProperty.Info.DeclaringType!.Name}.{targetProperty.Info.Name}) could not be resolved.");

        failure.AddMessages(result.Failure.Messages);

        return false;
    }
}