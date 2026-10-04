export interface ValidationResult {
  succeeded: boolean;
  message: string | null;
  errors: string[];
}
