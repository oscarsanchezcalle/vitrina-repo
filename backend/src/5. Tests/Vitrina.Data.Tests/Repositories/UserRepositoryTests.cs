using Vitrina.Data.Entities;
using Vitrina.Data.Repositories;
using Vitrina.Data.Tests.TestInfrastructure;
using Vitrina.Dto.Common;

namespace Vitrina.Data.Tests.Repositories;

public sealed class UserRepositoryTests
{
	[Fact]
	public async Task GetByEmailAsync_And_GetByUsernameAsync_Return_Matching_User()
	{
		var scope = await SqliteTestContextFactory.CreateAsync();
		await using var connection = scope.Connection;
		await using var context = scope.Context;

		var repository = new UserRepository(context);
		var user = new User
		{
			Name = "Test Admin User",
			Email = "test-admin@vitrina.dev",
			Username = "test-admin",
			PasswordHash = "HASHED-PASSWORD",
			Role = UserRoleDto.Admin,
			CreatedAt = new DateTime(2025, 5, 4, 10, 0, 0, DateTimeKind.Utc)
		};

		await repository.AddAsync(user);
		await context.SaveChangesAsync();

		var byEmail = await repository.GetByEmailAsync("test-admin@vitrina.dev");
		var byUsername = await repository.GetByUsernameAsync("test-admin");

		Assert.NotNull(byEmail);
		Assert.NotNull(byUsername);
		Assert.Equal(user.Id, byEmail!.Id);
		Assert.Equal(user.Id, byUsername!.Id);
		Assert.Equal(UserRoleDto.Admin, byEmail.Role);
	}
}
