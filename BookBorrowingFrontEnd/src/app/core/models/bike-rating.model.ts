export interface BikeRating {
  id: string;
  bikeId: string;
  userId: string;
  rating: number;
  review?: string;
  lastUpdated: string;
}

export interface BikeRatingDetails {
  averageRating: number;
  numberOfRating: number;
  bikeRatings: BikeRating[];
}
