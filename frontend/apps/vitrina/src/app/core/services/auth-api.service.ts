import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthSession } from '../interfaces/auth-session.interface';
import { API_BASE_URL } from '../tokens/api-base-url.token';
import { LoginRequest } from '../../auth/interfaces/login-request.interface';

@Injectable({ providedIn: 'root' })
export class AuthApiService {
  private readonly httpClient = inject(HttpClient);
  private readonly apiBaseUrl = inject(API_BASE_URL);

  login(request: LoginRequest): Observable<AuthSession> {
    return this.httpClient.post<AuthSession>(`${this.apiBaseUrl}/Auth/login`, request);
  }
}
