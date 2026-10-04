using Microsoft.Extensions.DependencyInjection;
using Vitrina.Business.Services.Products;
using Vitrina.Business.ValidationServices;

namespace Vitrina.Business.Extensions;

/// <summary>
/// Provides dependency injection registration for the business layer.
/// </summary>
public static class ServiceCollectionExtensions
{
	extension(IServiceCollection services)
	{
		/// <summary>
		/// Registers business layer services.
		/// </summary>
		/// <returns>The updated service collection.</returns>
		public IServiceCollection AddBusiness()
		{
			services.AddScoped<IProductValidationService, ProductValidationService>();
			services.AddScoped<IProductService, ProductService>();

			return services;
		}
	}
}
