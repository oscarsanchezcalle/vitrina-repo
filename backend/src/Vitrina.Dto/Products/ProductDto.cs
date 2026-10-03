namespace Vitrina.Dto.Products;

/// <summary>
/// Represents a product returned by the application layer.
/// </summary>
public sealed record ProductDto
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
	/// Gets or sets the stock quantity.
	/// </summary>
	public int Stock { get; init; }

	/// <summary>
	/// Gets or sets the category.
	/// </summary>
	public string? Category { get; init; }

	/// <summary>
	/// Gets or sets the image URL.
	/// </summary>
	public string? ImageUrl { get; init; }

	/// <summary>
	/// Gets or sets a value indicating whether the product is active.
	/// </summary>
	public bool IsActive { get; init; }

	/// <summary>
	/// Gets or sets the creation date.
	/// </summary>
	public DateTime CreatedAt { get; init; }

	/// <summary>
	/// Gets or sets the last update date.
	/// </summary>
	public DateTime? UpdatedAt { get; init; }
}
