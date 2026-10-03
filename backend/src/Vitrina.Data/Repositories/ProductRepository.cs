using Microsoft.EntityFrameworkCore;
using Vitrina.Data.Context;
using Vitrina.Domain.Common;
using Vitrina.Domain.Entities;
using Vitrina.Domain.Repositories;

namespace Vitrina.Data.Repositories;

public sealed class ProductRepository(VitrinaDbContext context) : IProductRepository
{
	public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
	{
		return await context.Products.AsNoTracking().FirstOrDefaultAsync(product => product.Id == id, cancellationToken);
	}

	public async Task<PagedResult<Product>> SearchAsync(string? searchTerm, bool? isActive, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
	{
		pageNumber = pageNumber < 1 ? 1 : pageNumber;
		pageSize = pageSize < 1 ? 10 : pageSize;

		IQueryable<Product> query = context.Products.AsNoTracking();

		if (!string.IsNullOrWhiteSpace(searchTerm))
		{
			var normalizedSearchTerm = searchTerm.Trim().ToLower();
			query = query.Where(product =>
				product.Name.ToLower().Contains(normalizedSearchTerm) ||
				(product.Description != null && product.Description.ToLower().Contains(normalizedSearchTerm)) ||
				(product.Category != null && product.Category.ToLower().Contains(normalizedSearchTerm)));
		}

		if (isActive.HasValue)
		{
			query = query.Where(product => product.IsActive == isActive.Value);
		}

		var totalCount = await query.CountAsync(cancellationToken);
		var items = await query
			.OrderBy(product => product.Id)
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync(cancellationToken);

		return new PagedResult<Product>(items, totalCount, pageNumber, pageSize);
	}

	public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		return await context.Products.AsNoTracking().OrderBy(product => product.Id).ToListAsync(cancellationToken);
	}

	public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
	{
		await context.Products.AddAsync(product, cancellationToken);
	}

	public void Update(Product product)
	{
		context.Products.Update(product);
	}

	public void Remove(Product product)
	{
		context.Products.Remove(product);
	}
}
