import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { UserService } from '../../shared/services/user.service';

export const adminGuard: CanActivateFn = (route, state) => {
  return inject(UserService).isAdminUser();
};
