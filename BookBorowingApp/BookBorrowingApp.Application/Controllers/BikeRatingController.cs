using System.Net;
using System.Net.Mime;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Service.DTOs;
using BookBoowingApp.Service.IServices;
using BookBoowingApp.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookBorrowingApp.Application.Controllers;

/// <summary>
/// Represents bike rating controller.
/// </summary>
/// <param name="bikeRatingService">The bike rating service interface.</param>
[ApiController]
[Route("api/bike-rating")]
public class BikeRatingController(IBikeRatingService bikeRatingService) : ControllerBase
{
    /// <summary>
    /// Represents bike rating service interface.
    /// </summary>
    private readonly IBikeRatingService _bikeRatingService = bikeRatingService;

    /// <summary>
    /// Create new bike rating.
    /// </summary>
    /// <param name="bikeRatingModel">The bike rating model.</param>
    /// <returns>Returns created response.</returns>
    /// <exception cref="ApiException">The api exception.</exception>
    [HttpPost]
    [Authorize]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Created))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequest))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> AddRating([FromBody] AddBikeRatingModel bikeRatingModel)
    {
        try
        {
            // Check for model validation.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // creating new rating
            var response = await _bikeRatingService.AddRating(bikeRatingModel);

            // check for bad request response.
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(response.ValidationError);
            }

            // check for created response
            if (response.StatusCode == HttpStatusCode.Created)
            {
                return Created();
            }

            // return the response code if anything goes wrong.
            return StatusCode((int)response.StatusCode);
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

    [HttpPut]
    [Route("{id}")]
    [Authorize]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Ok))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequest))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFound))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(string))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> UpdateBikeRating(Guid id, [FromBody] string review)
    {
        try
        {
            // check for review exists or not
            if (review.IsNullOrEmpty())
            {
                return BadRequest(new ValidationError(code: "Review", description: "Review cannot be empty"));
            }

            // check the review length between 30 to 500
            if (review.Length < 30 || review.Length > 500)
            {
                return BadRequest(new ValidationError(code: "Review", description: "Review must be 30 to 500 character long."));
            }

            // update the rating
            var response = await _bikeRatingService.UpdateExistingRating(id, review);

            // check for not found response
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(response.ValidationError);
            }

            // check for forbidden response
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return Forbid(response.ValidationError.Description);
            }

            // check for ok response
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok();
            }

            // return the response code if anything goes wrong.
            return StatusCode((int)response.StatusCode);
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

    [HttpGet]
    [Route("bike-rating-details/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BikeRatingDetailsDTO))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> GetBikeRatingDetails(Guid id)
    {
        try
        {
            // Get bike rating details
            var response = await _bikeRatingService.GetAverageRatingDetails(id);

            return Ok(response.Data);
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
