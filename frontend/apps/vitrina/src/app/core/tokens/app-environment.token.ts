import { InjectionToken } from '@angular/core';
import { AppEnvironment } from '../interfaces/app-environment.interface';

export const APP_ENVIRONMENT = new InjectionToken<AppEnvironment>('APP_ENVIRONMENT');
