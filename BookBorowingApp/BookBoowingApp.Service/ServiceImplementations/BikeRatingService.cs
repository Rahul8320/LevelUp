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

    public async Task<ServiceResult<BikeRatingDetailsDTO>> GetAverageRatingDetails(Guid bikeId)
    {
        try
        {
            // declare bike rating details dto.
            var bikeRatingDetail = new BikeRatingDetailsDTO
            {
                NumberOfRating = 0,
                AverageRating = 0
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

    public Task<ServiceResult> UpdateExistingRating(Guid ratingId, string review, Guid userId)
    {
        throw new NotImplementedException();
    }
}
