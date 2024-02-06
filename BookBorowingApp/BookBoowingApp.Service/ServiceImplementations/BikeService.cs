using System.Net;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Domain.Entities;
using BookBoowingApp.Domain.Enums;
using BookBoowingApp.Infrastructure.IRepositories;
using BookBoowingApp.Service.IServices;
using BookBoowingApp.Service.Models;
using Microsoft.IdentityModel.Tokens;

namespace BookBoowingApp.Service.ServiceImplementations;

/// <summary>
/// Represent the implementation of the bike service interface.
/// </summary>
/// <param name="unitOfWork">The unit of work interface.</param>
public class BikeService(IUnitOfWork unitOfWork, IAuthService authService) : IBikeService
{
    /// <summary>
    /// Represents unit of work interface.
    /// </summary>
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    /// <summary>
    /// Represents the auth service interface.
    /// </summary>
    private readonly IAuthService _authService = authService;

    /// <summary>
    /// Create new bike.
    /// </summary>
    /// <param name="bikeModel">The bike details.</param>
    /// <returns>Returns the newly created bike id.</returns>
    /// <exception cref="ApiException">The api exception.</exception>
    public async Task<ServiceResult<Guid>> CreateNewBike(AddBikeModel bikeModel)
    {
        try
        {
            // Get logged in user data
            var userData = _authService.GetAuthenticatedUserData();

            // Check for admin role
            if (userData.Role != UserRole.Admin.ToString())
            {
                return new ServiceResult<Guid>(
                    HttpStatusCode.Forbidden,
                    new ValidationError(
                        code: "PermissionDenied",
                        description: "You don't have permission to perform this operation!")
                );
            }

            // Create new bike entity.
            var newBike = new Bike()
            {
                Id = Guid.NewGuid(),
                Owner = Guid.Parse(userData.UserId!),
                CoverImage = bikeModel.CoverImage,
                Images = bikeModel.Images,
                Description = bikeModel.Description,
                Maker = bikeModel.Maker,
                Model = bikeModel.Model,
                RentalPricePerDay = bikeModel.RentalPricePerDay,
                LateFeesPerDay = bikeModel.LateFeesPerDay,
                FuelCapacity = bikeModel.FuelCapacity,
                FuelEconomy = bikeModel.FuelEconomy,
                IsAvailableForRent = true,
                CurrentBikeStatus = BikeStatus.AvailableForRent,
                IsRequestForReturn = false,
                CreatedAt = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            };

            // Add bike to database.
            _unitOfWork.BikeRepository.Add(newBike);
            var result = await _unitOfWork.Complete();

            // Check for success result.
            if (result == false)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, new Exception("Bike created failed!"));
            }

            // return service result with 201 status code.
            return new ServiceResult<Guid>(HttpStatusCode.Created, newBike.Id);
        }
        catch (ApiException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApiException(HttpStatusCode.InternalServerError, ex);
        }
    }

    /// <summary>
    /// Delete an existing bike.
    /// </summary>
    /// <param name="bikeId">The bike id.</param>
    /// <returns>Returns the service result indicating this delete operation result.</returns>
    /// <exception cref="ApiException">The api exception.</exception>
    public async Task<ServiceResult> DeleteBike(Guid bikeId)
    {
        try
        {
            // Get logged in user data
            var userData = _authService.GetAuthenticatedUserData();

            // Check for admin role
            if (userData.Role != UserRole.Admin.ToString())
            {
                return new ServiceResult(
                    HttpStatusCode.Forbidden,
                    new ValidationError(
                        code: "PermissionDenied",
                        description: "You don't have permission to perform this operation!")
                );
            }

            // Fetch bike data
            var existingBike = await _unitOfWork.BikeRepository.Get(bikeId);

            // Check for bike is exists or not.
            if (existingBike == null)
            {
                return new ServiceResult(
                    HttpStatusCode.NotFound,
                    new ValidationError(
                        code: "BikeNotFound",
                        description: $"Requested bike with id: {bikeId} is not found!")
                );
            }

            // Check the owner of the bike is same as user id.
            if (existingBike.Owner != Guid.Parse(userData.UserId!))
            {
                return new ServiceResult(
                    HttpStatusCode.Forbidden,
                    new ValidationError(
                        code: "PermissionDenied",
                        description: "You don't have permission to perform this operation!")
                );
            }

            // Delete the existing bike
            _unitOfWork.BikeRepository.Delete(existingBike);
            var result = await _unitOfWork.Complete();

            // Check for success result.
            if (result == false)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, new Exception("Failed to delete bike data!"));
            }

            // returns service result with success status code
            return new ServiceResult(HttpStatusCode.OK);
        }
        catch (ApiException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApiException(HttpStatusCode.InternalServerError, ex);
        }
    }

    /// <summary>
    /// Update existing bike details.
    /// </summary>
    /// <param name="bikeId">The bike id.</param>
    /// <param name="bikeModel">The update bike details.</param>
    /// <returns>Returns update bike data.</returns>
    /// <exception cref="ApiException">The api exception.</exception>
    public async Task<ServiceResult<Bike>> UpdateExistingBike(Guid bikeId, AddBikeModel bikeModel)
    {
        try
        {
            // Get logged in user data
            var userData = _authService.GetAuthenticatedUserData();

            // Check for admin role
            if (userData.Role != UserRole.Admin.ToString())
            {
                return new ServiceResult<Bike>(
                    HttpStatusCode.Forbidden,
                    new ValidationError(code: "PermissionDenied", description: "You don't have permission to perform this operation!")
                );
            }

            // Fetch bike data
            var existingBike = await _unitOfWork.BikeRepository.Get(bikeId);

            // Check for bike is exists or not.
            if (existingBike == null)
            {
                return new ServiceResult<Bike>(
                    HttpStatusCode.NotFound,
                    new ValidationError(
                        code: "BikeNotFound",
                        description: $"Requested bike with id: {bikeId} is not found!")
                );
            }

            // Check the owner of the bike is same as user id.
            if (existingBike.Owner != Guid.Parse(userData.UserId!))
            {
                return new ServiceResult<Bike>(
                   HttpStatusCode.Forbidden,
                   new ValidationError(
                       code: "PermissionDenied",
                       description: "You don't have permission to perform this operation!")
               );
            }

            // Update the bike details.
            existingBike.Maker = bikeModel.Maker;
            existingBike.Model = bikeModel.Model;
            existingBike.RentalPricePerDay = bikeModel.RentalPricePerDay;
            existingBike.LateFeesPerDay = bikeModel.LateFeesPerDay;
            existingBike.CoverImage = bikeModel.CoverImage;
            existingBike.Images = bikeModel.Images;
            existingBike.Description = bikeModel.Description;
            existingBike.FuelCapacity = bikeModel.FuelCapacity;
            existingBike.FuelEconomy = bikeModel.FuelEconomy;
            existingBike.LastUpdated = DateTime.UtcNow;

            // Save updated details to db.
            _unitOfWork.BikeRepository.Update(existingBike);
            var result = await _unitOfWork.Complete();

            // Check for success result.
            if (result == false)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, new Exception("Failed to updated bike data!"));
            }

            // returns service result with success status code
            return new ServiceResult<Bike>(HttpStatusCode.OK, existingBike);
        }
        catch (ApiException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApiException(HttpStatusCode.InternalServerError, ex);
        }
    }

    /// <summary>
    /// Get all available bike for rent.
    /// </summary>
    /// <param name="maker">The maker of bike.</param>
    /// <param name="model">The model of bike.</param>
    /// <param name="price">The price of bike.</param>
    /// <returns>Returns the list of bikes.</returns>
    /// <exception cref="ApiException">The api exception.</exception>
    public async Task<ServiceResult<List<Bike>>> GetAllAvailableBike(string? maker = null, string? model = null, int? price = null)
    {
        try
        {
            // Fetch all bikes data
            var allBikes = await _unitOfWork.BikeRepository.GetAll();

            // Check maker is null or not
            if (!maker.IsNullOrEmpty())
            {
                allBikes = allBikes.Where(b => b.Maker.Contains(maker!, StringComparison.OrdinalIgnoreCase));
            }

            // Check model is null or not
            if (!model.IsNullOrEmpty())
            {
                allBikes = allBikes.Where(b => b.Model.Contains(model!, StringComparison.OrdinalIgnoreCase));
            }

            // check price is null or not
            if (price.HasValue)
            {
                allBikes = allBikes.Where(b => b.RentalPricePerDay == price);
            }

            return new ServiceResult<List<Bike>>(HttpStatusCode.OK, allBikes.ToList());
        }
        catch (ApiException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApiException(HttpStatusCode.InternalServerError, ex);
        }
    }

    /// <summary>
    /// Get bike details by it's id.
    /// </summary>
    /// <param name="bikeId">The bike id.</param>
    /// <returns>Returns bike details.</returns>
    /// <exception cref="ApiException">The pai exception.</exception>
    public async Task<ServiceResult<Bike>> GetBikeDetails(Guid bikeId)
    {
        try
        {
            // fetch bike details
            var bike = await _unitOfWork.BikeRepository.Get(bikeId);

            // check for bike is exists or not.
            if (bike == null)
            {
                return new ServiceResult<Bike>(
                   HttpStatusCode.NotFound,
                   new ValidationError(
                       code: "BikeNotFound",
                       description: $"Requested bike with id: {bikeId} is not found!")
               );
            }

            return new ServiceResult<Bike>(HttpStatusCode.OK, bike);
        }
        catch (ApiException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApiException(HttpStatusCode.InternalServerError, ex);
        }
    }

    /// <summary>
    /// Update bike availability status.
    /// </summary>
    /// <param name="bike">The bike details.</param>
    /// <param name="isAvailableForRent">The availability status</param>
    /// <param name="currentBikeStatus">The current bike status.</param>
    /// <returns>Returns updated bike details.</returns>
    /// <exception cref="ApiException">The api exception.</exception>
    public async Task<ServiceResult<Bike>> UpdateBikeAvailabilityStatus(Bike bike, bool isAvailableForRent, BikeStatus currentBikeStatus)
    {
        try
        {
            // update bike data.
            bike.IsAvailableForRent = isAvailableForRent;
            bike.CurrentBikeStatus = currentBikeStatus;
            bike.LastUpdated = DateTime.UtcNow;

            // Update bike in database.
            _unitOfWork.BikeRepository.Update(bike);
            var result = await _unitOfWork.Complete();

            // Check for success result
            if (result == false)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, new Exception("Failed to update bike data!"));
            }

            return new ServiceResult<Bike>(HttpStatusCode.OK, bike);
        }
        catch (ApiException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApiException(HttpStatusCode.InternalServerError, ex);
        }
    }

    /// <summary>
    /// Update bike request for return status.
    /// </summary>
    /// <param name="bike">The bike details.</param>
    /// <param name="isRequestForReturn">The request for return status.</param>
    /// <param name="currentBikeStatus">The current bike status.</param>
    /// <returns>Returns updated bike details.</returns>
    /// <exception cref="ApiException">The api exception</exception>
    public async Task<ServiceResult<Bike>> UpdateBikeRequestForReturnStatus(Bike bike, bool isRequestForReturn, BikeStatus currentBikeStatus)
    {
        try
        {
            // update bike data.
            bike.IsRequestForReturn = isRequestForReturn;
            bike.CurrentBikeStatus = currentBikeStatus;
            bike.LastUpdated = DateTime.UtcNow;

            // Update bike in database.
            _unitOfWork.BikeRepository.Update(bike);
            var result = await _unitOfWork.Complete();

            // Check for success result
            if (result == false)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, new Exception("Failed to update bike data!"));
            }

            return new ServiceResult<Bike>(HttpStatusCode.OK, bike);
        }
        catch (ApiException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ApiException(HttpStatusCode.InternalServerError, ex);
        }
    }
}
