export interface Bike {
    id: string;
    owner: string;
    maker: string;
    model: string;
    rentalPricePerDay: number;
    lateFeesPerDay: number,
    isAvailableForRent: boolean,
    isRequestForReturn: boolean,
    currentBikeStatus: string,
    coverImage: string;
    images: [string];
    description: string;
    fuelCapacity: number;
    fuelEconomy: number;
    createdAt: string;
    lastUpdated: string;
}