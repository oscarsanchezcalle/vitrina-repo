using Vitrina.Dto.Common;
using Vitrina.Dto.Products;

namespace Vitrina.Business.Services.Products;

/// <summary>
/// Defines product application operations.
/// </summary>
public interface IProductService
{
	/// <summary>
	/// Gets a product by its identifier.
	/// </summary>
	/// <param name="id">The product identifier.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The matching product or null.</returns>
	Task<ProductDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets all products.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The list of products.</returns>
	Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Searches products.
	/// </summary>
	/// <param name="criteria">The search criteria.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A paged result of products.</returns>
	Task<PagedResultDto<ProductDto>> SearchAsync(ProductSearchCriteriaDto criteria, CancellationToken cancellationToken = default);

	/// <summary>
	/// Creates a product.
	/// </summary>
	/// <param name="criteria">The create criteria.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The validation result.</returns>
	Task<ValidationResultDto> CreateAsync(CreateProductCriteriaDto criteria, CancellationToken cancellationToken = default);

	/// <summary>
	/// Updates a product.
	/// </summary>
	/// <param name="criteria">The update criteria.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The validation result.</returns>
	Task<ValidationResultDto> UpdateAsync(UpdateProductCriteriaDto criteria, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes a product.
	/// </summary>
	/// <param name="id">The product identifier.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The validation result.</returns>
	Task<ValidationResultDto> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
