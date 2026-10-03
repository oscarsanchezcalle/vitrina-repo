using Microsoft.EntityFrameworkCore;
using Vitrina.Domain.Entities;

namespace Vitrina.Data.Context;

public sealed class VitrinaDbContext(DbContextOptions<VitrinaDbContext> options) : DbContext(options)
{
	public DbSet<Product> Products => Set<Product>();
		
	public DbSet<User> Users => Set<User>();
	
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(VitrinaDbContext).Assembly);
	}
}
