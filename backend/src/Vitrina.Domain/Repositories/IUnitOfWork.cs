namespace Vitrina.Domain.Repositories;

/// <summary>
/// Defines a unit of work for committing repository changes.
/// </summary>
public interface IUnitOfWork
{
	/// <summary>
	/// Persists pending changes.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The number of affected rows.</returns>
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
