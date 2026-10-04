import { Routes } from '@angular/router';
import { guestGuard } from '../core/guards/guest.guard';

export const AUTH_ROUTES: Routes = [
  {
    path: 'login',
    canActivate: [guestGuard],
    loadComponent: () =>
      import('./container/login-page.component').then(
        (component) => component.LoginPageComponent
      )
  },
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'login'
  }
];
