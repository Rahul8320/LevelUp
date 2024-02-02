using System.Net;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Domain.Entities;
using BookBoowingApp.Infrastructure.IRepositories;
using BookBoowingApp.Service.IServices;
using BookBoowingApp.Service.Models;

namespace BookBoowingApp.Service.ServiceImplementations;

public class BikeService(IUnitOfWork unitOfWork) : IBikeService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ServiceResult<Guid>> CreateNewBike(AddBikeModel bikeModel, Guid userId)
    {
        try
        {
            // Create new bike entity.
            var newBike = new Bike()
            {
                Id = Guid.NewGuid(),
                Owner = userId,
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

    public async Task<ServiceResult> DeleteBike(Guid bikeId, Guid userId)
    {
        try
        {
            // Fetch bike data
            var existingBike = await _unitOfWork.BikeRepository.Get(bikeId);

            // Check for bike is exists or not.
            if (existingBike == null)
            {
                return new ServiceResult(HttpStatusCode.NotFound);
            }

            // Check the owner of the bike is same as user id.
            if (existingBike.Owner != userId)
            {
                return new ServiceResult(HttpStatusCode.Forbidden);
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

    public async Task<ServiceResult<Bike>> UpdateExistingBike(Guid bikeId, AddBikeModel bikeModel, Guid userId)
    {
        try
        {
            // Fetch bike data
            var existingBike = await _unitOfWork.BikeRepository.Get(bikeId);

            // Check for bike is exists or not.
            if (existingBike == null)
            {
                return new ServiceResult<Bike>(HttpStatusCode.NotFound);
            }

            // Check the owner of the bike is same as user id.
            if (existingBike.Owner != userId)
            {
                return new ServiceResult<Bike>(HttpStatusCode.Forbidden);
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

    public Task<ServiceResult<List<Bike>>> GetAllAvailableBike(string? maker = null, string? model = null, int? price = null)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult<Bike>> GetBikeDetails(Guid bikeId)
    {
        throw new NotImplementedException();
    }

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
