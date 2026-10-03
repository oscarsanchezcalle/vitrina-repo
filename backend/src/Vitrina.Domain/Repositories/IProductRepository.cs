using Vitrina.Domain.Common;
using Vitrina.Domain.Entities;

namespace Vitrina.Domain.Repositories;

public interface IProductRepository
{
	Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

	Task<PagedResult<Product>> SearchAsync(string? searchTerm, bool? isActive, int pageNumber, int pageSize, CancellationToken cancellationToken = default);

	Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default);

	Task AddAsync(Product product, CancellationToken cancellationToken = default);

	void Update(Product product);

	void Remove(Product product);
}
