import { Component, inject, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs';
import { ValidationResult } from '../../core/interfaces/validation-result.interface';
import { FlashMessageService } from '../../core/services/flash-message.service';
import { ProductsApiService } from '../../core/services/products-api.service';
import { extractValidationResult } from '../../core/utilities/extract-validation-result';
import { ProductFormComponent } from '../components/product-form/product-form.component';
import { CreateProductRequest } from '../interfaces/create-product-request.interface';
import { Product } from '../interfaces/product.interface';
import { UpdateProductRequest } from '../interfaces/update-product-request.interface';

@Component({
  selector: 'app-product-editor-page',
  imports: [ReactiveFormsModule, ProductFormComponent],
  templateUrl: './product-editor-page.component.html',
  styleUrl: './product-editor-page.component.scss'
})
export class ProductEditorPageComponent {
  private readonly activatedRoute = inject(ActivatedRoute);
  private readonly flashMessageService = inject(FlashMessageService);
  private readonly productsApiService = inject(ProductsApiService);
  private readonly router = inject(Router);

  readonly isLoading = signal(false);
  readonly isSubmitting = signal(false);
  readonly loadError = signal<string | null>(null);
  readonly requestError = signal<ValidationResult | null>(null);
  readonly form = new FormGroup({
    name: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required, Validators.maxLength(255)]
    }),
    description: new FormControl('', {
      nonNullable: true,
      validators: [Validators.maxLength(2000)]
    }),
    price: new FormControl(0, {
      nonNullable: true,
      validators: [Validators.min(0)]
    }),
    stock: new FormControl(0, {
      nonNullable: true,
      validators: [Validators.min(0)]
    }),
    category: new FormControl('', {
      nonNullable: true,
      validators: [Validators.maxLength(100)]
    }),
    imageUrl: new FormControl('', {
      nonNullable: true,
      validators: [Validators.maxLength(500)]
    }),
    isActive: new FormControl(true, { nonNullable: true })
  });

  constructor() {
    if (this.isEditMode) {
      this.loadProduct();
    }
  }

  get isEditMode(): boolean {
    return this.productId !== null;
  }

  get pageTitle(): string {
    return this.isEditMode ? 'Edit product' : 'Create product';
  }

  get submitLabel(): string {
    return this.isEditMode ? 'Save changes' : 'Create product';
  }

  cancel(): void {
    if (this.productId) {
      void this.router.navigate(['/products', this.productId]);
      return;
    }

    void this.router.navigate(['/products']);
  }

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting.set(true);
    this.requestError.set(null);

    const request = this.buildRequest();
    const saveRequest = this.isEditMode
      ? this.productsApiService.update(request as UpdateProductRequest)
      : this.productsApiService.create(request as CreateProductRequest);

    saveRequest.pipe(finalize(() => this.isSubmitting.set(false))).subscribe({
      next: (result) => {
        this.flashMessageService.show(
          result.message ??
            (this.isEditMode
              ? 'Product updated successfully.'
              : 'Product created successfully.')
        );
        void this.router.navigate(['/products']);
      },
      error: (error: unknown) => {
        this.requestError.set(
          extractValidationResult(error) ?? {
            succeeded: false,
            message: 'Unable to save the product.',
            errors: ['Try again in a few moments.']
          }
        );
      }
    });
  }

  private get productId(): number | null {
    const value = Number(this.activatedRoute.snapshot.paramMap.get('id'));
    return Number.isInteger(value) && value > 0 ? value : null;
  }

  private loadProduct(): void {
    if (!this.productId) {
      this.loadError.set('The product identifier is invalid.');
      return;
    }

    this.isLoading.set(true);
    this.loadError.set(null);

    this.productsApiService
      .getById(this.productId)
      .pipe(finalize(() => this.isLoading.set(false)))
      .subscribe({
        next: (product) => this.patchForm(product),
        error: (error: unknown) => {
          const validation = extractValidationResult(error);
          this.loadError.set(
            validation?.errors[0] ?? validation?.message ?? 'Unable to load the product.'
          );
        }
      });
  }

  private patchForm(product: Product): void {
    this.form.reset({
      name: product.name,
      description: product.description ?? '',
      price: product.price,
      stock: product.stock,
      category: product.category ?? '',
      imageUrl: product.imageUrl ?? '',
      isActive: product.isActive
    });
  }

  private buildRequest(): CreateProductRequest | UpdateProductRequest {
    const rawValue = this.form.getRawValue();
    const request = {
      name: rawValue.name.trim(),
      description: rawValue.description.trim() || null,
      price: rawValue.price,
      stock: rawValue.stock,
      category: rawValue.category.trim() || null,
      imageUrl: rawValue.imageUrl.trim() || null,
      isActive: rawValue.isActive
    };

    return this.isEditMode
      ? {
          id: this.productId as number,
          ...request
        }
      : request;
  }
}
