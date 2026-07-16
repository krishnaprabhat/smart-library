import { Routes } from '@angular/router';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  {
    path: 'login',
    loadComponent: () => import('./components/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'student',
    canActivate: [authGuard],
    data: { role: 'Student' },
    children: [
      { path: '', redirectTo: 'catalog', pathMatch: 'full' },
      {
        path: 'catalog',
        loadComponent: () => import('./components/student/catalog/catalog.component').then(m => m.CatalogComponent)
      },
      {
        path: 'my-borrows',
        loadComponent: () => import('./components/student/my-borrows/my-borrows.component').then(m => m.MyBorrowsComponent)
      }
    ]
  },
  {
    path: 'admin',
    canActivate: [authGuard],
    data: { role: 'Admin' },
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      {
        path: 'dashboard',
        loadComponent: () => import('./components/admin/dashboard/admin-dashboard.component').then(m => m.AdminDashboardComponent)
      },
      {
        path: 'books',
        loadComponent: () => import('./components/admin/manage-books/manage-books.component').then(m => m.ManageBooksComponent)
      },
      {
        path: 'borrows',
        loadComponent: () => import('./components/admin/monitor-borrows/monitor-borrows.component').then(m => m.MonitorBorrowsComponent)
      },
      {
        path: 'users',
        loadComponent: () => import('./components/admin/manage-users/manage-users.component').then(m => m.ManageUsersComponent)
      }
    ]
  },
  { path: '**', redirectTo: '/login' }
];
