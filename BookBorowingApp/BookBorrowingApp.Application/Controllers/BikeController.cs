using System.Net;
using System.Net.Mime;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Domain.Entities;
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
                return StatusCode(StatusCodes.Status201Created, new
                {
                    response.Data,
                    response.StatusCode
                });
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
            // Deleting the existing bike data
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

    /// <summary>
    /// Update existing bike data.
    /// </summary>
    /// <param name="id">The bike id.</param>
    /// <param name="bikeModel">The bike model.</param>
    /// <returns>Returns action result.</returns>
    /// <exception cref="ApiException">The api exception.</exception>
    [HttpPut]
    [Route("{id}")]
    [Authorize(Roles = "Admin")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Bike))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequest))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFound))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(string))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> UpdateBike(Guid id, [FromBody] AddBikeModel bikeModel)
    {
        try
        {
            // Check for model validations.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Updating existing bike data
            var response = await _bikeService.UpdateExistingBike(id, bikeModel);

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
                return Ok(response.Data);
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
    /// Get all available bikes.
    /// </summary>
    /// <param name="maker">The bike maker</param>
    /// <param name="model">The bike model</param>
    /// <param name="price">The bike price</param>
    /// <returns>Returns action result</returns>
    /// <exception cref="ApiException">The api exception</exception>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Bike>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(string))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> GetAvailableBike([FromQuery] string? maker = null, [FromQuery] string? model = null, [FromQuery] int? price = null)
    {
        try
        {
            // search bikes
            var response = await _bikeService.GetAllAvailableBike(maker, model, price);

            // check or success response.
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }

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
    [Route("owner-bikes")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Bike>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(string))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> GetOwnerBikes()
    {
        try
        {
            // fetch owner bikes
            var response = await _bikeService.GetOwnersAllBikes();

            // check for forbidden response
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return Forbid(response.ValidationError.Description);
            }

            // check for success response
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }

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
    /// Get bike details by it's id.
    /// </summary>
    /// <param name="id">The bike id.</param>
    /// <returns>Returns action result.</returns>
    /// <exception cref="ApiException">The api exception.</exception>
    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Bike))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFound))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(string))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> GetBikeDetails(Guid id)
    {
        try
        {
            // search bikes
            var response = await _bikeService.GetBikeDetails(id);

            // check for not found response
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(response.ValidationError);
            }

            // check or success response.
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }

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
