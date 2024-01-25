import { Component, Input } from '@angular/core';
import { Book } from '../../../../models/book.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-book',
  standalone: true,
  imports: [],
  templateUrl: './book.component.html',
  styleUrl: './book.component.css'
})
export class BookComponent {
  @Input() book: Book | undefined;

  constructor(private _route: Router) { }

  goToDetailsPage() {
    this._route.navigate(["/book-details/", this.book?.id]);
  }
}
