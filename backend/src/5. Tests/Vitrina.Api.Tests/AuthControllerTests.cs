using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Vitrina.Api.Controllers;
using Vitrina.Api.Services.Authentication;
using Vitrina.Dto.Auth;
using Vitrina.Dto.Common;

namespace Vitrina.Api.Tests;

public sealed class AuthControllerTests
{
	[Fact]
	public async Task Login_WhenAuthenticationSucceeds_ReturnsOkWithToken()
	{
		var authService = Substitute.For<IAuthService>();
		var request = new LoginRequestDto { Username = "admin", Password = "Admin123!" };
		var response = new LoginResponseDto
		{
			AccessToken = "token",
			ExpiresAt = DateTime.UtcNow.AddHours(1),
			Name = "Admin User",
			Username = "admin",
			Role = "Admin"
		};
		authService.AuthenticateAsync(request, Arg.Any<CancellationToken>()).Returns(response);

		var controller = new AuthController(authService);

		var result = await controller.Login(request, CancellationToken.None);

		var okResult = Assert.IsType<OkObjectResult>(result.Result);
		Assert.Same(response, okResult.Value);
	}

	[Fact]
	public async Task Login_WhenAuthenticationFails_ReturnsUnauthorized()
	{
		var authService = Substitute.For<IAuthService>();
		var request = new LoginRequestDto { Username = "admin", Password = "wrong" };
		authService.AuthenticateAsync(request, Arg.Any<CancellationToken>()).Returns((LoginResponseDto?)null);

		var controller = new AuthController(authService);

		var result = await controller.Login(request, CancellationToken.None);

		var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result.Result);
		var validationResult = Assert.IsType<ValidationResultDto>(unauthorizedResult.Value);
		Assert.False(validationResult.Succeeded);
		Assert.Equal("Authentication failed.", validationResult.Message);
		Assert.Contains("Invalid username or password.", validationResult.Errors);
	}
}
