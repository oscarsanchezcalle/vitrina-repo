using System.Security.Cryptography;
using System.Text;

namespace Vitrina.Api.Services.Authentication;

/// <summary>
/// Provides PBKDF2 password hashing.
/// </summary>
public sealed class PasswordHasher : IPasswordHasher
{
	private const int Iterations = 100_000;
	private const int SaltSize = 16;
	private const int KeySize = 32;

	public string Hash(string password)
	{
		ArgumentNullException.ThrowIfNull(password);

		var salt = RandomNumberGenerator.GetBytes(SaltSize);
		var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, KeySize);

		return $"PBKDF2${Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
	}

	public bool Verify(string password, string storedHash)
	{
		ArgumentNullException.ThrowIfNull(password);
		ArgumentNullException.ThrowIfNull(storedHash);

		var parts = storedHash.Split('$', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
		if (parts.Length != 4 || !string.Equals(parts[0], "PBKDF2", StringComparison.OrdinalIgnoreCase))
		{
			return false;
		}

		if (!int.TryParse(parts[1], out var iterations))
		{
			return false;
		}

		var salt = Convert.FromBase64String(parts[2]);
		var expectedHash = Convert.FromBase64String(parts[3]);
		var actualHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, expectedHash.Length);

		return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
	}
}
