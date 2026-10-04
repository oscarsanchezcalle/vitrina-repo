import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ReactiveFormsModule, FormGroup } from '@angular/forms';
import { ValidationResult } from '../../../core/interfaces/validation-result.interface';

@Component({
  selector: 'app-login-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.scss'
})
export class LoginFormComponent {
  @Input({ required: true }) form!: FormGroup;
  @Input() isSubmitting = false;
  @Input() requestError: ValidationResult | null = null;
  @Output() submitted = new EventEmitter<void>();

  get usernameInvalid(): boolean {
    const control = this.form.get('username');
    return !!control && control.invalid && (control.dirty || control.touched);
  }

  get passwordInvalid(): boolean {
    const control = this.form.get('password');
    return !!control && control.invalid && (control.dirty || control.touched);
  }
}
