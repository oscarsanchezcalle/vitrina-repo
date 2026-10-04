namespace Vitrina.Dto.Products;

/// <summary>
/// Represents the data required to update a product.
/// </summary>
public sealed record UpdateProductCriteriaDto
{
	/// <summary>
	/// Gets or sets the product identifier.
	/// </summary>
	public int Id { get; init; }

	/// <summary>
	/// Gets or sets the product name.
	/// </summary>
	public string Name { get; init; } = string.Empty;

	/// <summary>
	/// Gets or sets the product description.
	/// </summary>
	public string? Description { get; init; }

	/// <summary>
	/// Gets or sets the product price.
	/// </summary>
	public decimal Price { get; init; }

	/// <summary>
	/// Gets or sets the available stock quantity.
	/// </summary>
	public int Stock { get; init; }

	/// <summary>
	/// Gets or sets the product category.
	/// </summary>
	public string? Category { get; init; }

	/// <summary>
	/// Gets or sets the product image URL.
	/// </summary>
	public string? ImageUrl { get; init; }

	/// <summary>
	/// Gets or sets a value indicating whether the product is active.
	/// </summary>
	public bool IsActive { get; init; }
}
