import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PagedResult } from '../interfaces/paged-result.interface';
import { ValidationResult } from '../interfaces/validation-result.interface';
import { API_BASE_URL } from '../tokens/api-base-url.token';
import { CreateProductRequest } from '../../products/interfaces/create-product-request.interface';
import { Product } from '../../products/interfaces/product.interface';
import { ProductSearchCriteria } from '../../products/interfaces/product-search-criteria.interface';
import { UpdateProductRequest } from '../../products/interfaces/update-product-request.interface';

@Injectable({ providedIn: 'root' })
export class ProductsApiService {
  private readonly httpClient = inject(HttpClient);
  private readonly apiBaseUrl = inject(API_BASE_URL);

  search(criteria: ProductSearchCriteria): Observable<PagedResult<Product>> {
    let params = new HttpParams()
      .set('PageNumber', criteria.pageNumber.toString())
      .set('PageSize', criteria.pageSize.toString());

    if (criteria.searchTerm?.trim()) {
      params = params.set('SearchTerm', criteria.searchTerm.trim());
    }

    if (criteria.isActive !== null && criteria.isActive !== undefined) {
      params = params.set('IsActive', criteria.isActive.toString());
    }

    return this.httpClient.get<PagedResult<Product>>(`${this.apiBaseUrl}/Products`, {
      params
    });
  }

  getById(id: number): Observable<Product> {
    return this.httpClient.get<Product>(`${this.apiBaseUrl}/Products/${id}`);
  }

  create(request: CreateProductRequest): Observable<ValidationResult> {
    return this.httpClient.post<ValidationResult>(`${this.apiBaseUrl}/Products`, request);
  }

  update(request: UpdateProductRequest): Observable<ValidationResult> {
    return this.httpClient.put<ValidationResult>(
      `${this.apiBaseUrl}/Products/${request.id}`,
      request
    );
  }

  delete(id: number): Observable<ValidationResult> {
    return this.httpClient.delete<ValidationResult>(`${this.apiBaseUrl}/Products/${id}`);
  }
}
