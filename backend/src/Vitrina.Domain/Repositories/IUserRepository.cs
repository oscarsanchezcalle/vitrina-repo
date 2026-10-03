using Vitrina.Domain.Entities;

namespace Vitrina.Domain.Repositories;

/// <summary>
/// Defines user persistence operations.
/// </summary>
public interface IUserRepository
{
	/// <summary>
	/// Gets a user by its identifier.
	/// </summary>
	/// <param name="id">The user identifier.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The matching user or null.</returns>
	Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets a user by email.
	/// </summary>
	/// <param name="email">The email address.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The matching user or null.</returns>
	Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets a user by username.
	/// </summary>
	/// <param name="username">The username.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The matching user or null.</returns>
	Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets all users.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The list of users.</returns>
	Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Adds a user to the repository.
	/// </summary>
	/// <param name="user">The user to add.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	Task AddAsync(User user, CancellationToken cancellationToken = default);

	/// <summary>
	/// Marks a user as updated in the current context.
	/// </summary>
	/// <param name="user">The user to update.</param>
	void Update(User user);

	/// <summary>
	/// Marks a user as removed in the current context.
	/// </summary>
	/// <param name="user">The user to remove.</param>
	void Remove(User user);
}
