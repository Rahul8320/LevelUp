import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { Book } from '../../core/models/book.model';

@Injectable({
  providedIn: 'root'
})
export class BooksService {
  apiBaseUrl: string = '';

  constructor(private _httpClient: HttpClient) {
    this.apiBaseUrl = environment.apiUrl;
  }

  getAllBooks(): Observable<Book[]> {
    const apiUrl = `${this.apiBaseUrl}/api/books`;
    return this._httpClient.get<Book[]>(apiUrl);
  }

  getBookDetails(bookId: string): Observable<Book> {
    const apiUrl = `${this.apiBaseUrl}/api/books/${bookId}`;
    return this._httpClient.get<Book>(apiUrl);
  }

}
