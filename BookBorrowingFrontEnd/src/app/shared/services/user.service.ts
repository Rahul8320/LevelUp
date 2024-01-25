import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { LoginRequest, LoginResponse } from '../../core/models/login.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly TOKEN_KEY = "authToken";

  apiBaseUrl: string = '';

  constructor(private _httpClient: HttpClient) {
    this.apiBaseUrl = environment.apiUrl;
  }

  userLogin(loginRequestData: LoginRequest): Observable<LoginResponse> {
    const apiUrl = `${this.apiBaseUrl}/api/Authentication/login`;
    return this._httpClient.post<LoginResponse>(apiUrl, { loginRequestData });
  }

  getAuthToken(): string | null {
    return sessionStorage.getItem(this.TOKEN_KEY);
  }

  setAuthToken(token: string): void {
    sessionStorage.setItem(this.TOKEN_KEY, token);
  }

  clearAuthToken(): void {
    sessionStorage.removeItem(this.TOKEN_KEY);
  }
}
