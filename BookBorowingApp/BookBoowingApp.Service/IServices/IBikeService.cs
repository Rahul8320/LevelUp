using BookBoowingApp.Domain.Common;
using BookBoowingApp.Domain.Entities;
using BookBoowingApp.Service.Models;

namespace BookBoowingApp.Service.IServices;

public interface IBikeService
{
    Task<ServiceResult<Guid>> CreateNewBike(AddBikeModel bikeModel, Guid userId);
    Task<ServiceResult<Bike>> UpdateExistingBike(Guid bikeId, AddBikeModel bikeModel, Guid userId);
    Task<ServiceResult<Bike>> UpdateBikeAvailabilityStatus(Bike bike, bool isAvailableForRent, BikeStatus currentBikeStatus);
    Task<ServiceResult<Bike>> UpdateBikeRequestForReturnStatus(Bike bike, bool isRequestForReturn, BikeStatus currentBikeStatus);
    Task<ServiceResult> DeleteBike(Guid bikeId, Guid userId);
    Task<ServiceResult<Bike>> GetBikeDetails(Guid bikeId);
    Task<ServiceResult<List<Bike>>> GetAllAvailableBike(string? maker = null, string? model = null, int? price = null);
}
