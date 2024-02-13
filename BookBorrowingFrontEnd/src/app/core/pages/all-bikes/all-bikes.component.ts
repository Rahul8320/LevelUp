import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { Bike } from '../../models/bike.model';
import { BikesService } from '../../../shared/services/bikes.service';
import { Subscription } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-all-bikes',
  standalone: true,
  imports: [],
  templateUrl: './all-bikes.component.html',
  styleUrl: './all-bikes.component.css',
})
export class AllBikesComponent implements OnInit, OnDestroy {
  adminBikes = signal<Bike[] | null>(null);
  isLoading = signal<boolean>(true);
  getAdminBikesSubscription: Subscription | undefined;

  constructor(
    private _bikeService: BikesService,
    private _snackbar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.getAdminBikesSubscription = this._bikeService
      .getAdminBikes()
      .subscribe({
        next: (res: Bike[]) => {
          this.adminBikes.set(res);
          this.isLoading.set(false);
        },
        error: (err: HttpErrorResponse) => {
          console.error(err);
          this._snackbar.open('Something went wrong!', '❌', {
            duration: 5000,
          });
        },
      });
  }

  ngOnDestroy(): void {
    this.getAdminBikesSubscription?.unsubscribe();
  }
}
