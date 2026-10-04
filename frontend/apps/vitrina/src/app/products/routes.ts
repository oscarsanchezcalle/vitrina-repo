import { Routes } from '@angular/router';
import { adminGuard } from '../core/guards/admin.guard';
import { authGuard } from '../core/guards/auth.guard';

export const PRODUCTS_ROUTES: Routes = [
  {
    path: '',
    canActivate: [authGuard],
    children: [
      {
        path: '',
        loadComponent: () =>
          import('./container/products-page.component').then(
            (component) => component.ProductsPageComponent
          )
      },
      {
        path: 'new',
        canActivate: [adminGuard],
        loadComponent: () =>
          import('./container/product-editor-page.component').then(
            (component) => component.ProductEditorPageComponent
          )
      },
      {
        path: ':id',
        loadComponent: () =>
          import('./container/product-detail-page.component').then(
            (component) => component.ProductDetailPageComponent
          )
      },
      {
        path: ':id/edit',
        canActivate: [adminGuard],
        loadComponent: () =>
          import('./container/product-editor-page.component').then(
            (component) => component.ProductEditorPageComponent
          )
      }
    ]
  }
];
