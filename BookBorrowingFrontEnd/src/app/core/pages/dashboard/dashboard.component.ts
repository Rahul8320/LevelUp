import { Component } from '@angular/core';
import { BooksService } from '../../../shared/services/books.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  constructor(private _bookService: BooksService) { }
}
