using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vitrina.Api.Services.Authentication;
using Vitrina.Dto.Auth;
using Vitrina.Dto.Common;

namespace Vitrina.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController(IAuthService authService) : ControllerBase
{
	[AllowAnonymous]
	[HttpPost("login")]
	[ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ValidationResultDto), StatusCodes.Status401Unauthorized)]
	public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto request, CancellationToken cancellationToken)
	{
		var result = await authService.AuthenticateAsync(request, cancellationToken);
		if (result is null)
		{
			return Unauthorized(ValidationResultDto.Failure("Authentication failed.", "Invalid username or password."));
		}

		return Ok(result);
	}
}
