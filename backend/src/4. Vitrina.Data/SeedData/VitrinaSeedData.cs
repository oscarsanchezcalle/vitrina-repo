using System.Text.Json;
using Vitrina.Dto.Common;

namespace Vitrina.Data.SeedData;

/// <summary>
/// Represents the external seed data for the application.
/// </summary>
public sealed record VitrinaSeedData(IReadOnlyList<ProductSeedData> Products, IReadOnlyList<UserSeedData> Users)
{
	private static readonly Lazy<VitrinaSeedData> CachedSeedData = new(LoadInternal);

	/// <summary>
	/// Gets the cached seed data.
	/// </summary>
	public static VitrinaSeedData Current => CachedSeedData.Value;

	/// <summary>
	/// Loads the seed data from the JSON file on disk.
	/// </summary>
	/// <returns>The loaded seed data.</returns>
	/// <exception cref="FileNotFoundException">Thrown when the seed file cannot be found.</exception>
	/// <exception cref="InvalidOperationException">Thrown when the seed file cannot be deserialized.</exception>
	private static VitrinaSeedData LoadInternal()
	{
		var filePath = Path.Combine(AppContext.BaseDirectory, "SeedData", "vitrina.seed.json");
		if (!File.Exists(filePath))
		{
			throw new FileNotFoundException($"Seed data file was not found at '{filePath}'.");
		}

		var json = File.ReadAllText(filePath);
		var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
		var seedData = JsonSerializer.Deserialize<VitrinaSeedData>(json, options);

		return seedData ?? throw new InvalidOperationException("Seed data file could not be deserialized.");
	}
}

/// <summary>
/// Represents a seeded product definition.
/// </summary>
public sealed record ProductSeedData
{
	/// <summary>
	/// Gets or sets the product identifier.
	/// </summary>
	public int Id { get; init; }

	/// <summary>
	/// Gets or sets the product name.
	/// </summary>
	public string Name { get; init; } = string.Empty;

	/// <summary>
	/// Gets or sets the product description.
	/// </summary>
	public string? Description { get; init; }

	/// <summary>
	/// Gets or sets the product price.
	/// </summary>
	public decimal Price { get; init; }

	/// <summary>
	/// Gets or sets the stock quantity.
	/// </summary>
	public int Stock { get; init; }

	/// <summary>
	/// Gets or sets the category name.
	/// </summary>
	public string? Category { get; init; }

	/// <summary>
	/// Gets or sets the image URL.
	/// </summary>
	public string? ImageUrl { get; init; }

	/// <summary>
	/// Gets or sets a value indicating whether the product is active.
	/// </summary>
	public bool IsActive { get; init; }

	/// <summary>
	/// Gets or sets the creation date.
	/// </summary>
	public DateTime CreatedAt { get; init; }

	/// <summary>
	/// Gets or sets the last update date.
	/// </summary>
	public DateTime? UpdatedAt { get; init; }
}

/// <summary>
/// Represents a seeded user definition.
/// </summary>
public sealed record UserSeedData
{
	/// <summary>
	/// Gets or sets the user identifier.
	/// </summary>
	public int Id { get; init; }

	/// <summary>
	/// Gets or sets the user's display name.
	/// </summary>
	public string Name { get; init; } = string.Empty;

	/// <summary>
	/// Gets or sets the user's email.
	/// </summary>
	public string Email { get; init; } = string.Empty;

	/// <summary>
	/// Gets or sets the username.
	/// </summary>
	public string Username { get; init; } = string.Empty;

	/// <summary>
	/// Gets or sets the password hash.
	/// </summary>
	public string PasswordHash { get; init; } = string.Empty;

	/// <summary>
	/// Gets or sets the user role.
	/// </summary>
	public UserRoleDto Role { get; init; }

	/// <summary>
	/// Gets or sets the creation date.
	/// </summary>
	public DateTime CreatedAt { get; init; }
}
