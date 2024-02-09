import { Component, inject } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { UserService } from '../../services/user.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    RouterLink,
    RouterLinkActive,
    MatButtonModule,
    MatMenuModule,
    MatIconModule,
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  private readonly _userService = inject(UserService);

  authUser = this._userService.authUser;

  isAdminUser = this._userService.isAdminUser;

  constructor(private _matSnackbar: MatSnackBar) {}

  onLogout() {
    const result = this._userService.userLogout();
    if (result) {
      this._matSnackbar.open('Logout Successfully.', '✅', { duration: 3000 });
    } else {
      this._matSnackbar.open('Error in logout!', '❌', { duration: 3000 });
    }
  }
}
