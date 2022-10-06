namespace RoyalCode.SmartMapper.Infrastructure.Core;

public class NameHandlers
{
    private readonly List<SourceNameHandler> sourceNameHandlers = new();
    private readonly List<TargetNameHandler> targetNameHandlers = new();

    public IEnumerable<SourceNameHandler> SourceNameHandlers => sourceNameHandlers;

    public IEnumerable<TargetNameHandler> TargetNameHandlers => targetNameHandlers;

    public void Add(SourceNameHandler nameHandler) => sourceNameHandlers.Add(nameHandler);

    public void Add(TargetNameHandler nameHandler) => targetNameHandlers.Add(nameHandler);
}