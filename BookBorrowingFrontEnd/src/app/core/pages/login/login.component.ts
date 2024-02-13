import { Component, OnDestroy, signal } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { NgClass } from '@angular/common';
import { UserService } from '../../../shared/services/user.service';
import { Subscription } from 'rxjs';
import { LoginRequest, LoginResponse } from '../../models/login.model';
import { HttpErrorResponse, HttpStatusCode } from '@angular/common/http';
import { LoadingComponent } from '../../../shared/components/loading/loading.component';
import { AuthUser } from '../../models/auth-user.model';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    RouterLink,
    ReactiveFormsModule,
    MatButtonModule,
    NgClass,
    LoadingComponent,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnDestroy {
  loginForm = new FormGroup({
    username: new FormControl<string>('', Validators.required),
    password: new FormControl<string>('', Validators.required),
  });
  userLoginSubscription: Subscription | undefined;
  getUserDataSubscription: Subscription | undefined;
  isLoading = signal<boolean>(false);

  constructor(
    private _userService: UserService,
    private _router: Router,
    private _snackbar: MatSnackBar
  ) {}

  isUsernameInvalid() {
    return (
      this.loginForm.controls.username.invalid &&
      (this.loginForm.controls.username.dirty ||
        this.loginForm.controls.username.touched)
    );
  }

  isPasswordInvalid() {
    return (
      this.loginForm.controls.password.invalid &&
      (this.loginForm.controls.password.dirty ||
        this.loginForm.controls.password.touched)
    );
  }

  onSubmit() {
    if (this.isUsernameInvalid() || this.isPasswordInvalid()) {
      return alert('Please fill the required field!');
    }
    this.isLoading.set(true);
    const loginRequestData: LoginRequest = {
      username: this.loginForm.value.username!,
      password: this.loginForm.value.password!,
    };

    // submit login request
    this.userLoginSubscription = this._userService
      .userLogin(loginRequestData)
      .subscribe({
        next: (res: LoginResponse) => {
          this._userService.setSession(res);
          // submit verify token request
          this.getUserDataSubscription = this._userService
            .verifyAuthToken(res.token)
            .subscribe({
              next: (res: AuthUser) => {
                this._userService.setSessionUserData(res);
                this._router.navigate(['/']);
                this._snackbar.open('Login Successfully.', '✅', {
                  duration: 3000,
                });
                this.isLoading.set(false);
              },
              error: (err: HttpErrorResponse) => {
                this._snackbar.open('Something went wrong!', '❌', {
                  duration: 5000,
                });
                this.isLoading.set(false);
              },
            });
        },
        error: (err: HttpErrorResponse) => {
          console.warn(err);
          if (err.status === HttpStatusCode.Unauthorized) {
            this._snackbar.open('Invalid credentials', '❌', {
              duration: 5000,
            });
          } else {
            this._snackbar.open('Something went wrong!', '❌', {
              duration: 5000,
            });
          }
          this.isLoading.set(false);
        },
      });
  }

  ngOnDestroy(): void {
    this.userLoginSubscription?.unsubscribe();
    this.getUserDataSubscription?.unsubscribe();
  }
}
