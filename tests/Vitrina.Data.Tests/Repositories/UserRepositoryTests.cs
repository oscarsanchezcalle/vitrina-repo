using Vitrina.Data.Repositories;
using Vitrina.Data.Tests.TestInfrastructure;
using Vitrina.Domain.Entities;
using Vitrina.Domain.Enums;

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
			Name = "Admin User",
			Email = "admin@vitrina.dev",
			Username = "admin",
			PasswordHash = "HASHED-PASSWORD",
			Role = UserRole.Admin,
			CreatedAt = new DateTime(2025, 5, 4, 10, 0, 0, DateTimeKind.Utc)
		};

		await repository.AddAsync(user);
		await context.SaveChangesAsync();

		var byEmail = await repository.GetByEmailAsync("admin@vitrina.dev");
		var byUsername = await repository.GetByUsernameAsync("admin");

		Assert.NotNull(byEmail);
		Assert.NotNull(byUsername);
		Assert.Equal(user.Id, byEmail!.Id);
		Assert.Equal(user.Id, byUsername!.Id);
		Assert.Equal(UserRole.Admin, byEmail.Role);
	}
}
