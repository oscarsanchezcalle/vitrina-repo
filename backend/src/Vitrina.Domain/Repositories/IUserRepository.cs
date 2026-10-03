using Vitrina.Domain.Entities;

namespace Vitrina.Domain.Repositories;

public interface IUserRepository
{
	Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

	Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

	Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);

	Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken = default);

	Task AddAsync(User user, CancellationToken cancellationToken = default);

	void Update(User user);

	void Remove(User user);
}
