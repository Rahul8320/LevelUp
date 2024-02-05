using BookBoowingApp.Domain.Common;
using BookBoowingApp.Service.DTOs;
using BookBoowingApp.Service.Models;

namespace BookBoowingApp.Service.IServices;

/// <summary>
/// Represent interface for bike rating service.
/// </summary>
public interface IBikeRatingService
{
    /// <summary>
    /// Create new rating for bike.
    /// </summary>
    /// <param name="bikeRatingModel">The bike rating model.</param>
    /// <param name="userId">The user id.</param>
    /// <returns>Returns service result indicating the result of this operation.</returns>
    Task<ServiceResult> AddRating(AddBikeRatingModel bikeRatingModel, Guid userId);

    /// <summary>
    /// Update an existing rating details.
    /// </summary>
    /// <param name="ratingId">The rating id.</param>
    /// <param name="review">The updated review.</param>
    /// <param name="userId">The user id.</param>
    /// <returns>Returns service result indicating the result of this operation.</returns>
    Task<ServiceResult> UpdateExistingRating(Guid ratingId, string review, Guid userId);

    /// <summary>
    /// Get bike rating details.
    /// </summary>
    /// <param name="bikeId">The bike id.</param>
    /// <returns>Return bike rating details.</returns>
    Task<ServiceResult<BikeRatingDetailsDTO>> GetAverageRatingDetails(Guid bikeId);
}
