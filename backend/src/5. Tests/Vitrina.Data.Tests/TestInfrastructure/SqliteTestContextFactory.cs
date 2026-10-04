using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Vitrina.Data.Context;

namespace Vitrina.Data.Tests.TestInfrastructure;

/// <summary>
/// Creates an in-memory SQLite context for fast repository tests.
/// </summary>
public static class SqliteTestContextFactory
{
	/// <summary>
	/// Creates a fresh SQLite connection and DbContext for each test.
	/// </summary>
	/// <returns>The open connection and initialized context.</returns>
	public static async Task<(SqliteConnection Connection, VitrinaDbContext Context)> CreateAsync()
	{
		var connection = new SqliteConnection("DataSource=:memory:");
		await connection.OpenAsync();

		var options = new DbContextOptionsBuilder<VitrinaDbContext>()
			.UseSqlite(connection)
			.Options;

		var context = new VitrinaDbContext(options);
		await context.Database.EnsureCreatedAsync();

		return (connection, context);
	}
}
