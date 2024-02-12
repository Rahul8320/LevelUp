import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../../shared/services/user.service';

export const authGuard: CanActivateFn = (route, state) => {
  if (inject(UserService).isLoggedIn()) {
    return true;
  } else {
    inject(Router).navigate(['/login']);
    return false;
  }
};
