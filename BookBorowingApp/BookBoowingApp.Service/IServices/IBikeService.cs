using BookBoowingApp.Domain.Common;
using BookBoowingApp.Domain.Entities;
using BookBoowingApp.Service.Models;

namespace BookBoowingApp.Service.IServices;

public interface IBikeService
{
    Task<ServiceResult> CreateNewBike(AddBikeModel bikeModel, Guid userId);
    Task<ServiceResult> UpdateExistingBike(Guid bikeId, AddBikeModel bikeModel, Guid userId);
    Task<ServiceResult> UpdateBikeAvailabilityStatus(Guid bikeId, Guid userId, bool isAvailableForRent, BikeStatus currentBikeStatus);
    Task<ServiceResult> UpdateBikeRequestForReturnStatus(Guid bikeId, Guid userId, bool isRequestForReturn, BikeStatus currentBikeStatus);
    Task<ServiceResult> DeleteBike(Guid bikeId, Guid userId);
    Task<ServiceResult<Bike>> GetBikeDetails(Guid bikeId);
    Task<ServiceResult<List<Bike>>> GetAllAvailableBike(string? maker = null, string? model = null, int? price = null);
}
