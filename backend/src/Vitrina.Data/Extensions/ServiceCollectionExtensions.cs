using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vitrina.Data.Context;
using Vitrina.Data.Repositories;
using Vitrina.Domain.Repositories;

namespace Vitrina.Data.Extensions;

/// <summary>
/// Provides dependency injection registration for the data layer.
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Registers the data layer services.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <param name="configuration">The application configuration.</param>
	/// <returns>The updated service collection.</returns>
	public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnection")
			?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

		services.AddDbContext<VitrinaDbContext>(options =>
			options.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure()));

		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<IUserRepository, UserRepository>();

		return services;
	}
}
