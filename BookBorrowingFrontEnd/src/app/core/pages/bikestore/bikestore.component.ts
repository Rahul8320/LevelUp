import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { Bike } from '../../models/bike.model';
import { Subscription } from 'rxjs';
import { BikesService } from '../../../shared/services/bikes.service';
import { HttpErrorResponse } from '@angular/common/http';
import { LoadingComponent } from '../../../shared/components/loading/loading.component';
import { CurrencyPipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';

@Component({
  selector: 'app-bikestore',
  standalone: true,
  imports: [
    LoadingComponent,
    CurrencyPipe,
    MatButtonModule,
    RouterLink,
    MatInputModule,
    MatFormFieldModule,
  ],
  templateUrl: './bikestore.component.html',
  styleUrl: './bikestore.component.css',
})
export class BikestoreComponent implements OnInit, OnDestroy {
  isLoading = signal<boolean>(true);
  availableBikes = signal<Bike[]>([]);
  getAllBikesSubscription: Subscription | undefined;
  maker = signal<string | null>(null);
  model = signal<string | null>(null);
  price = signal<number | null>(null);

  constructor(private _bikeService: BikesService) {}

  ngOnInit(): void {
    this.getAvailableBikes();
  }

  getAvailableBikes(): void {
    this.isLoading.set(true);
    this.getAllBikesSubscription = this._bikeService
      .getAllAvailableBikes(this.maker(), this.model(), this.price())
      .subscribe({
        next: (res: Bike[]) => {
          this.availableBikes.set(res);
        },
        error: (err: HttpErrorResponse) => {
          console.error(err);
          alert('Something went wrong!');
        },
      });
    this.isLoading.set(false);
  }

  onMakerChange(event: Event) {
    const target = event.target as HTMLInputElement;
    this.maker.set(target.value);
    this.isButtonEnable();
  }

  onModelChange(event: Event) {
    const target = event.target as HTMLInputElement;
    this.model.set(target.value);
    this.isButtonEnable();
  }

  onPriceChange(event: Event) {
    const target = event.target as HTMLInputElement;
    this.price.set(Number(target.value));
    this.isButtonEnable();
  }

  isButtonEnable() {
    return this.maker() || this.model() || this.price();
  }

  onSearch() {
    this.getAvailableBikes();
  }

  onReset() {
    // this.isButtonEnable.set(false);
    this.maker.set(null);
    this.model.set(null);
    this.price.set(null);
    this.getAvailableBikes();
  }

  ngOnDestroy(): void {
    this.getAllBikesSubscription?.unsubscribe();
  }
}
