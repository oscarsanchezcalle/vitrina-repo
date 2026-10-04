namespace Vitrina.Dto.Auth;

/// <summary>
/// Represents a login request.
/// </summary>
public sealed record LoginRequestDto
{
	/// <summary>
	/// Gets or sets the username.
	/// </summary>
	public string Username { get; init; } = string.Empty;

	/// <summary>
	/// Gets or sets the password.
	/// </summary>
	public string Password { get; init; } = string.Empty;
}
