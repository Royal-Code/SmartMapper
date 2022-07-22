using System.Reflection;

namespace RoyalCode.SmartMapper.Infrastructure.Adapters;

public class AdapterSourceToMethodOptions
{
    public MethodInfo? Method { get; internal set; }
    public string? MethodName { get; internal set; }

    public void ClearParameters()
    {
        throw new NotImplementedException();
    }
}