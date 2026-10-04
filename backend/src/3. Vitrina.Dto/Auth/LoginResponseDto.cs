namespace Vitrina.Dto.Auth;

/// <summary>
/// Represents an authenticated session.
/// </summary>
public sealed record LoginResponseDto
{
	/// <summary>
	/// Gets or sets the access token.
	/// </summary>
	public string AccessToken { get; init; } = string.Empty;

	/// <summary>
	/// Gets or sets the token expiration date and time.
	/// </summary>
	public DateTime ExpiresAt { get; init; }

	/// <summary>
	/// Gets or sets the user name.
	/// </summary>
	public string Name { get; init; } = string.Empty;

	/// <summary>
	/// Gets or sets the username.
	/// </summary>
	public string Username { get; init; } = string.Empty;

	/// <summary>
	/// Gets or sets the user role.
	/// </summary>
	public string Role { get; init; } = string.Empty;
}
