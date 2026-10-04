import {
  HttpTestingController,
  provideHttpClientTesting
} from '@angular/common/http/testing';
import { provideHttpClient } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { APP_ENVIRONMENT } from '../tokens/app-environment.token';
import { ProductsApiService } from './products-api.service';

describe('ProductsApiService', () => {
  let httpTestingController: HttpTestingController;
  let service: ProductsApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        ProductsApiService,
        provideHttpClient(),
        provideHttpClientTesting(),
        {
          provide: APP_ENVIRONMENT,
          useValue: {
            production: false,
            apiBaseUrl: '/api'
          }
        }
      ]
    });

    httpTestingController = TestBed.inject(HttpTestingController);
    service = TestBed.inject(ProductsApiService);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('builds the search request with the backend query parameters', () => {
    service
      .search({
        searchTerm: 'audio',
        isActive: true,
        pageNumber: 2,
        pageSize: 10
      })
      .subscribe();

    const request = httpTestingController.expectOne(
      (candidate: { url: string; params: { get(name: string): string | null } }) =>
        candidate.url === '/api/Products' &&
        candidate.params.get('SearchTerm') === 'audio' &&
        candidate.params.get('IsActive') === 'true' &&
        candidate.params.get('PageNumber') === '2' &&
        candidate.params.get('PageSize') === '10'
    );

    expect(request.request.method).toBe('GET');
    request.flush({
      items: [],
      totalCount: 0,
      pageNumber: 2,
      pageSize: 10
    });
  });

  it('uses the expected endpoints for create, update, and delete', () => {
    service
      .create({
        name: 'Keyboard',
        description: 'Mechanical keyboard',
        price: 120,
        stock: 5,
        category: 'Computing',
        imageUrl: null,
        isActive: true
      })
      .subscribe();
    service
      .update({
        id: 7,
        name: 'Keyboard',
        description: 'Updated description',
        price: 125,
        stock: 6,
        category: 'Computing',
        imageUrl: null,
        isActive: true
      })
      .subscribe();
    service.delete(7).subscribe();

    const createRequest = httpTestingController.expectOne('/api/Products');
    expect(createRequest.request.method).toBe('POST');
    createRequest.flush({ succeeded: true, message: 'Product created successfully.', errors: [] });

    const updateRequest = httpTestingController.expectOne('/api/Products/7');
    expect(updateRequest.request.method).toBe('PUT');
    updateRequest.flush({ succeeded: true, message: 'Product updated successfully.', errors: [] });

    const deleteRequest = httpTestingController.expectOne('/api/Products/7');
    expect(deleteRequest.request.method).toBe('DELETE');
    deleteRequest.flush({ succeeded: true, message: 'Product deleted successfully.', errors: [] });
  });
});
