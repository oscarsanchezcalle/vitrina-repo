using Vitrina.Business.ValidationServices;
using Vitrina.Dto.Products;

namespace Vitrina.Business.Tests.ValidationServices;

public sealed class ProductValidationServiceTests
{
	[Fact]
	public void ValidateCreateCriteria_ReturnsFailure_WhenDataIsInvalid()
	{
		var service = new ProductValidationService();
		var criteria = new CreateProductCriteriaDto
		{
			Name = string.Empty,
			Price = -1,
			Stock = -2,
			Description = new string('a', 2001),
			Category = new string('b', 101),
			ImageUrl = new string('c', 501),
			IsActive = true
		};

		var result = service.ValidateCreateCriteria(criteria);

		Assert.False(result.Succeeded);
		Assert.NotEmpty(result.Errors);
	}

	[Fact]
	public void ValidateCreateCriteria_ReturnsFailure_WhenCriteriaIsNull()
	{
		var service = new ProductValidationService();

		var result = service.ValidateCreateCriteria(null);

		Assert.False(result.Succeeded);
		Assert.Contains(result.Errors, error => error.Contains("Product data is required."));
	}

	[Fact]
	public void ValidateUpdateCriteria_ReturnsFailure_WhenIdIsInvalid()
	{
		var service = new ProductValidationService();
		var criteria = new UpdateProductCriteriaDto
		{
			Id = 0,
			Name = "Product",
			Price = 1m,
			Stock = 1,
			IsActive = true
		};

		var result = service.ValidateUpdateCriteria(criteria);

		Assert.False(result.Succeeded);
		Assert.Contains(result.Errors, error => error.Contains("Product id must be greater than zero."));
	}

	[Fact]
	public void ValidateUpdateCriteria_ReturnsFailure_WhenCriteriaIsNull()
	{
		var service = new ProductValidationService();

		var result = service.ValidateUpdateCriteria(null);

		Assert.False(result.Succeeded);
		Assert.Contains(result.Errors, error => error.Contains("Product data is required."));
	}

	[Fact]
	public void ValidateSearchCriteria_ReturnsFailure_WhenCriteriaIsNull()
	{
		var service = new ProductValidationService();

		var result = service.ValidateSearchCriteria(null);

		Assert.False(result.Succeeded);
		Assert.Contains(result.Errors, error => error.Contains("Product search criteria is required."));
	}
}
