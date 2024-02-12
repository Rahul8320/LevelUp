import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import {
  AddBikeRequest,
  AddBikeResponse,
  Bike,
} from '../../core/models/bike.model';
import { BikeRatingDetails } from '../../core/models/bike-rating.model';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root',
})
export class BikesService {
  apiBaseUrl: string = '';

  constructor(
    private _httpClient: HttpClient,
    private _userService: UserService
  ) {
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

  createNewBike(bikeDetails: AddBikeRequest): Observable<AddBikeResponse> {
    console.log(bikeDetails);
    const apiUrl = `${this.apiBaseUrl}/api/bikes`;
    return this._httpClient.post<AddBikeResponse>(apiUrl, bikeDetails, {
      headers: {
        Authorization: `Bearer ${this._userService.authToken()}`,
      },
    });
  }
}
