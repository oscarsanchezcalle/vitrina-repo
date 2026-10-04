import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { AuthApiService } from '../../core/services/auth-api.service';
import { ValidationResult } from '../../core/interfaces/validation-result.interface';
import { SessionService } from '../../core/services/session.service';
import { extractValidationResult } from '../../core/utilities/extract-validation-result';
import { LoginFormComponent } from '../components/login/login-form.component';

@Component({
  selector: 'app-login-page',
  imports: [CommonModule, ReactiveFormsModule, LoginFormComponent],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.scss'
})
export class LoginPageComponent {
  private readonly authApiService = inject(AuthApiService);
  private readonly router = inject(Router);
  private readonly sessionService = inject(SessionService);

  readonly isSubmitting = signal(false);
  readonly requestError = signal<ValidationResult | null>(null);
  readonly form = new FormGroup({
    username: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required]
    }),
    password: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required]
    })
  });

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting.set(true);
    this.requestError.set(null);

    this.authApiService
      .login(this.form.getRawValue())
      .pipe(finalize(() => this.isSubmitting.set(false)))
      .subscribe({
        next: (session) => {
          this.sessionService.setSession(session);
          void this.router.navigateByUrl('/products');
        },
        error: (error: unknown) => {
          this.requestError.set(
            extractValidationResult(error) ?? {
              succeeded: false,
              message: 'Authentication failed.',
              errors: ['Unable to sign in right now.']
            }
          );
        }
      });
  }
}
