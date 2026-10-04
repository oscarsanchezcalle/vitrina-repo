namespace Vitrina.Dto.Common;

/// <summary>
/// Represents the standardized result returned for validation and application errors.
/// </summary>
public sealed record ValidationResultDto
{
	/// <summary>
	/// Gets or sets a value indicating whether the operation succeeded.
	/// </summary>
	public bool Succeeded { get; init; }

	/// <summary>
	/// Gets or sets the main message associated with the result.
	/// </summary>
	public string? Message { get; init; }

	/// <summary>
	/// Gets or sets the collection of validation or error messages.
	/// </summary>
	public IReadOnlyList<string> Errors { get; init; } = Array.Empty<string>();

	/// <summary>
	/// Creates a successful validation result.
	/// </summary>
	/// <param name="message">Optional success message.</param>
	/// <returns>A successful <see cref="ValidationResultDto"/> instance.</returns>
	public static ValidationResultDto Success(string? message = null) => new()
	{
		Succeeded = true,
		Message = message,
		Errors = Array.Empty<string>()
	};

	/// <summary>
	/// Creates a failed validation result.
	/// </summary>
	/// <param name="message">Optional error summary message.</param>
	/// <param name="errors">The validation or error messages.</param>
	/// <returns>A failed <see cref="ValidationResultDto"/> instance.</returns>
	public static ValidationResultDto Failure(string? message, params string[] errors) => new()
	{
		Succeeded = false,
		Message = message,
		Errors = errors.Length == 0 ? Array.Empty<string>() : errors
	};
}
