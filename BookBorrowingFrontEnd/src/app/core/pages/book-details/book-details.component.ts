import { Component, OnDestroy, OnInit } from '@angular/core';
import { LoadingComponent } from '../../../shared/components/loading/loading.component';
import { Book } from '../../models/book.model';
import { Subscription } from 'rxjs';
import { BooksService } from '../../../shared/services/books.service';
import { ActivatedRoute } from '@angular/router';
import { HttpErrorResponse, HttpStatusCode } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-book-details',
  standalone: true,
  imports: [LoadingComponent, DatePipe, MatButtonModule],
  templateUrl: './book-details.component.html',
  styleUrl: './book-details.component.css',
})
export class BookDetailsComponent implements OnInit, OnDestroy {
  isLoading: boolean = false;
  bookDetails: Book | undefined;
  bookId: string = '';
  getBookDetailsSubscription: Subscription | undefined;

  constructor(
    private _bookService: BooksService,
    private _route: ActivatedRoute,
    private _snackbar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.isLoading = true;
    this._route.paramMap.subscribe((params) => {
      this.bookId = params.get('bookId') ?? '';
    });

    this.getBookDetailsSubscription = this._bookService
      .getBookDetails(this.bookId)
      .subscribe({
        next: (res: Book) => {
          this.bookDetails = res;
          this.isLoading = false;
        },
        error: (err: HttpErrorResponse) => {
          console.error(err);
          if (err.status === HttpStatusCode.NotFound) {
            this._snackbar.open(
              `Book with id: ${this.bookId} not found.`,
              'Ok',
              { duration: 5000 }
            );
          } else {
            this._snackbar.open('Something went wrong!', '❌', {
              duration: 5000,
            });
          }
        },
      });
    this.isLoading = false;
  }

  ngOnDestroy(): void {
    this.getBookDetailsSubscription?.unsubscribe();
  }
}
