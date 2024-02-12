import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class JwtService {
  private readonly TOKEN_KEY = 'auth-token';

  decodeToken(token: string): any {
    return jwtDecode(token);
  }

  isTokenValid(): boolean {
    const token = localStorage.getItem(this.TOKEN_KEY);

    if (!token) {
      return false;
    }

    const decoded_token = this.decodeToken(token);

    if (!decoded_token || !decoded_token.exp) {
      return false;
    }

    const currentTime = Math.floor(Date.now() / 1000);

    if (currentTime < Number(decoded_token.exp)) {
      return true;
    } else {
      return false;
    }
  }
}
