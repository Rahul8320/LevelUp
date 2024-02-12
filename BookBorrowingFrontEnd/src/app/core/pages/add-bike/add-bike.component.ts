import { Component, OnDestroy, signal } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { NgClass } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { BikesService } from '../../../shared/services/bikes.service';
import { AddBikeRequest } from '../../models/bike.model';
import { HttpErrorResponse } from '@angular/common/http';
import { MatButtonModule } from '@angular/material/button';
import { LoadingComponent } from '../../../shared/components/loading/loading.component';

@Component({
  selector: 'app-add-bike',
  standalone: true,
  imports: [ReactiveFormsModule, NgClass, MatButtonModule, LoadingComponent],
  templateUrl: './add-bike.component.html',
  styleUrl: './add-bike.component.css',
})
export class AddBikeComponent implements OnDestroy {
  addBikeRequestSubscription: Subscription | undefined;

  isLoading = signal<boolean>(false);

  addBikeForm = new FormGroup({
    maker: new FormControl<string>('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(50),
    ]),
    model: new FormControl<string>('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(50),
    ]),
    description: new FormControl<string>('', [
      Validators.required,
      Validators.minLength(30),
      Validators.maxLength(500),
    ]),
    rentalPricePerDay: new FormControl<number>(100, [
      Validators.required,
      Validators.min(100),
      Validators.max(10000),
    ]),
    lateFeesPerDay: new FormControl<number>(100, [
      Validators.required,
      Validators.min(100),
      Validators.max(10000),
    ]),
    fuelCapacity: new FormControl<number>(5, [
      Validators.required,
      Validators.min(5),
      Validators.max(25),
    ]),
    fuelEconomy: new FormControl<number>(5, [
      Validators.required,
      Validators.min(5),
      Validators.max(75),
    ]),
    coverImage: new FormControl<string>('', [
      Validators.required,
      Validators.pattern(/^(http|https):\/\/[^ "]+$/),
    ]),
    images: new FormControl<string>('', [Validators.required]),
  });

  constructor(
    private _matSnackbar: MatSnackBar,
    private _router: Router,
    private _bikeService: BikesService
  ) {}

  addNewBike() {
    if (this.addBikeForm.invalid) {
      this._matSnackbar.open('Please filled all the required field!', 'Ok', {
        duration: 5000,
      });
      return;
    }

    this.isLoading.set(true);

    const addBikeRequest: AddBikeRequest = {
      maker: this.addBikeForm.value.maker!,
      model: this.addBikeForm.value.model!,
      description: this.addBikeForm.value.description!,
      rentalPricePerDay: this.addBikeForm.value.rentalPricePerDay!,
      lateFeesPerDay: this.addBikeForm.value.lateFeesPerDay!,
      fuelCapacity: this.addBikeForm.value.fuelCapacity!,
      fuelEconomy: this.addBikeForm.value.fuelEconomy!,
      coverImage: this.addBikeForm.value.coverImage!,
      images: this.addBikeForm.value.images?.split(',')!,
    };

    this.addBikeRequestSubscription = this._bikeService
      .createNewBike(addBikeRequest)
      .subscribe({
        next: (res) => {
          this.isLoading.set(false);
          this._router.navigate(['/bike-details/', res.data]);
          this._matSnackbar.open('Biked added successfully.', '✅', {
            duration: 3000,
          });
        },
        error: (err: HttpErrorResponse) => {
          console.error(err);
          this._matSnackbar.open('Failed to create this bike.', '❌', {
            duration: 3000,
          });
        },
      });

    this.isLoading.set(false);
  }

  get maker() {
    return this.addBikeForm.get('maker');
  }

  get model() {
    return this.addBikeForm.get('model');
  }

  get description() {
    return this.addBikeForm.get('description');
  }

  get rentalPricePerDay() {
    return this.addBikeForm.get('rentalPricePerDay');
  }

  get lateFeesPerDay() {
    return this.addBikeForm.get('lateFeesPerDay');
  }

  get fuelCapacity() {
    return this.addBikeForm.get('fuelCapacity');
  }

  get fuelEconomy() {
    return this.addBikeForm.get('fuelEconomy');
  }

  get coverImage() {
    return this.addBikeForm.get('coverImage');
  }

  get images() {
    return this.addBikeForm.get('images');
  }

  ngOnDestroy(): void {
    this.addBikeRequestSubscription?.unsubscribe();
  }
}
