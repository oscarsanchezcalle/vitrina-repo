namespace Vitrina.Api.Services.Authentication;

/// <summary>
/// Hashes and verifies passwords.
/// </summary>
public interface IPasswordHasher
{
	/// <summary>
	/// Hashes a password.
	/// </summary>
	/// <param name="password">The password.</param>
	/// <returns>The hashed password.</returns>
	string Hash(string password);

	/// <summary>
	/// Verifies a password against a stored hash.
	/// </summary>
	/// <param name="password">The password.</param>
	/// <param name="storedHash">The stored hash.</param>
	/// <returns>A value indicating whether the password matches.</returns>
	bool Verify(string password, string storedHash);
}
