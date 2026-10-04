import { Component, inject, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { PagedResult } from '../../core/interfaces/paged-result.interface';
import { FlashMessageService } from '../../core/services/flash-message.service';
import { ProductsApiService } from '../../core/services/products-api.service';
import { SessionService } from '../../core/services/session.service';
import { extractValidationResult } from '../../core/utilities/extract-validation-result';
import { ProductListComponent } from '../components/product-list.component';
import { Product } from '../interfaces/product.interface';
import { ProductSearchCriteria } from '../interfaces/product-search-criteria.interface';

@Component({
  selector: 'app-products-page',
  imports: [ReactiveFormsModule, ProductListComponent],
  templateUrl: './products-page.component.html',
  styleUrl: './products-page.component.scss'
})
export class ProductsPageComponent {
  private readonly flashMessageService = inject(FlashMessageService);
  private readonly productsApiService = inject(ProductsApiService);
  private readonly router = inject(Router);
  private readonly sessionService = inject(SessionService);

  readonly deletingId = signal<number | null>(null);
  readonly errorMessage = signal<string | null>(null);
  readonly loading = signal(false);
  readonly pageNumber = signal(1);
  readonly pageSize = signal(10);
  readonly pendingDeleteId = signal<number | null>(null);
  readonly result = signal<PagedResult<Product>>({
    items: [],
    totalCount: 0,
    pageNumber: 1,
    pageSize: 10
  });
  readonly filterForm = new FormGroup({
    searchTerm: new FormControl('', { nonNullable: true }),
    isActive: new FormControl('all', { nonNullable: true })
  });

  constructor() {
    this.loadProducts();
  }

  get canManage(): boolean {
    return this.sessionService.hasRole('Admin');
  }

  get successMessage(): string | null {
    return this.flashMessageService.message();
  }

  submitFilters(): void {
    this.pageNumber.set(1);
    this.flashMessageService.clear();
    this.loadProducts();
  }

  resetFilters(): void {
    this.filterForm.reset({
      searchTerm: '',
      isActive: 'all'
    });
    this.pageNumber.set(1);
    this.flashMessageService.clear();
    this.loadProducts();
  }

  changePage(page: number): void {
    this.pageNumber.set(page);
    this.loadProducts();
  }

  viewProduct(productId: number): void {
    void this.router.navigate(['/products', productId]);
  }

  createProduct(): void {
    void this.router.navigate(['/products/new']);
  }

  editProduct(productId: number): void {
    void this.router.navigate(['/products', productId, 'edit']);
  }

  requestDelete(productId: number): void {
    this.pendingDeleteId.set(productId);
  }

  cancelDelete(): void {
    this.pendingDeleteId.set(null);
  }

  confirmDelete(productId: number): void {
    this.deletingId.set(productId);
    this.errorMessage.set(null);

    this.productsApiService
      .delete(productId)
      .pipe(finalize(() => this.deletingId.set(null)))
      .subscribe({
        next: (result) => {
          if (this.result().items.length === 1 && this.pageNumber() > 1) {
            this.pageNumber.update((pageNumber) => pageNumber - 1);
          }

          this.pendingDeleteId.set(null);
          this.flashMessageService.show(result.message ?? 'Product deleted successfully.');
          this.loadProducts();
        },
        error: (error: unknown) => {
          const validation = extractValidationResult(error);
          this.errorMessage.set(
            validation?.errors[0] ?? validation?.message ?? 'Unable to delete the product.'
          );
        }
      });
  }

  private loadProducts(): void {
    this.loading.set(true);
    this.errorMessage.set(null);

    this.productsApiService
      .search(this.buildCriteria())
      .pipe(finalize(() => this.loading.set(false)))
      .subscribe({
        next: (result) => this.result.set(result),
        error: (error: unknown) => {
          const validation = extractValidationResult(error);
          this.errorMessage.set(
            validation?.errors[0] ?? validation?.message ?? 'Unable to load products.'
          );
        }
      });
  }

  private buildCriteria(): ProductSearchCriteria {
    const rawValue = this.filterForm.getRawValue();

    return {
      searchTerm: rawValue.searchTerm.trim() || null,
      isActive:
        rawValue.isActive === 'all'
          ? null
          : rawValue.isActive === 'true',
      pageNumber: this.pageNumber(),
      pageSize: this.pageSize()
    };
  }
}
