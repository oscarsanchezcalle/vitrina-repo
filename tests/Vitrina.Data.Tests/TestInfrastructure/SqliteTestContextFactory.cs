using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Vitrina.Data.Context;

namespace Vitrina.Data.Tests.TestInfrastructure;

public static class SqliteTestContextFactory
{
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
