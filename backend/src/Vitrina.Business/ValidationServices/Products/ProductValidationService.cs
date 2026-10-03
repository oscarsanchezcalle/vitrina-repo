using Vitrina.Dto.Common;
using Vitrina.Dto.Products;

namespace Vitrina.Business.ValidationServices;

/// <summary>
/// Provides validation rules for products.
/// </summary>
public sealed class ProductValidationService : IProductValidationService
{
	private const int NameMaxLength = 255;
	private const int DescriptionMaxLength = 2000;
	private const int CategoryMaxLength = 100;
	private const int ImageUrlMaxLength = 500;

	/// <inheritdoc />
	public ValidationResultDto ValidateCreateCriteria(CreateProductCriteriaDto? criteria)
	{
		if (criteria is null)
		{
			return ValidationResultDto.Failure("The product data is invalid.", "Product data is required.");
		}

		var errors = ValidateCore(criteria.Name, criteria.Description, criteria.Price, criteria.Stock, criteria.Category, criteria.ImageUrl);
		return errors.Count == 0
			? ValidationResultDto.Success()
			: ValidationResultDto.Failure("The product data is invalid.", errors.ToArray());
	}

	/// <inheritdoc />
	public ValidationResultDto ValidateUpdateCriteria(UpdateProductCriteriaDto? criteria)
	{
		if (criteria is null)
		{
			return ValidationResultDto.Failure("The product data is invalid.", "Product data is required.");
		}

		var errors = ValidateCore(criteria.Name, criteria.Description, criteria.Price, criteria.Stock, criteria.Category, criteria.ImageUrl);
		if (criteria.Id <= 0)
		{
			errors.Add("Product id must be greater than zero.");
		}

		return errors.Count == 0
			? ValidationResultDto.Success()
			: ValidationResultDto.Failure("The product data is invalid.", errors.ToArray());
	}

	/// <inheritdoc />
	public ValidationResultDto ValidateSearchCriteria(ProductSearchCriteriaDto? criteria)
	{
		if (criteria is null)
		{
			return ValidationResultDto.Failure("The product search criteria is invalid.", "Product search criteria is required.");
		}

		if (criteria.PageNumber <= 0)
		{
			return ValidationResultDto.Failure("The product search criteria is invalid.", "Page number must be greater than zero.");
		}

		if (criteria.PageSize <= 0)
		{
			return ValidationResultDto.Failure("The product search criteria is invalid.", "Page size must be greater than zero.");
		}

		return ValidationResultDto.Success();
	}

	private static List<string> ValidateCore(string name, string? description, decimal price, int stock, string? category, string? imageUrl)
	{
		var errors = new List<string>();

		if (string.IsNullOrWhiteSpace(name))
		{
			errors.Add("Product name is required.");
		}
		else if (name.Trim().Length > NameMaxLength)
		{
			errors.Add($"Product name cannot exceed {NameMaxLength} characters.");
		}

		if (description?.Length > DescriptionMaxLength)
		{
			errors.Add($"Product description cannot exceed {DescriptionMaxLength} characters.");
		}

		if (price < 0)
		{
			errors.Add("Product price cannot be negative.");
		}

		if (stock < 0)
		{
			errors.Add("Product stock cannot be negative.");
		}

		if (category?.Length > CategoryMaxLength)
		{
			errors.Add($"Product category cannot exceed {CategoryMaxLength} characters.");
		}

		if (imageUrl?.Length > ImageUrlMaxLength)
		{
			errors.Add($"Product image URL cannot exceed {ImageUrlMaxLength} characters.");
		}

		return errors;
	}
}
