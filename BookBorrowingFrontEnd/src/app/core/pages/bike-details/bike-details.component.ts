import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Bike } from '../../models/bike.model';
import { LoadingComponent } from '../../../shared/components/loading/loading.component';
import { BikesService } from '../../../shared/services/bikes.service';
import { HttpErrorResponse } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-bike-details',
  standalone: true,
  imports: [LoadingComponent, DatePipe, MatButtonModule],
  templateUrl: './bike-details.component.html',
  styleUrl: './bike-details.component.css',
})
export class BikeDetailsComponent implements OnInit, OnDestroy {
  isLoading = signal<boolean>(true);
  bikeId = signal<string>('');
  bikeDetails = signal<Bike | undefined>(undefined);
  getBikeDetailsSubscription: Subscription | undefined;

  constructor(
    private _route: ActivatedRoute,
    private _bikeService: BikesService
  ) {}

  ngOnInit(): void {
    this._route.paramMap.subscribe((params) => {
      this.bikeId.set(params.get('bikeId') ?? '');
    });

    if (this.bikeId() === '') {
      alert('Please provide an valid bike id!');
    }

    this.getBikeDetailsSubscription = this._bikeService
      .getBikeDetails(this.bikeId())
      .subscribe({
        next: (res: Bike) => {
          this.bikeDetails.set(res);
        },
        error: (err: HttpErrorResponse) => {
          console.error(err);
          alert('Something went wrong!');
        },
      });

    this.isLoading.set(false);
  }

  ngOnDestroy(): void {
    this.getBikeDetailsSubscription?.unsubscribe();
  }
}
