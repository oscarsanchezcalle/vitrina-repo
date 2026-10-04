import { Routes } from '@angular/router';

export const appRoutes: Routes = [
  {
    path: 'auth',
    loadChildren: () => import('./auth/routes').then((routes) => routes.AUTH_ROUTES)
  },
  {
    path: 'products',
    loadChildren: () =>
      import('./products/routes').then((routes) => routes.PRODUCTS_ROUTES)
  },
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'products'
  },
  {
    path: '**',
    redirectTo: 'products'
  }
];
