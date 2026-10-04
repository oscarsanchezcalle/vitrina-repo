namespace Vitrina.Data.Common;

/// <summary>
/// Represents a paged result.
/// </summary>
/// <typeparam name="T">The item type.</typeparam>
public sealed record PagedResult<T>(IReadOnlyList<T> Items, int TotalCount, int PageNumber, int PageSize)
{
	/// <summary>
	/// Creates an empty paged result.
	/// </summary>
	/// <param name="pageNumber">The page number.</param>
	/// <param name="pageSize">The page size.</param>
	/// <returns>An empty paged result.</returns>
	public static PagedResult<T> Empty(int pageNumber, int pageSize) => new(Array.Empty<T>(), 0, pageNumber, pageSize);
}
