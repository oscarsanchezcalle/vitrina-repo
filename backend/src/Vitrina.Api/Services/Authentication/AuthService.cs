using Vitrina.Api.Services.Authentication;
using Vitrina.Domain.Repositories;
using Vitrina.Dto.Auth;

namespace Vitrina.Api.Services.Authentication;

/// <summary>
/// Authenticates users and creates JWT responses.
/// </summary>
public sealed class AuthService(
	IUserRepository userRepository,
	IPasswordHasher passwordHasher,
	IJwtTokenService tokenService) : IAuthService
{
	public async Task<LoginResponseDto?> AuthenticateAsync(LoginRequestDto request, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(request);

		if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
		{
			return null;
		}

		var user = await userRepository.GetByUsernameAsync(request.Username.Trim(), cancellationToken);
		if (user is null || !passwordHasher.Verify(request.Password, user.PasswordHash))
		{
			return null;
		}

		var (token, expiresAt) = tokenService.CreateToken(user);
		return new LoginResponseDto
		{
			AccessToken = token,
			ExpiresAt = expiresAt,
			Name = user.Name,
			Username = user.Username,
			Role = user.Role.ToString()
		};
	}
}
