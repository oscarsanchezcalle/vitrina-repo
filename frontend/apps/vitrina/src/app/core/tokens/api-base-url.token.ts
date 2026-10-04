import { inject, InjectionToken } from '@angular/core';
import { APP_ENVIRONMENT } from './app-environment.token';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL', {
  factory: () => inject(APP_ENVIRONMENT).apiBaseUrl
});
