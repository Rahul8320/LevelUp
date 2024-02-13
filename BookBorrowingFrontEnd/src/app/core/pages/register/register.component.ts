import { Component, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { LoadingComponent } from '../../../shared/components/loading/loading.component';
import { Subscription } from 'rxjs';
import { UserService } from '../../../shared/services/user.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { NgClass } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { DirtyComponent } from '../../models/dirty-component';
import { RegisterRequest, RegisterResponse } from '../../models/register.model';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    RouterLink,
    LoadingComponent,
    ReactiveFormsModule,
    NgClass,
    MatButtonModule,
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent implements DirtyComponent {
  isLoading = signal<boolean>(false);
  registerUserSubscription: Subscription | undefined;

  registerForm = new FormGroup({
    name: new FormControl<string>('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(15),
    ]),
    username: new FormControl<string>('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(15),
    ]),
    email: new FormControl<string>('', [Validators.required, Validators.email]),
    password: new FormControl<string>('', [
      Validators.required,
      Validators.minLength(8),
      Validators.maxLength(20),
    ]),
  });

  constructor(
    private _router: Router,
    private _userService: UserService,
    private _snackbar: MatSnackBar
  ) {}

  onSubmit() {
    if (this.registerForm.invalid) {
      this._snackbar.open('Please filled all the required field!', 'Ok', {
        duration: 5000,
      });
      return;
    }

    this.isLoading.set(true);

    const registerRequest: RegisterRequest = {
      name: this.name?.value!,
      email: this.email?.value!,
      userName: this.username?.value!,
      password: this.password?.value!,
    };

    this.registerUserSubscription = this._userService
      .userRegister(registerRequest)
      .subscribe({
        next: (res: RegisterResponse) => {
          this.isLoading.set(false);
          this._snackbar.open(res.message, '✅', { duration: 3000 });
          this._router.navigate(['/login']);
        },
        error: (err: HttpErrorResponse) => {
          console.error(err);
          this._snackbar.open('Register Failed!', '❌', { duration: 5000 });
          this.isLoading.set(false);
        },
      });
  }

  isDirty(): boolean {
    return this.registerForm.dirty;
  }

  get name() {
    return this.registerForm.get('name');
  }

  get username() {
    return this.registerForm.get('username');
  }

  get email() {
    return this.registerForm.get('email');
  }

  get password() {
    return this.registerForm.get('password');
  }

  ngOnDestroy(): void {
    this.registerUserSubscription?.unsubscribe();
  }
}
