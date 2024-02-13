import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Bike } from '../../models/bike.model';
import { LoadingComponent } from '../../../shared/components/loading/loading.component';
import { BikesService } from '../../../shared/services/bikes.service';
import { HttpErrorResponse } from '@angular/common/http';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatIconModule } from '@angular/material/icon';
import { BikeRatingDetails } from '../../models/bike-rating.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { duration } from 'moment';

@Component({
  selector: 'app-bike-details',
  standalone: true,
  imports: [
    LoadingComponent,
    DatePipe,
    MatButtonModule,
    CurrencyPipe,
    MatTooltipModule,
    MatIconModule,
  ],
  templateUrl: './bike-details.component.html',
  styleUrl: './bike-details.component.css',
})
export class BikeDetailsComponent implements OnInit, OnDestroy {
  isLoading = signal<boolean>(true);
  bikeId = signal<string>('');
  bikeDetails = signal<Bike | undefined>(undefined);
  bikeRatingDetails = signal<BikeRatingDetails | undefined>(undefined);
  getBikeDetailsSubscription: Subscription | undefined;
  getBikeRatingDetailsSubscription: Subscription | undefined;

  constructor(
    private _route: ActivatedRoute,
    private _bikeService: BikesService,
    private _snackbar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this._route.paramMap.subscribe((params) => {
      this.bikeId.set(params.get('bikeId') ?? '');
    });

    if (this.bikeId() === '') {
      this._snackbar.open('Please provide an valid bike id!', 'Ok', {
        duration: 5000,
      });
    }

    this.getBikeDetailsSubscription = this._bikeService
      .getBikeDetails(this.bikeId())
      .subscribe({
        next: (res: Bike) => {
          this.bikeDetails.set(res);
        },
        error: (err: HttpErrorResponse) => {
          this.isLoading.set(false);
          console.error(err);
          this._snackbar.open('Something went wrong!', '❌', {
            duration: 5000,
          });
        },
      });

    this.getBikeRatingDetailsSubscription = this._bikeService
      .getBikeRatingDetails(this.bikeId())
      .subscribe({
        next: (res: BikeRatingDetails) => {
          this.bikeRatingDetails.set(res);
          this.isLoading.set(false);
        },
        error: (err: HttpErrorResponse) => {
          this.isLoading.set(false);
          console.error(err);
          this._snackbar.open('Something went wrong!', '❌', {
            duration: 5000,
          });
        },
      });
  }

  ngOnDestroy(): void {
    this.getBikeDetailsSubscription?.unsubscribe();
    this.getBikeRatingDetailsSubscription?.unsubscribe();
  }
}
