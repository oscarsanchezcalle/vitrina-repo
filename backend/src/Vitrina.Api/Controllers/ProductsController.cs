using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vitrina.Business.Services.Products;
using Vitrina.Domain.Enums;
using Vitrina.Dto.Common;
using Vitrina.Dto.Products;

namespace Vitrina.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class ProductsController(IProductService productService) : ControllerBase
{
	[HttpGet]
	[Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.User))]
	[ProducesResponseType(typeof(PagedResultDto<ProductDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ValidationResultDto), StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<PagedResultDto<ProductDto>>> Search([FromQuery] ProductSearchCriteriaDto criteria, CancellationToken cancellationToken)
	{
		var result = await productService.SearchAsync(criteria, cancellationToken);
		return Ok(result);
	}

	[HttpGet("{id:int}")]
	[Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.User))]
	[ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ValidationResultDto), StatusCodes.Status404NotFound)]
	public async Task<ActionResult<ProductDto>> GetById(int id, CancellationToken cancellationToken)
	{
		var product = await productService.GetByIdAsync(id, cancellationToken);
		return product is null ? NotFound(ValidationResultDto.Failure($"Product with id '{id}' was not found.", $"Product with id '{id}' was not found.")) : Ok(product);
	}

	[HttpPost]
	[Authorize(Roles = nameof(UserRole.Admin))]
	[ProducesResponseType(typeof(ValidationResultDto), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ValidationResultDto), StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<ValidationResultDto>> Create([FromBody] CreateProductCriteriaDto criteria, CancellationToken cancellationToken)
	{
		var result = await productService.CreateAsync(criteria, cancellationToken);
		return result.Succeeded ? Ok(result) : BadRequest(result);
	}

	[HttpPut("{id:int}")]
	[Authorize(Roles = nameof(UserRole.Admin))]
	[ProducesResponseType(typeof(ValidationResultDto), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ValidationResultDto), StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<ValidationResultDto>> Update(int id, [FromBody] UpdateProductCriteriaDto? criteria, CancellationToken cancellationToken)
	{
		var updateCriteria = criteria ?? new UpdateProductCriteriaDto { Id = id };
		if (updateCriteria.Id != id)
		{
			updateCriteria = updateCriteria with { Id = id };
		}

		var result = await productService.UpdateAsync(updateCriteria, cancellationToken);
		return result.Succeeded ? Ok(result) : BadRequest(result);
	}

	[HttpDelete("{id:int}")]
	[Authorize(Roles = nameof(UserRole.Admin))]
	[ProducesResponseType(typeof(ValidationResultDto), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ValidationResultDto), StatusCodes.Status404NotFound)]
	public async Task<ActionResult<ValidationResultDto>> Delete(int id, CancellationToken cancellationToken)
	{
		var result = await productService.DeleteAsync(id, cancellationToken);
		return result.Succeeded ? Ok(result) : result.Message is not null && result.Message.Contains("was not found") ? NotFound(result) : BadRequest(result);
	}
}
