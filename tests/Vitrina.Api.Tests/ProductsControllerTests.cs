using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Vitrina.Api.Controllers;
using Vitrina.Business.Services.Products;
using Vitrina.Dto.Common;
using Vitrina.Dto.Products;

namespace Vitrina.Api.Tests;

public sealed class ProductsControllerTests
{
	[Fact]
	public async Task Search_ReturnsOkWithPagedResult()
	{
		var productService = Substitute.For<IProductService>();
		var criteria = new ProductSearchCriteriaDto { SearchTerm = "phone", PageNumber = 1, PageSize = 10 };
		var expected = new PagedResultDto<ProductDto>
		{
			Items = [new ProductDto { Id = 1, Name = "Phone", Price = 100m, Stock = 5, IsActive = true, CreatedAt = DateTime.UtcNow }],
			TotalCount = 1,
			PageNumber = 1,
			PageSize = 10
		};
		productService.SearchAsync(criteria, Arg.Any<CancellationToken>()).Returns(expected);

		var controller = new ProductsController(productService);

		var result = await controller.Search(criteria, CancellationToken.None);

		var okResult = Assert.IsType<OkObjectResult>(result.Result);
		Assert.Same(expected, okResult.Value);
	}

	[Fact]
	public async Task GetById_WhenFound_ReturnsOkWithProduct()
	{
		var productService = Substitute.For<IProductService>();
		var product = new ProductDto { Id = 1, Name = "Phone", Price = 100m, Stock = 5, IsActive = true, CreatedAt = DateTime.UtcNow };
		productService.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(product);

		var controller = new ProductsController(productService);

		var result = await controller.GetById(1, CancellationToken.None);

		var okResult = Assert.IsType<OkObjectResult>(result.Result);
		Assert.Same(product, okResult.Value);
	}

	[Fact]
	public async Task GetById_WhenMissing_ReturnsNotFound()
	{
		var productService = Substitute.For<IProductService>();
		productService.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns((ProductDto?)null);

		var controller = new ProductsController(productService);

		var result = await controller.GetById(1, CancellationToken.None);

		var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
		var validationResult = Assert.IsType<ValidationResultDto>(notFoundResult.Value);
		Assert.False(validationResult.Succeeded);
		Assert.Equal("Product with id '1' was not found.", validationResult.Message);
	}

	[Fact]
	public async Task Create_WhenValid_ReturnsOk()
	{
		var productService = Substitute.For<IProductService>();
		var criteria = new CreateProductCriteriaDto { Name = "New product", Price = 10m, Stock = 1, IsActive = true };
		var resultDto = ValidationResultDto.Success("Product created.");
		productService.CreateAsync(criteria, Arg.Any<CancellationToken>()).Returns(resultDto);

		var controller = new ProductsController(productService);

		var result = await controller.Create(criteria, CancellationToken.None);

		var okResult = Assert.IsType<OkObjectResult>(result.Result);
		Assert.Same(resultDto, okResult.Value);
	}

	[Fact]
	public async Task Create_WhenInvalid_ReturnsBadRequest()
	{
		var productService = Substitute.For<IProductService>();
		var criteria = new CreateProductCriteriaDto { Name = string.Empty };
		var resultDto = ValidationResultDto.Failure("Validation failed.", "Name is required.");
		productService.CreateAsync(criteria, Arg.Any<CancellationToken>()).Returns(resultDto);

		var controller = new ProductsController(productService);

		var result = await controller.Create(criteria, CancellationToken.None);

		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
		Assert.Same(resultDto, badRequestResult.Value);
	}

	[Fact]
	public async Task Update_WhenValid_ReturnsOk()
	{
		var productService = Substitute.For<IProductService>();
		var criteria = new UpdateProductCriteriaDto { Id = 99, Name = "Updated product", Price = 20m, Stock = 2, IsActive = true };
		var resultDto = ValidationResultDto.Success("Product updated.");
		productService.UpdateAsync(Arg.Any<UpdateProductCriteriaDto>(), Arg.Any<CancellationToken>()).Returns(resultDto);

		var controller = new ProductsController(productService);

		var result = await controller.Update(1, criteria, CancellationToken.None);

		var okResult = Assert.IsType<OkObjectResult>(result.Result);
		Assert.Same(resultDto, okResult.Value);
		await productService.Received(1).UpdateAsync(Arg.Is<UpdateProductCriteriaDto>(c => c.Id == 1), Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task Delete_WhenMissing_ReturnsNotFound()
	{
		var productService = Substitute.For<IProductService>();
		var resultDto = ValidationResultDto.Failure("Product with id '1' was not found.", "Product with id '1' was not found.");
		productService.DeleteAsync(1, Arg.Any<CancellationToken>()).Returns(resultDto);

		var controller = new ProductsController(productService);

		var result = await controller.Delete(1, CancellationToken.None);

		var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
		Assert.Same(resultDto, notFoundResult.Value);
	}

	[Fact]
	public async Task Delete_WhenSuccessful_ReturnsOk()
	{
		var productService = Substitute.For<IProductService>();
		var resultDto = ValidationResultDto.Success("Product deleted.");
		productService.DeleteAsync(1, Arg.Any<CancellationToken>()).Returns(resultDto);

		var controller = new ProductsController(productService);

		var result = await controller.Delete(1, CancellationToken.None);

		var okResult = Assert.IsType<OkObjectResult>(result.Result);
		Assert.Same(resultDto, okResult.Value);
	}
}
