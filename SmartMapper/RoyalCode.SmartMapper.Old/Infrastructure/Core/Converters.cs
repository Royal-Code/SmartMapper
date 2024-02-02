using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace RoyalCode.SmartMapper.Infrastructure.Core;

/// <summary>
/// <para>
///     Contém os conversores pré-configurados.
/// </para>
/// </summary>
public class Converters
{
    private readonly Dictionary<MapKey, ConvertOptions> converters = new();

    public bool TryGetConverter(MapKey key, [NotNullWhen(true)] out ConvertOptions? convertOptions)
    {
        convertOptions = converters.ContainsKey(key) ? converters[key] : null;
        return convertOptions != null;
    }

    public bool TryGetConverter(Type source, Type target, [NotNullWhen(true)] out ConvertOptions? convertOptions)
        => TryGetConverter(new MapKey(source, target), out convertOptions);

    public void AddConverter(MapKey key, ConvertOptions options) => converters[key] = options;
}