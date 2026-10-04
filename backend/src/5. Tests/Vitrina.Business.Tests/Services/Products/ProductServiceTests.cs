using NSubstitute;
using Vitrina.Business.Services.Products;
using Vitrina.Business.ValidationServices;
using Vitrina.Data.Common;
using Vitrina.Data.Entities;
using Vitrina.Data.Repositories;
using Vitrina.Dto.Common;
using Vitrina.Dto.Products;

namespace Vitrina.Business.Tests.Services.Products;

public sealed class ProductServiceTests
{
	private readonly IProductRepository productRepository = Substitute.For<IProductRepository>();
	private readonly IProductValidationService productValidationService = Substitute.For<IProductValidationService>();
	private readonly IUnitOfWork unitOfWork = Substitute.For<IUnitOfWork>();

	[Fact]
	public async Task GetByIdAsync_ReturnsDto_WhenProductExists()
	{
		var service = new ProductService(productRepository, productValidationService, unitOfWork);
		var product = new Product
		{
			Id = 1,
			Name = "Monitor",
			Description = "4K",
			Price = 100m,
			Stock = 5,
			Category = "Computing",
			ImageUrl = "https://example.com/monitor.png",
			IsActive = true,
			CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
		};

		productRepository.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(product);

		var result = await service.GetByIdAsync(1);

		Assert.NotNull(result);
		Assert.Equal(product.Id, result!.Id);
		Assert.Equal(product.Name, result.Name);
	}

	[Fact]
	public async Task CreateAsync_SavesChanges_WhenValidationPasses()
	{
		var service = new ProductService(productRepository, productValidationService, unitOfWork);
		var criteria = new CreateProductCriteriaDto
		{
			Name = "Monitor",
			Description = "4K",
			Price = 100m,
			Stock = 5,
			Category = "Computing",
			ImageUrl = "https://example.com/monitor.png",
			IsActive = true
		};
		var validation = ValidationResultDto.Success();

		productValidationService.ValidateCreateCriteria(criteria).Returns(validation);
		unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

		var result = await service.CreateAsync(criteria);

		Assert.True(result.Succeeded);
		await productRepository.Received(1).AddAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
		await unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task CreateAsync_ReturnsFailure_WhenCriteriaIsNull()
	{
		var service = new ProductService(productRepository, productValidationService, unitOfWork);

		productValidationService.ValidateCreateCriteria(null).Returns(ValidationResultDto.Failure("The product data is invalid.", "Product data is required."));

		var result = await service.CreateAsync(null!);

		Assert.False(result.Succeeded);
		await productRepository.DidNotReceive().AddAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
		await unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task UpdateAsync_ReturnsFailure_WhenProductDoesNotExist()
	{
		var service = new ProductService(productRepository, productValidationService, unitOfWork);
		var criteria = new UpdateProductCriteriaDto
		{
			Id = 99,
			Name = "Monitor",
			Description = "4K",
			Price = 100m,
			Stock = 5,
			Category = "Computing",
			ImageUrl = "https://example.com/monitor.png",
			IsActive = true
		};

		productValidationService.ValidateUpdateCriteria(criteria).Returns(ValidationResultDto.Success());
		productRepository.GetByIdAsync(criteria.Id, Arg.Any<CancellationToken>()).Returns((Product?)null);

		var result = await service.UpdateAsync(criteria);

		Assert.False(result.Succeeded);
		await unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task UpdateAsync_ReturnsFailure_WhenCriteriaIsNull()
	{
		var service = new ProductService(productRepository, productValidationService, unitOfWork);

		productValidationService.ValidateUpdateCriteria(null).Returns(ValidationResultDto.Failure("The product data is invalid.", "Product data is required."));

		var result = await service.UpdateAsync(null!);

		Assert.False(result.Succeeded);
		await productRepository.DidNotReceive().GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>());
		await unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task SearchAsync_MapsPagedResult()
	{
		var service = new ProductService(productRepository, productValidationService, unitOfWork);
		var criteria = new ProductSearchCriteriaDto
		{
			SearchTerm = "monitor",
			IsActive = true,
			PageNumber = 2,
			PageSize = 10
		};
		var products = new PagedResult<Product>(
			[new Product { Id = 1, Name = "Monitor", Price = 100m, Stock = 5, IsActive = true, CreatedAt = DateTime.UtcNow }],
			1,
			2,
			10);

		productValidationService.ValidateSearchCriteria(criteria).Returns(ValidationResultDto.Success());
		productRepository.SearchAsync(criteria.SearchTerm, criteria.IsActive, criteria.PageNumber, criteria.PageSize, Arg.Any<CancellationToken>())
			.Returns(products);

		var result = await service.SearchAsync(criteria);

		Assert.Equal(1, result.TotalCount);
		Assert.Single(result.Items);
		Assert.Equal("Monitor", result.Items[0].Name);
	}

	[Fact]
	public async Task SearchAsync_ReturnsEmptyResult_WhenCriteriaIsNull()
	{
		var service = new ProductService(productRepository, productValidationService, unitOfWork);

		productValidationService.ValidateSearchCriteria(null).Returns(ValidationResultDto.Failure("The product search criteria is invalid.", "Product search criteria is required."));

		var result = await service.SearchAsync(null!);

		Assert.Empty(result.Items);
		Assert.Equal(0, result.TotalCount);
		await productRepository.DidNotReceive().SearchAsync(Arg.Any<string?>(), Arg.Any<bool?>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<CancellationToken>());
	}
}
