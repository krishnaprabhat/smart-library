import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = (route) => {
  const auth = inject(AuthService);
  const router = inject(Router);

  if (!auth.isLoggedIn()) {
    router.navigate(['/login']);
    return false;
  }

  const requiredRole = route.data?.['role'];
  const userRole = auth.getRole();

  if (requiredRole) {
    // 'Admin' route is also accessible by 'Librarian'
    const allowed = userRole === requiredRole ||
      (requiredRole === 'Admin' && userRole === 'Librarian');
    if (!allowed) {
      if (userRole === 'Admin' || userRole === 'Librarian') {
        router.navigate(['/admin/dashboard']);
      } else {
        router.navigate(['/student/catalog']);
      }
      return false;
    }
  }

  return true;
};
