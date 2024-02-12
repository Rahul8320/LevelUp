import { inject } from '@angular/core';
import { DirtyComponent } from '../models/dirty-component';
import { MatDialog } from '@angular/material/dialog';
import { UnsaveChangesWaringComponent } from '../../shared/components/unsave-changes-waring/unsave-changes-waring.component';

export const hasUnsavedChangesGuard = async (component: DirtyComponent) => {
  const isDirty = component.isDirty();
  let shouldNavigate = true;
  if (isDirty) {
    const dialog = inject(MatDialog);
    const dialogRef = dialog.open(UnsaveChangesWaringComponent, {
      width: '600px',
      enterAnimationDuration: '200ms',
      exitAnimationDuration: '100ms',
    });
    const result = await dialogRef.afterClosed().toPromise();
    shouldNavigate = result ?? false;
  }
  return shouldNavigate;
};
