import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs';
import { FlashMessageService } from '../../core/services/flash-message.service';
import { ProductsApiService } from '../../core/services/products-api.service';
import { SessionService } from '../../core/services/session.service';
import { extractValidationResult } from '../../core/utilities/extract-validation-result';
import { ProductDetailComponent } from '../components/product-detail/product-detail.component';
import { Product } from '../interfaces/product.interface';

@Component({
  selector: 'app-product-detail-page',
  imports: [ProductDetailComponent],
  templateUrl: './product-detail-page.component.html',
  styleUrl: './product-detail-page.component.scss'
})
export class ProductDetailPageComponent {
  private readonly activatedRoute = inject(ActivatedRoute);
  private readonly flashMessageService = inject(FlashMessageService);
  private readonly productsApiService = inject(ProductsApiService);
  private readonly router = inject(Router);
  private readonly sessionService = inject(SessionService);

  readonly confirmDelete = signal(false);
  readonly deleting = signal(false);
  readonly errorMessage = signal<string | null>(null);
  readonly loading = signal(false);
  readonly product = signal<Product | null>(null);

  constructor() {
    this.loadProduct();
  }

  get canManage(): boolean {
    return this.sessionService.hasRole('Admin');
  }

  goBack(): void {
    void this.router.navigate(['/products']);
  }

  editProduct(): void {
    const productId = this.product()?.id;
    if (!productId) {
      return;
    }

    void this.router.navigate(['/products', productId, 'edit']);
  }

  requestDelete(): void {
    this.confirmDelete.set(true);
  }

  cancelDelete(): void {
    this.confirmDelete.set(false);
  }

  deleteProduct(): void {
    const productId = this.product()?.id;
    if (!productId) {
      return;
    }

    this.deleting.set(true);
    this.errorMessage.set(null);

    this.productsApiService
      .delete(productId)
      .pipe(finalize(() => this.deleting.set(false)))
      .subscribe({
        next: (result) => {
          this.flashMessageService.show(result.message ?? 'Product deleted successfully.');
          void this.router.navigate(['/products']);
        },
        error: (error: unknown) => {
          const validation = extractValidationResult(error);
          this.errorMessage.set(
            validation?.errors[0] ?? validation?.message ?? 'Unable to delete the product.'
          );
        }
      });
  }

  private loadProduct(): void {
    const productId = Number(this.activatedRoute.snapshot.paramMap.get('id'));
    if (!Number.isInteger(productId) || productId <= 0) {
      this.errorMessage.set('The product identifier is invalid.');
      return;
    }

    this.loading.set(true);
    this.errorMessage.set(null);

    this.productsApiService
      .getById(productId)
      .pipe(finalize(() => this.loading.set(false)))
      .subscribe({
        next: (product) => this.product.set(product),
        error: (error: unknown) => {
          const validation = extractValidationResult(error);
          this.errorMessage.set(
            validation?.errors[0] ?? validation?.message ?? 'Unable to load the product.'
          );
        }
      });
  }
}
