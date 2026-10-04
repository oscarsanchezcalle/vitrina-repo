using Vitrina.Dto.Common;

namespace Vitrina.Data.Entities;

/// <summary>
/// Represents a user persisted by the application.
/// </summary>
public sealed class User
{
	/// <summary>
	/// Gets or sets the unique identifier of the user.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the user's display name.
	/// </summary>
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the user's email address.
	/// </summary>
	public string Email { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the user's username.
	/// </summary>
	public string Username { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the hashed password.
	/// </summary>
	public string PasswordHash { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the user role.
	/// </summary>
	public UserRoleDto Role { get; set; }

	/// <summary>
	/// Gets or sets the creation date and time.
	/// </summary>
	public DateTime CreatedAt { get; set; }
}
