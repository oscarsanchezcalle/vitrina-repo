using Vitrina.Domain.Entities;
using Vitrina.Dto.Products;

namespace Vitrina.Business.Extensions;

/// <summary>
/// Provides mapping helpers for products.
/// </summary>
public static class ProductExtensions
{
	extension(Product product)
	{
		/// <summary>
		/// Converts the entity to a DTO.
		/// </summary>
		/// <returns>The mapped DTO.</returns>
		public ProductDto ToDto() => new()
		{
			Id = product.Id,
			Name = product.Name,
			Description = product.Description,
			Price = product.Price,
			Stock = product.Stock,
			Category = product.Category,
			ImageUrl = product.ImageUrl,
			IsActive = product.IsActive,
			CreatedAt = product.CreatedAt,
			UpdatedAt = product.UpdatedAt
		};
	}
}
