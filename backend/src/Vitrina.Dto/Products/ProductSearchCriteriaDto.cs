namespace Vitrina.Dto.Products;

/// <summary>
/// Represents the criteria used to search products.
/// </summary>
public sealed record ProductSearchCriteriaDto
{
	/// <summary>
	/// Gets or sets the search term used to filter products.
	/// </summary>
	public string? SearchTerm { get; init; }

	/// <summary>
	/// Gets or sets whether to filter by active products.
	/// </summary>
	public bool? IsActive { get; init; }

	/// <summary>
	/// Gets or sets the page number.
	/// </summary>
	public int PageNumber { get; init; } = 1;

	/// <summary>
	/// Gets or sets the page size.
	/// </summary>
	public int PageSize { get; init; } = 10;
}
