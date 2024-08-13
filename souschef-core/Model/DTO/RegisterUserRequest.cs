namespace souschef_core.Model.DTO;

public record RegisterUserRequest
{
    

    /// <summary>
    /// Optional BASE64 encoded image for profile picture
    /// </summary>
    public byte[]? Photo { get; init; }

    /// <summary>
    /// Username, will be used for logging in. Displayed if display name is not provided.
    /// </summary>
    public required string Username { get; init; }

    /// <summary>
    /// Plain text password. Must be 8-24 characters, include one upper and one lower case letter, and a special character.
    /// </summary>
    public required string Password { get; init; }

    /// <summary>
    /// Name to display publicly to other users, appears on authored recipes and comments.
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// Email address for the user.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// User's first name.
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// User's last name.
    /// </summary>
    public required string LastName { get; init; }
}