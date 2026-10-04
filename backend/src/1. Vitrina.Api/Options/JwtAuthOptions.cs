namespace Vitrina.Api.Options;

/// <summary>
/// Represents JWT configuration settings.
/// </summary>
public sealed class JwtAuthOptions
{
	public const string SectionName = "Jwt";

	/// <summary>
	/// Gets or sets the issuer.
	/// </summary>
	public string Issuer { get; init; } = string.Empty;

	/// <summary>
	/// Gets or sets the audience.
	/// </summary>
	public string Audience { get; init; } = string.Empty;

	/// <summary>
	/// Gets or sets the signing key.
	/// </summary>
	public string Key { get; init; } = string.Empty;

	/// <summary>
	/// Gets or sets the token expiration in minutes.
	/// </summary>
	public int ExpirationMinutes { get; init; } = 60;
}
