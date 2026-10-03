using Vitrina.Domain.Entities;

namespace Vitrina.Api.Services.Authentication;

/// <summary>
/// Creates JWT access tokens.
/// </summary>
public interface IJwtTokenService
{
	/// <summary>
	/// Creates a token for the specified user.
	/// </summary>
	/// <param name="user">The authenticated user.</param>
	/// <returns>The token and its expiration date.</returns>
	(string Token, DateTime ExpiresAt) CreateToken(User user);
}
