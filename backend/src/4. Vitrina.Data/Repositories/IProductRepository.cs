using Vitrina.Data.Common;
using Vitrina.Data.Entities;

namespace Vitrina.Data.Repositories;

/// <summary>
/// Defines product persistence operations.
/// </summary>
public interface IProductRepository
{
	/// <summary>
	/// Gets a product by its identifier.
	/// </summary>
	/// <param name="id">The product identifier.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The matching product or null.</returns>
	Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Searches products using a text filter and optional active flag.
	/// </summary>
	/// <param name="searchTerm">The search term.</param>
	/// <param name="isActive">The active flag filter.</param>
	/// <param name="pageNumber">The page number.</param>
	/// <param name="pageSize">The page size.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A paged result of products.</returns>
	Task<PagedResult<Product>> SearchAsync(string? searchTerm, bool? isActive, int pageNumber, int pageSize, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets all products.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The list of products.</returns>
	Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Adds a product to the repository.
	/// </summary>
	/// <param name="product">The product to add.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	Task AddAsync(Product product, CancellationToken cancellationToken = default);

	/// <summary>
	/// Marks a product as updated in the current context.
	/// </summary>
	/// <param name="product">The product to update.</param>
	void Update(Product product);

	/// <summary>
	/// Marks a product as removed in the current context.
	/// </summary>
	/// <param name="product">The product to remove.</param>
	void Remove(Product product);
}
