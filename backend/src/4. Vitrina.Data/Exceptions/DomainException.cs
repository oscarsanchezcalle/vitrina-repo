namespace Vitrina.Data.Exceptions;

/// <summary>
/// Represents the base exception for data-layer domain failures.
/// </summary>
public abstract class DomainException(string message) : Exception(message);
