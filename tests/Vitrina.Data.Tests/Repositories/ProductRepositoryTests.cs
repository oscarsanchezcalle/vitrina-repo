using Vitrina.Data.Repositories;
using Vitrina.Data.Tests.TestInfrastructure;
using Vitrina.Domain.Entities;

namespace Vitrina.Data.Tests.Repositories;

public sealed class ProductRepositoryTests
{
	[Fact]
	public async Task AddAsync_Persists_Product()
	{
		var scope = await SqliteTestContextFactory.CreateAsync();
		await using var connection = scope.Connection;
		await using var context = scope.Context;

		var repository = new ProductRepository(context);
		var product = new Product
		{
			Name = "Conference Laptop Stand",
			Description = "Portable aluminum laptop stand for hybrid work setups.",
			Price = 59.99m,
			Stock = 14,
			Category = "Office",
			ImageUrl = "https://images.unsplash.com/photo-1498050108023-c5249f4df085?auto=format&fit=crop&w=1200&q=80",
			IsActive = true,
			CreatedAt = new DateTime(2025, 5, 1, 10, 0, 0, DateTimeKind.Utc)
		};

		await repository.AddAsync(product);
		await context.SaveChangesAsync();

		var persisted = await repository.GetByIdAsync(product.Id);

		Assert.NotNull(persisted);
		Assert.Equal("Conference Laptop Stand", persisted!.Name);
		Assert.Equal(59.99m, persisted.Price);
	}

	[Fact]
	public async Task SearchAsync_Returns_Paged_Matches()
	{
		var scope = await SqliteTestContextFactory.CreateAsync();
		await using var connection = scope.Connection;
		await using var context = scope.Context;

		var repository = new ProductRepository(context);
		await repository.AddAsync(new Product
		{
			Name = "Interview Desk Microphone",
			Description = "USB microphone for remote interviews and presentations.",
			Price = 79.00m,
			Stock = 8,
			Category = "Audio",
			ImageUrl = "https://images.unsplash.com/photo-1590658268037-6bf12165a8df?auto=format&fit=crop&w=1200&q=80",
			IsActive = false,
			CreatedAt = new DateTime(2025, 5, 2, 10, 0, 0, DateTimeKind.Utc)
		});
		await repository.AddAsync(new Product
		{
			Name = "Interview Desk Light",
			Description = "Soft LED desk light for video calls.",
			Price = 39.00m,
			Stock = 10,
			Category = "Office",
			ImageUrl = "https://images.unsplash.com/photo-1552860348-8e8b6e2d9f1f?auto=format&fit=crop&w=1200&q=80",
			IsActive = false,
			CreatedAt = new DateTime(2025, 5, 3, 10, 0, 0, DateTimeKind.Utc)
		});
		await context.SaveChangesAsync();

		var result = await repository.SearchAsync("Interview", null, pageNumber: 1, pageSize: 1);

		Assert.Equal(2, result.TotalCount);
		Assert.Single(result.Items);
		Assert.Equal(1, result.PageNumber);
		Assert.Equal(1, result.PageSize);
	}
}
