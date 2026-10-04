import {
  HttpTestingController,
  provideHttpClientTesting
} from '@angular/common/http/testing';
import { provideHttpClient } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { AuthApiService } from './auth-api.service';
import { APP_ENVIRONMENT } from '../tokens/app-environment.token';

describe('AuthApiService', () => {
  let httpTestingController: HttpTestingController;
  let service: AuthApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        AuthApiService,
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
    service = TestBed.inject(AuthApiService);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('posts credentials to the login endpoint', () => {
    const requestBody = {
      username: 'admin',
      password: 'Admin123!'
    };

    service.login(requestBody).subscribe();

    const request = httpTestingController.expectOne('/api/Auth/login');
    expect(request.request.method).toBe('POST');
    expect(request.request.body).toEqual(requestBody);
    request.flush({
      accessToken: 'token',
      expiresAt: '2026-10-05T00:00:00Z',
      name: 'Admin User',
      username: 'admin',
      role: 'Admin'
    });
  });
});
