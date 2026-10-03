using Vitrina.Dto.Auth;

namespace Vitrina.Api.Services.Authentication;

/// <summary>
/// Authenticates users and creates JWT responses.
/// </summary>
public interface IAuthService
{
	/// <summary>
	/// Authenticates a user.
	/// </summary>
	/// <param name="request">The login request.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The login response or null if authentication fails.</returns>
	Task<LoginResponseDto?> AuthenticateAsync(LoginRequestDto request, CancellationToken cancellationToken = default);
}
