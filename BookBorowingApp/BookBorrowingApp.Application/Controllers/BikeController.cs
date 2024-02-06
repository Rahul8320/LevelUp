using System.Net;
using System.Net.Mime;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Service.IServices;
using BookBoowingApp.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookBorrowingApp.Application.Controllers;

/// <summary>
/// Represents bike controller.
/// </summary>
/// <param name="bikeService">The bike service interface.</param>
[ApiController]
[Route("api/bikes")]
public class BikeController(IBikeService bikeService) : ControllerBase
{
    /// <summary>
    /// Represents bike service interface for maintaining bike data.
    /// </summary>
    private readonly IBikeService _bikeService = bikeService;

    /// <summary>
    /// Create new Bike
    /// </summary>
    /// <param name="bikeModel">Bike model</param>
    /// <returns>Returns action result</returns>
    /// <exception cref="ApiException">The api exception.</exception>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequest))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(string))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> AddBike([FromBody] AddBikeModel bikeModel)
    {
        try
        {
            // Check for model validation.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // creating new bike
            var response = await _bikeService.CreateNewBike(bikeModel);

            // check for forbidden response
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return Forbid(response.ValidationError.Description);
            }

            // check for created response
            if (response.StatusCode == HttpStatusCode.Created)
            {
                return Created(new Uri(null!), response.Data);
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

    /// <summary>
    /// Delete existing bike data.
    /// </summary>
    /// <param name="id">Requested bike id.</param>
    /// <returns>Return action result</returns>
    /// <exception cref="ApiException">The api exception</exception>
    [HttpDelete]
    [Route("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Ok))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFound))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> DeleteBike(Guid id)
    {
        try
        {
            // Delete the existing bike data
            var response = await _bikeService.DeleteBike(id);

            // check for forbidden response
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return Forbid(response.ValidationError.Description);
            }

            // Check for 404 response code.
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(response.ValidationError);
            }

            // Check for 200 response code.
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
}
