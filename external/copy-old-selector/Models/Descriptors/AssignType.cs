namespace RoyalCode.SmartSelector.Generators.Models.Descriptors;

internal enum AssignType
{
    /// <summary>
    /// Direct assignment, no conversion needed.
    /// </summary>
    /// <remarks>
    /// Example:
    /// <code>
    ///     Name = e.Name,
    ///     UserName = e.User.Name,
    /// </code>
    /// </remarks>
    Direct,

    /// <summary>
    /// Simple cast, no conversion needed.
    /// </summary>
    /// <remarks>
    /// Example:
    /// <code>
    ///     Status = (Status)e.Status,
    ///     UserStatus = (Status)e.User.Status,
    /// </code>
    /// </remarks>
    SimpleCast,

    /// <summary>
    /// Represents a nullable assignment performed using a ternary operator.
    /// This allows for conditional assignment based on the presence of a value.
    /// </summary>
    /// <remarks>
    /// Example:
    /// <code>
    ///     LastLogin = e.LastLogin.HasValue ? e.LastLogin.Value : default,
    /// </code>
    /// </remarks>
    NullableTernary,

    /// <summary>
    /// Represents a nullable assignment performed using a ternary operator with a cast.
    /// This allows for conditional assignment based on the presence of a value.
    /// </summary>
    /// <remarks>
    /// Example:
    /// <code>
    ///     LastLogin = e.LastLogin.HasValue ? (DateTime)e.LastLogin.Value : default,
    /// </code>
    /// </remarks>
    NullableTernaryCast,

    /// <summary>
    /// It is required to create a new object for the target property type,
    /// mapping the properties between the two objects.
    /// </summary>
    /// <remarks>
    /// Example:
    /// <code>
    ///     CreatedBy = new UserDto
    ///     {
    ///         Name = a.CreatedBy.Name
    ///     },
    /// </code>
    /// </remarks>
    NewInstance,

    /// <summary>
    /// It is required to apply the Select method to the target property type,
    /// to transform the elements accordingly.
    /// </summary>
    /// <remarks>
    /// Example:
    /// <code>
    ///     Posts = e.Posts.Select(p => new PostDto
    ///     {
    ///         Title = p.Title,
    ///         Content = p.Content
    ///     }),
    /// </code>
    /// </remarks>
    Select,
}
