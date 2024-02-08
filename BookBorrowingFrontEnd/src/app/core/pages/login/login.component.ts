import { Component, OnDestroy, signal } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { UserService } from '../../../shared/services/user.service';
import { Subscription } from 'rxjs';
import { LoginRequest, LoginResponse } from '../../models/login.model';
import { HttpErrorResponse } from '@angular/common/http';
import { LoadingComponent } from '../../../shared/components/loading/loading.component';
import { AuthUser } from '../../models/auth-user.model';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    RouterLink,
    ReactiveFormsModule,
    MatButtonModule,
    CommonModule,
    LoadingComponent,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnDestroy {
  loginForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });
  userLoginSubscription: Subscription | undefined;
  getUserDataSubscription: Subscription | undefined;
  isLoading = signal<boolean>(false);

  constructor(private _userService: UserService, private _router: Router) {}

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

    this.userLoginSubscription = this._userService
      .userLogin(loginRequestData)
      .subscribe({
        next: (res: LoginResponse) => {
          this._userService.authToken.set(res.token);
          this.getUserDataSubscription = this._userService
            .verifyAuthToken(res.token)
            .subscribe({
              next: (res: AuthUser) => {
                this._userService.authUser.set(res);
              },
              error: (err: HttpErrorResponse) => {
                console.warn(err);
                alert('Something went wrong!');
              },
            });
        },
        error: (err: HttpErrorResponse) => {
          console.warn(err);
          alert('Something went wrong!');
        },
      });

    this._router.navigate(['/']);
    this.isLoading.set(false);
    this.loginForm.reset();
  }

  ngOnDestroy(): void {
    this.userLoginSubscription?.unsubscribe();
    this.getUserDataSubscription?.unsubscribe();
  }
}
