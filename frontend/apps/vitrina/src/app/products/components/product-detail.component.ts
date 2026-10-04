import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Product } from '../interfaces/product.interface';

@Component({
  selector: 'app-product-detail',
  imports: [CommonModule, CurrencyPipe, DatePipe],
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.scss'
})
export class ProductDetailComponent {
  @Input() product: Product | null = null;
  @Input() loading = false;
  @Input() errorMessage: string | null = null;
  @Input() canManage = false;
  @Input() confirmDelete = false;
  @Input() deleting = false;

  @Output() back = new EventEmitter<void>();
  @Output() edit = new EventEmitter<void>();
  @Output() requestDelete = new EventEmitter<void>();
  @Output() cancelDelete = new EventEmitter<void>();
  @Output() confirmDeleteRequested = new EventEmitter<void>();
}
