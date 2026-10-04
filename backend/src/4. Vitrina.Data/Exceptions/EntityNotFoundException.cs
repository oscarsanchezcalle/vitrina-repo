namespace Vitrina.Data.Exceptions;

/// <summary>
/// Represents a missing entity failure.
/// </summary>
public sealed class EntityNotFoundException(string entityName, object key)
	: DomainException($"{entityName} with key '{key}' was not found.");
