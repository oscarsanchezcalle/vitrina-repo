using Microsoft.EntityFrameworkCore;
using Vitrina.Data.Context;
using Vitrina.Domain.Entities;
using Vitrina.Domain.Repositories;

namespace Vitrina.Data.Repositories;

public sealed class UserRepository(VitrinaDbContext context) : IUserRepository
{
	public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
	{
		return await context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
	}

	public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
	{
		return await context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
	}

	public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
	{
		return await context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Username == username, cancellationToken);
	}

	public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		return await context.Users.AsNoTracking().OrderBy(user => user.Id).ToListAsync(cancellationToken);
	}

	public async Task AddAsync(User user, CancellationToken cancellationToken = default)
	{
		await context.Users.AddAsync(user, cancellationToken);
	}

	public void Update(User user)
	{
		context.Users.Update(user);
	}

	public void Remove(User user)
	{
		context.Users.Remove(user);
	}
}
