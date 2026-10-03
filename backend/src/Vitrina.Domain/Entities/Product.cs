namespace Vitrina.Domain.Entities;

public sealed class Product
{
	/// <summary>
	/// Gets or sets the unique identifier of the product.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the product name.
	/// </summary>
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the product description.
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Gets or sets the product price.
	/// </summary>
	public decimal Price { get; set; }

	/// <summary>
	/// Gets or sets the current stock quantity.
	/// </summary>
	public int Stock { get; set; }

	/// <summary>
	/// Gets or sets the product category.
	/// </summary>
	public string? Category { get; set; }

	/// <summary>
	/// Gets or sets the product image URL.
	/// </summary>
	public string? ImageUrl { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether the product is active.
	/// </summary>
	public bool IsActive { get; set; }

	/// <summary>
	/// Gets or sets the creation date and time.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the last update date and time.
	/// </summary>
	public DateTime? UpdatedAt { get; set; }
}
