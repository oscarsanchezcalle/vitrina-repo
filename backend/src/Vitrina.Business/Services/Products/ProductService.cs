using Vitrina.Business.Extensions;
using Vitrina.Business.ValidationServices;
using Vitrina.Domain.Entities;
using Vitrina.Domain.Repositories;
using Vitrina.Dto.Common;
using Vitrina.Dto.Products;

namespace Vitrina.Business.Services.Products;

/// <summary>
/// Provides product application operations.
/// </summary>
public sealed class ProductService(
	IProductRepository productRepository,
	IProductValidationService productValidationService,
	IUnitOfWork unitOfWork) : IProductService
{
	/// <inheritdoc />
	public async Task<ProductDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
	{
		var product = await productRepository.GetByIdAsync(id, cancellationToken);
		return product?.ToDto();
	}

	/// <inheritdoc />
	public async Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		var products = await productRepository.GetAllAsync(cancellationToken);
		return products.Select(product => product.ToDto()).ToList();
	}

	/// <inheritdoc />
	public async Task<PagedResultDto<ProductDto>> SearchAsync(ProductSearchCriteriaDto criteria, CancellationToken cancellationToken = default)
	{
		var validationResult = productValidationService.ValidateSearchCriteria(criteria);
		if (!validationResult.Succeeded || criteria is null)
		{
			return new PagedResultDto<ProductDto>();
		}

		var result = await productRepository.SearchAsync(criteria.SearchTerm, criteria.IsActive, criteria.PageNumber, criteria.PageSize, cancellationToken);
		return new PagedResultDto<ProductDto>
		{
			Items = result.Items.Select(product => product.ToDto()).ToList(),
			TotalCount = result.TotalCount,
			PageNumber = result.PageNumber,
			PageSize = result.PageSize
		};
	}

	/// <inheritdoc />
	public async Task<ValidationResultDto> CreateAsync(CreateProductCriteriaDto criteria, CancellationToken cancellationToken = default)
	{
		var validationResult = productValidationService.ValidateCreateCriteria(criteria);
		if (!validationResult.Succeeded || criteria is null)
		{
			return validationResult;
		}

		var product = CreateProduct(criteria, DateTime.UtcNow);
		await productRepository.AddAsync(product, cancellationToken);
		await unitOfWork.SaveChangesAsync(cancellationToken);

		return ValidationResultDto.Success("Product created successfully.");
	}

	/// <inheritdoc />
	public async Task<ValidationResultDto> UpdateAsync(UpdateProductCriteriaDto criteria, CancellationToken cancellationToken = default)
	{
		var validationResult = productValidationService.ValidateUpdateCriteria(criteria);
		if (!validationResult.Succeeded || criteria is null)
		{
			return validationResult;
		}

		var product = await productRepository.GetByIdAsync(criteria.Id, cancellationToken);
		if (product is null)
		{
			return ValidationResultDto.Failure($"Product with id '{criteria.Id}' was not found.", $"Product with id '{criteria.Id}' was not found.");
		}

		ApplyUpdate(product, criteria, DateTime.UtcNow);

		productRepository.Update(product);
		await unitOfWork.SaveChangesAsync(cancellationToken);

		return ValidationResultDto.Success("Product updated successfully.");
	}

	/// <inheritdoc />
	public async Task<ValidationResultDto> DeleteAsync(int id, CancellationToken cancellationToken = default)
	{
		var product = await productRepository.GetByIdAsync(id, cancellationToken);
		if (product is null)
		{
			return ValidationResultDto.Failure($"Product with id '{id}' was not found.", $"Product with id '{id}' was not found.");
		}

		productRepository.Remove(product);
		await unitOfWork.SaveChangesAsync(cancellationToken);

		return ValidationResultDto.Success("Product deleted successfully.");
	}

	private static Product CreateProduct(CreateProductCriteriaDto criteria, DateTime utcNow)
	{
		return new Product
		{
			Name = criteria.Name.Trim(),
			Description = criteria.Description?.Trim(),
			Price = criteria.Price,
			Stock = criteria.Stock,
			Category = criteria.Category?.Trim(),
			ImageUrl = criteria.ImageUrl?.Trim(),
			IsActive = criteria.IsActive,
			CreatedAt = utcNow,
			UpdatedAt = null
		};
	}

	private static void ApplyUpdate(Product product, UpdateProductCriteriaDto criteria, DateTime utcNow)
	{
		product.Name = criteria.Name.Trim();
		product.Description = criteria.Description?.Trim();
		product.Price = criteria.Price;
		product.Stock = criteria.Stock;
		product.Category = criteria.Category?.Trim();
		product.ImageUrl = criteria.ImageUrl?.Trim();
		product.IsActive = criteria.IsActive;
		product.UpdatedAt = utcNow;
	}
}
