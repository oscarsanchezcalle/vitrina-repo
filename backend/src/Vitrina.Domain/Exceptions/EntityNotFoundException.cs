namespace Vitrina.Domain.Exceptions;

public sealed class EntityNotFoundException(string entityName, object key)
	: DomainException($"{entityName} with key '{key}' was not found.");
