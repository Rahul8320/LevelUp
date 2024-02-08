import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { Bike } from '../../core/models/bike.model';
import { BikeRatingDetails } from '../../core/models/bike-rating.model';

@Injectable({
  providedIn: 'root',
})
export class BikesService {
  apiBaseUrl: string = '';

  constructor(private _httpClient: HttpClient) {
    this.apiBaseUrl = environment.apiUrl;
  }

  getAllAvailableBikes(
    maker: string | null,
    model: string | null,
    price: number | null
  ): Observable<Bike[]> {
    const apiUrl = `${this.apiBaseUrl}/api/bikes?maker=${maker ?? ''}&model=${
      model ?? ''
    }&price=${price ?? ''}`;
    return this._httpClient.get<Bike[]>(apiUrl);
  }

  getBikeDetails(bikeId: string): Observable<Bike> {
    const apiUrl = `${this.apiBaseUrl}/api/bikes/${bikeId}`;
    return this._httpClient.get<Bike>(apiUrl);
  }

  getBikeRatingDetails(bikeId: string): Observable<BikeRatingDetails> {
    const apiUrl = `${this.apiBaseUrl}/api/bike-rating/bike-rating-details/${bikeId}`;
    return this._httpClient.get<BikeRatingDetails>(apiUrl);
  }
}
