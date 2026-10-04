import { HttpErrorResponse } from '@angular/common/http';
import { ValidationResult } from '../interfaces/validation-result.interface';

export function extractValidationResult(error: unknown): ValidationResult | null {
  if (!(error instanceof HttpErrorResponse)) {
    return null;
  }

  const payload = error.error as Partial<ValidationResult> | null;
  if (!payload || typeof payload !== 'object') {
    return null;
  }

  const errors = Array.isArray(payload.errors)
    ? payload.errors.filter((entry): entry is string => typeof entry === 'string')
    : [];

  return {
    succeeded: payload.succeeded ?? false,
    message: typeof payload.message === 'string' ? payload.message : null,
    errors
  };
}
