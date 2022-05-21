using System.Reflection;

namespace RoyalCode.SmartMapper.Exceptions;

public class UnmappablePropertyException : Exception
{
    private const string MessagePattern = "The '{0}' property of type '{1}' cannot be automatically mapped to type '{2}'.";
    
    public UnmappablePropertyException(PropertyInfo sourceProperty, Type to, Exception innerException)
        : base(string.Format(MessagePattern, sourceProperty.Name, sourceProperty.DeclaringType!.FullName, to.FullName), innerException)
    { }
}