using System.Net;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Domain.Entities;
using BookBoowingApp.Infrastructure.IRepositories;
using BookBoowingApp.Service.DTOs;
using BookBoowingApp.Service.IServices;
using BookBoowingApp.Service.Models;

namespace BookBoowingApp.Service.ServiceImplementations;

/// <summary>
/// Represents the implementation of bike rating service interface.
/// </summary>
public class BikeRatingService(IUnitOfWork unitOfWork) : IBikeRatingService
{
    /// <summary>
    /// Represents the unit of work interface.
    /// </summary>
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    /// <summary>
    /// Create new rating.
    /// </summary>
    /// <param name="bikeRatingModel">The bike rating model.</param>
    /// <param name="userId">The user id.</param>
    /// <returns>Returns service result indicating the result of this action.</returns>
    /// <exception cref="ApiException">The api exception.</exception>
    public async Task<ServiceResult> AddRating(AddBikeRatingModel bikeRatingModel, Guid userId)
    {
        try
        {
            // fetch bike data
            var bikeDetails = await _unitOfWork.BikeRepository.Get(bikeRatingModel.BikeId);

            // Check for bike is exists or not.
            if (bikeDetails == null)
            {
                return new ServiceResult(
                    HttpStatusCode.BadRequest,
                    new ValidationError(code: "InvalidBikeId", description: "Bike id is not valid.")
                );
            }

            // Create the bike rating entity.
            var bikeRating = new BikeRating()
            {
                Id = Guid.NewGuid(),
                BikeId = bikeRatingModel.BikeId,
                UserId = userId,
                Rating = bikeRatingModel.Rating,
                Review = bikeRatingModel.Review,
                LastUpdated = DateTime.UtcNow,
            };

            // Create and update the database.
            _unitOfWork.BikeRatingRepository.Add(bikeRating);
            var result = await _unitOfWork.Complete();

            // Check for success response.
            if (result == false)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, new Exception("Failed to create bike rating!"));
            }

            return new ServiceResult(HttpStatusCode.Created);
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
    /// Get bike rating details
    /// </summary>
    /// <param name="bikeId">The bike id</param>
    /// <returns>Returns list of all ratings with average rating number.</returns>
    /// <exception cref="ApiException">The Api Exception.</exception>
    public async Task<ServiceResult<BikeRatingDetailsDTO>> GetAverageRatingDetails(Guid bikeId)
    {
        try
        {
            // declare bike rating details dto.
            var bikeRatingDetail = new BikeRatingDetailsDTO
            {
                NumberOfRating = 0,
                AverageRating = 0,
                BikeRatings = []
            };

            // Fetch all rating details.
            var allRatings = await _unitOfWork.BikeRatingRepository.GetAll();

            // Check anu rating exists or not
            if (allRatings.Any())
            {
                // Get all bike rating.
                var allBikeRatings = allRatings.Where(u => u.BikeId == bikeId).ToList();

                // check any rating of requested bike id exists or not
                if (allBikeRatings.Count != 0)
                {
                    // total sum of all ratings
                    var totalSumOfAllRatings = allBikeRatings.Sum(u => u.Rating);
                    var numberOfRating = allBikeRatings.Count;

                    // update bike rating details dto.
                    bikeRatingDetail.NumberOfRating = numberOfRating;
                    bikeRatingDetail.AverageRating = totalSumOfAllRatings / numberOfRating;
                    bikeRatingDetail.BikeRatings = allBikeRatings;
                }
            }

            return new ServiceResult<BikeRatingDetailsDTO>(HttpStatusCode.OK, bikeRatingDetail);
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
    /// Update an existing rating details.
    /// </summary>
    /// <param name="ratingId">The rating id.</param>
    /// <param name="review">The updated review.</param>
    /// <param name="userId">The user id.</param>
    /// <returns>Returns a service result indication the result of this operation.</returns>
    /// <exception cref="ApiException">The api exception.</exception>
    public async Task<ServiceResult> UpdateExistingRating(Guid ratingId, string review, Guid userId)
    {
        try
        {
            // Fetch rating details
            var bikeRating = await _unitOfWork.BikeRatingRepository.Get(ratingId);

            // check for rating details exists or not
            if (bikeRating == null)
            {
                return new ServiceResult(
                    HttpStatusCode.NotFound,
                    new ValidationError(code: "NotFoundRating", description: $"The rating with id: {ratingId} is not found!")
                );
            }

            // check for requested user is same as rating user
            if (bikeRating.UserId != userId)
            {
                return new ServiceResult(
                    HttpStatusCode.Forbidden,
                    new ValidationError(code: "PermissionDenied", description: "You don't have permission for this operation!")
                );
            }

            // Update the bike rating
            bikeRating.Review = review;
            bikeRating.LastUpdated = DateTime.UtcNow;

            // Update the database
            _unitOfWork.BikeRatingRepository.Update(bikeRating);
            var result = await _unitOfWork.Complete();

            // Check for success result.
            if (result == false)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, new Exception("Bike rating updated failed!"));
            }

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
}
