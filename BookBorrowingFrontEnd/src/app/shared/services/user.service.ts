import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable, single } from 'rxjs';
import { LoginRequest, LoginResponse } from '../../core/models/login.model';
import { AuthUser } from '../../core/models/auth-user.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  apiBaseUrl: string = '';
  authToken = signal<string | null>(null);
  authUser = signal<AuthUser | undefined>(undefined);

  constructor(private _httpClient: HttpClient) {
    this.apiBaseUrl = environment.apiUrl;
  }

  userLogin(loginRequestData: LoginRequest): Observable<LoginResponse> {
    const apiUrl = `${this.apiBaseUrl}/api/authentication/login`;
    return this._httpClient.post<LoginResponse>(apiUrl, loginRequestData);
  }

  userLogout(): boolean {
    this.authToken.set(null);
    this.authUser.set(undefined);
    return true;
  }

  verifyAuthToken(token: string): Observable<AuthUser> {
    const apiUrl = `${this.apiBaseUrl}/api/authentication/verify`;
    return this._httpClient.get<AuthUser>(apiUrl, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
}
