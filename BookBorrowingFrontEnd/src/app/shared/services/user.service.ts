import { HttpClient } from '@angular/common/http';
import { Injectable, computed, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { LoginRequest, LoginResponse } from '../../core/models/login.model';
import { AuthUser } from '../../core/models/auth-user.model';
import { JwtService } from './jwt.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  apiBaseUrl: string = '';
  authToken = signal<string | null>(null);
  authUser = signal<AuthUser | undefined>(undefined);

  isAdminUser = computed(() => this.authUser()?.role === 'Admin');

  private readonly TOKEN_KEY = 'auth-token';
  private readonly EXPIRES_AT = 'expires-at';
  private readonly AUTH_USER_DATA = 'auth-user-data';

  constructor(
    private _httpClient: HttpClient,
    private _jwtService: JwtService
  ) {
    this.apiBaseUrl = environment.apiUrl;
    if (localStorage.getItem(this.TOKEN_KEY)) {
      this.authToken.set(localStorage.getItem(this.TOKEN_KEY));
    }
    if (localStorage.getItem(this.AUTH_USER_DATA)) {
      this.authUser.set(
        JSON.parse(localStorage.getItem(this.AUTH_USER_DATA) ?? '')
      );
    }
  }

  userLogin(loginRequestData: LoginRequest): Observable<LoginResponse> {
    const apiUrl = `${this.apiBaseUrl}/api/authentication/login`;
    return this._httpClient.post<LoginResponse>(apiUrl, loginRequestData);
  }

  verifyAuthToken(token: string): Observable<AuthUser> {
    const apiUrl = `${this.apiBaseUrl}/api/authentication/verify`;
    return this._httpClient.get<AuthUser>(apiUrl, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  }

  setSession(authResult: LoginResponse) {
    this.authToken.set(authResult.token);

    localStorage.setItem(this.TOKEN_KEY, authResult.token);
    localStorage.setItem(
      this.EXPIRES_AT,
      JSON.stringify(authResult.expiration)
    );
  }

  setSessionUserData(result: AuthUser) {
    this.authUser.set(result);
    localStorage.setItem(this.AUTH_USER_DATA, JSON.stringify(result));
  }

  userLogout(): boolean {
    this.authToken.set(null);
    this.authUser.set(undefined);
    localStorage.removeItem(this.TOKEN_KEY);
    localStorage.removeItem(this.EXPIRES_AT);
    localStorage.removeItem(this.AUTH_USER_DATA);
    return true;
  }

  public isLoggedIn() {
    return this._jwtService.isTokenValid();
  }

  public isLogout() {
    return !this.isLoggedIn();
  }
}
