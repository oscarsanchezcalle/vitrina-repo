import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ValidationResult } from '../../core/interfaces/validation-result.interface';

@Component({
  selector: 'app-product-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './product-form.component.html',
  styleUrl: './product-form.component.scss'
})
export class ProductFormComponent {
  @Input({ required: true }) form!: FormGroup;
  @Input() pageTitle = 'Product';
  @Input() submitLabel = 'Save';
  @Input() isSubmitting = false;
  @Input() requestError: ValidationResult | null = null;

  @Output() submitted = new EventEmitter<void>();
  @Output() cancelled = new EventEmitter<void>();

  fieldInvalid(fieldName: string): boolean {
    const control = this.form.get(fieldName);
    return !!control && control.invalid && (control.dirty || control.touched);
  }

  hasError(fieldName: string, errorName: string): boolean {
    return !!this.form.get(fieldName)?.hasError(errorName);
  }
}
