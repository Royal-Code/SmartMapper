﻿
using RoyalCode.SmartMapper.Infrastructure.Configurations;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters.Resolvers;

/// <summary>
/// <para>
///     Request to resolve a activator for the destination type.
/// </para>
/// </summary>
/// <param name="AdapterContext">The context of the adapter resolution process.</param>
public record ActivationRequest(AdapterContext AdapterContext)
{
    /// <summary>
    /// <para>
    ///     The configuration used by resolvers.
    /// </para>
    /// </summary>
    public ResolutionConfiguration Configuration => AdapterContext.Configuration;
}
