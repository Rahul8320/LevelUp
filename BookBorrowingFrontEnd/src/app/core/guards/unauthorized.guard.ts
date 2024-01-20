import { CanActivateFn } from '@angular/router';

export const unauthorizedGuard: CanActivateFn = (route, state) => {
  return true;
};
