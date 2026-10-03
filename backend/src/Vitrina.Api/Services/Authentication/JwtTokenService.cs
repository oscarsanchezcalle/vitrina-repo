using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Vitrina.Api.Options;
using Vitrina.Domain.Entities;

namespace Vitrina.Api.Services.Authentication;

/// <summary>
/// Creates JWT access tokens.
/// </summary>
public sealed class JwtTokenService(IOptions<JwtAuthOptions> options) : IJwtTokenService
{
	public (string Token, DateTime ExpiresAt) CreateToken(User user)
	{
		ArgumentNullException.ThrowIfNull(user);

		var jwtOptions = options.Value;
		var expiresAt = DateTime.UtcNow.AddMinutes(jwtOptions.ExpirationMinutes);
		var claims = new List<Claim>
		{
			new(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new(ClaimTypes.Name, user.Username),
			new(ClaimTypes.Email, user.Email),
			new(ClaimTypes.Role, user.Role.ToString())
		};

		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key));
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
		var token = new JwtSecurityToken(
			issuer: jwtOptions.Issuer,
			audience: jwtOptions.Audience,
			claims: claims,
			notBefore: DateTime.UtcNow,
			expires: expiresAt,
			signingCredentials: credentials);

		return (new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
	}
}
