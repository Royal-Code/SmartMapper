
using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Resolvers.Callers;

/// <summary>
/// <para>
///     Represents a request for resolve a method or a constructor
/// </para>
/// </summary>
public interface IInvocableRequest
{
    /// <summary>
    /// Get the parameters of a method or constructor.
    /// </summary>
    /// <returns>The parameters.</returns>
    ParameterInfo[] GetParameters();

    /// <summary>
    /// Get the all <see cref="SourceProperty"/> avaliable for map to parameters.
    /// </summary>
    /// <returns></returns>
    IEnumerable<SourceProperty> GetSourceProperties();
}
