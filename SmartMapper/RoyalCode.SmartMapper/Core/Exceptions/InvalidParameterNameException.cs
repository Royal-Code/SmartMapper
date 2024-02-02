using System.Reflection;

namespace RoyalCode.SmartMapper.Core.Exceptions;

/// <summary>
/// Exception thrown when the parameter name of a method or constructor is invalid.
/// </summary>
public sealed class InvalidParameterNameException : ArgumentException
{
    /// <summary>
    /// Create a new instance of the <see cref="InvalidParameterNameException"/> class.
    /// </summary>
    /// <param name="argument">The argument that caused the exception.</param>
    public InvalidParameterNameException(string argument)
        : base("The parameter name cannot be null or empty.", argument)
    { }

    /// <summary>
    /// Creates a new instance of the <see cref="InvalidParameterNameException"/> class.
    /// </summary>
    /// <param name="parameterName">The parameter name.</param>
    /// <param name="method">The method.</param>
    /// <param name="argument">The argument that caused the exception.</param>
    public InvalidParameterNameException(string parameterName, MethodInfo method, string argument)
        : base($"Parameter '{parameterName}' does not exist in method '{method.Name}' of type '{method.DeclaringType?.Name}'.", argument)
    { }

    /// <summary>
    /// Creates a new instance of the <see cref="InvalidParameterNameException"/> class.
    /// </summary>
    /// <param name="parameterName">The parameter name.</param>
    /// <param name="targetType">The target type.</param>
    /// <param name="argument">The argument that caused the exception.</param>
    public InvalidParameterNameException(string parameterName, Type targetType, string argument)
        : base($"Parameter '{parameterName}' does not exist in the constructor of type '{targetType.Name}'.", argument)
    { }
}