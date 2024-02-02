
namespace RoyalCode.SmartMapper.Infrastructure.Naming;

public class IdNameHandler : SourceNameHandler
{
    private const string Name = "Id";
    
    public IdNameHandler()
    {
        Prefix = Name;
        Suffix = Name;
    }
    
    public override void Validate(NamingContext context)
    {
        
        
        throw new NotImplementedException();
    }
}