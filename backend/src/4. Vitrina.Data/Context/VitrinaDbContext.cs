using Microsoft.EntityFrameworkCore;
using Vitrina.Data.Entities;
using Vitrina.Data.Repositories;

namespace Vitrina.Data.Context;

/// <summary>
/// Represents the application's Entity Framework database context.
/// </summary>
public sealed class VitrinaDbContext(DbContextOptions<VitrinaDbContext> options) : DbContext(options), IUnitOfWork
{
	/// <summary>
	/// Gets the product set.
	/// </summary>
	public DbSet<Product> Products => Set<Product>();

	/// <summary>
	/// Gets the user set.
	/// </summary>
	public DbSet<User> Users => Set<User>();

	/// <summary>
	/// Persists pending changes.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The number of affected rows.</returns>
	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		return base.SaveChangesAsync(cancellationToken);
	}

	/// <summary>
	/// Configures the EF Core model using entity configurations from the current assembly.
	/// </summary>
	/// <param name="modelBuilder">The model builder.</param>
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(VitrinaDbContext).Assembly);
	}
}
