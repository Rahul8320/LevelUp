import { Component, OnDestroy, OnInit } from '@angular/core';
import { BooksService } from '../../../shared/services/books.service';
import { BookComponent } from './components/book/book.component';
import { Book } from '../../models/book.model';
import { MatGridListModule } from '@angular/material/grid-list';
import { Subscription } from 'rxjs';
import { LoadingComponent } from '../../../shared/components/loading/loading.component';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [BookComponent, MatGridListModule, LoadingComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit, OnDestroy {
  availableBooks: Book[] = [];
  isLoading: boolean = false;
  getAllBooksSubscription: Subscription | undefined;

  constructor(private _bookService: BooksService) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.getAllBooksSubscription = this._bookService.getAllBooks().subscribe({
      next: (res: Book[]) => {
        this.availableBooks = res;
        this.isLoading = false;
      },
      error: (err: HttpErrorResponse) => {
        alert(err?.message || "Something went wrong");
      }
    });
    this.isLoading = false;
  }

  ngOnDestroy(): void {
    this.getAllBooksSubscription?.unsubscribe();
  }
}