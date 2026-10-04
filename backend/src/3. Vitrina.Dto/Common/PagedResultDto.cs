namespace Vitrina.Dto.Common;

/// <summary>
/// Represents a paged result returned by application services.
/// </summary>
/// <typeparam name="T">The item type.</typeparam>
public sealed record PagedResultDto<T>
{
	/// <summary>
	/// Gets or sets the page items.
	/// </summary>
	public IReadOnlyList<T> Items { get; init; } = Array.Empty<T>();

	/// <summary>
	/// Gets or sets the total number of items.
	/// </summary>
	public int TotalCount { get; init; }

	/// <summary>
	/// Gets or sets the current page number.
	/// </summary>
	public int PageNumber { get; init; }

	/// <summary>
	/// Gets or sets the current page size.
	/// </summary>
	public int PageSize { get; init; }
}
