﻿using RoyalCode.SmartMapper.Core.Exceptions;
using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters.Resolutions;
using RoyalCode.SmartMapper.Core.Configurations;
using RoyalCode.SmartMapper.Adapters.Resolutions.Contexts;

namespace RoyalCode.SmartMapper.Core.Resolutions;

/// <summary>
/// <para>
///     A factory for creating resolutions from options for adapters, selectors, and mappers.
/// </para>
/// </summary>
public sealed class ResolutionFactory
{
    private readonly MapperConfigurations configurations;

    /// <summary>
    /// Create a new instance of <see cref="ResolutionFactory"/>.
    /// </summary>
    /// <param name="configurations">The configurations for the mapper.</param>
    public ResolutionFactory(MapperConfigurations configurations)
    {
        this.configurations = configurations;
    }

    /// <summary>
    /// Create an adapter resolution from the given adapter options.
    /// </summary>
    /// <param name="adapterOptions">The options for the adapter.</param>
    /// <returns>
    ///     A resolution for the adapter.
    /// </returns>
    /// <exception cref="ResolutionException">
    ///     When the resolution could not be created.
    /// </exception>
    public AdapterResolution CreateAdapterResolution(AdapterOptions adapterOptions)
    {
        var context = AdapterContext.Create(adapterOptions);

        var resolution = context.CreateResolution(configurations);

        if (!resolution.Resolved)
            throw resolution.Failure.CreateException();

        return resolution;
    }
}

