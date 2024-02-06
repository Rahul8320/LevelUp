using BookBoowingApp.Domain.Common;
using BookBoowingApp.Domain.Entities;
using BookBoowingApp.Domain.Enums;
using BookBoowingApp.Service.Models;

namespace BookBoowingApp.Service.IServices;

/// <summary>
/// Represent bike service interface.
/// </summary>
public interface IBikeService
{
    /// <summary>
    /// Add new bike to database.
    /// </summary>
    /// <param name="bikeModel">New bike details.</param>
    /// <param name="userId">User id</param>
    /// <returns>Returns the newly created bike id.</returns>
    Task<ServiceResult<Guid>> CreateNewBike(AddBikeModel bikeModel, Guid userId);

    /// <summary>
    /// Update existing bike details.
    /// </summary>
    /// <param name="bikeId">The bike id.</param>
    /// <param name="bikeModel">The update bike details.</param>
    /// <param name="userId">User id</param>
    /// <returns>Returns updated bike data.</returns>
    Task<ServiceResult<Bike>> UpdateExistingBike(Guid bikeId, AddBikeModel bikeModel, Guid userId);

    /// <summary>
    /// Update bike availability status
    /// </summary>
    /// <param name="bike">The bike details</param>
    /// <param name="isAvailableForRent">This bike availability</param>
    /// <param name="currentBikeStatus">The current bike status.</param>
    /// <returns>Returns the updated bike data.</returns>
    Task<ServiceResult<Bike>> UpdateBikeAvailabilityStatus(Bike bike, bool isAvailableForRent, BikeStatus currentBikeStatus);

    /// <summary>
    /// Update bike request for return status.
    /// </summary>
    /// <param name="bike">The bike details.</param>
    /// <param name="isRequestForReturn">The request for return status.</param>
    /// <param name="currentBikeStatus">The current bike status.</param>
    /// <returns></returns>
    Task<ServiceResult<Bike>> UpdateBikeRequestForReturnStatus(Bike bike, bool isRequestForReturn, BikeStatus currentBikeStatus);

    /// <summary>
    /// Delete bike from database.
    /// </summary>
    /// <param name="bikeId">The bike id.</param>
    /// <param name="userId">User id.</param>
    /// <returns>Returns service result indication delete operation result.</returns>
    Task<ServiceResult> DeleteBike(Guid bikeId, Guid userId);

    /// <summary>
    /// Get bike details by id.
    /// </summary>
    /// <param name="bikeId">The bike id.</param>
    /// <returns>Returns bike data.</returns>
    Task<ServiceResult<Bike>> GetBikeDetails(Guid bikeId);

    /// <summary>
    /// Search available bike for rent.
    /// </summary>
    /// <param name="maker">The bike maker.</param>
    /// <param name="model">The bike model.</param>
    /// <param name="price">The price.</param>
    /// <returns>Returns list of available bike for rent.</returns>
    Task<ServiceResult<List<Bike>>> GetAllAvailableBike(string? maker = null, string? model = null, int? price = null);
}
