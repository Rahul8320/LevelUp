import { Component, OnDestroy, OnInit } from '@angular/core';
import { BooksService } from '../../../shared/services/books.service';
import { BookComponent } from './components/book/book.component';
import { Book } from '../../models/book.model';
import { MatGridListModule } from '@angular/material/grid-list';
import { Subscription } from 'rxjs';
import { LoadingComponent } from '../../../shared/components/loading/loading.component';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [BookComponent, MatGridListModule, LoadingComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
})
export class DashboardComponent implements OnInit, OnDestroy {
  availableBooks: Book[] = [];
  isLoading: boolean = false;
  getAllBooksSubscription: Subscription | undefined;

  constructor(
    private _bookService: BooksService,
    private _snackbar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.getAllBooksSubscription = this._bookService.getAllBooks().subscribe({
      next: (res: Book[]) => {
        this.availableBooks = res;
        this.isLoading = false;
      },
      error: (err: HttpErrorResponse) => {
        console.error(err);
        this._snackbar.open('Something went wrong!', '❌', {
          duration: 5000,
        });
      },
    });
    this.isLoading = false;
  }

  ngOnDestroy(): void {
    this.getAllBooksSubscription?.unsubscribe();
  }
}
