import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../../shared/services/user.service';

export const adminGuard: CanActivateFn = (route, state) => {
  if (inject(UserService).isAdminUser()) {
    return true;
  } else {
    inject(Router).navigate(['/bikestore']);
    return false;
  }
};
