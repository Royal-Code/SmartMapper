using System.Reflection;

namespace RoyalCode.SmartMapper.Exceptions;

public class InvalidParameterNameException : ArgumentException
{
    public InvalidParameterNameException(string argument)
        : base("The parameter name cannot be null or empty.", argument)
    { }

    public InvalidParameterNameException(string parameterName, MethodInfo method, string argument)
        : base($"Parameter '{parameterName}' does not exist in method '{method.Name}' of type '{method.DeclaringType?.Name}'.", argument)
    { }

    public InvalidParameterNameException(string parameterName, Type targetType, string argument)
        : base($"Parameter '{parameterName}' does not exist in the constructor of type '{targetType.Name}'.", argument)
    { }
}