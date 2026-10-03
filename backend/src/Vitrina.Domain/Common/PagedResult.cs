namespace Vitrina.Domain.Common;

public sealed record PagedResult<T>(IReadOnlyList<T> Items, int TotalCount, int PageNumber, int PageSize)
{
	public static PagedResult<T> Empty(int pageNumber, int pageSize) => new(Array.Empty<T>(), 0, pageNumber, pageSize);
}
