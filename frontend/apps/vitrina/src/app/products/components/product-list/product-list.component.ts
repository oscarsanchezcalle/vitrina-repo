import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Product } from '../../interfaces/product.interface';

@Component({
  selector: 'app-product-list',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss'
})
export class ProductListComponent {
  @Input({ required: true }) filterForm!: FormGroup;
  @Input() products: Product[] = [];
  @Input() totalCount = 0;
  @Input() pageNumber = 1;
  @Input() pageSize = 10;
  @Input() loading = false;
  @Input() errorMessage: string | null = null;
  @Input() successMessage: string | null = null;
  @Input() canManage = false;
  @Input() pendingDeleteId: number | null = null;
  @Input() deletingId: number | null = null;

  @Output() filterSubmitted = new EventEmitter<void>();
  @Output() filtersReset = new EventEmitter<void>();
  @Output() pageChanged = new EventEmitter<number>();
  @Output() view = new EventEmitter<number>();
  @Output() create = new EventEmitter<void>();
  @Output() edit = new EventEmitter<number>();
  @Output() requestDelete = new EventEmitter<number>();
  @Output() cancelDelete = new EventEmitter<void>();
  @Output() confirmDelete = new EventEmitter<number>();

  get totalPages(): number {
    return Math.max(1, Math.ceil(this.totalCount / this.pageSize));
  }

  get hasResults(): boolean {
    return this.products.length > 0;
  }
}
