using System.Net;
using BookBoowingApp.Domain.Common;
using BookBoowingApp.Domain.Entities;
using BookBoowingApp.Infrastructure.IRepositories;
using BookBoowingApp.Service.IServices;
using BookBoowingApp.Service.Models;

namespace BookBoowingApp.Service.ServiceImplementations;

/// <summary>
/// Represents the agreement service implementation.
/// </summary>
public class AgreementService(IUnitOfWork unitOfWork) : IAgreementService
{
    /// <summary>
    /// Represents the interface of unit of work.
    /// </summary>
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    /// <summary>
    /// Create new agreement.
    /// </summary>
    /// <param name="agreementModel">New agreement details.</param>
    /// <param name="userId">The user id</param>
    /// <returns>Returns newly created agreement id.</returns>
    /// <exception cref="ApiException">The Api Exception.</exception>
    public async Task<ServiceResult<Guid>> CreateNewAgreement(AddAgreementModel agreementModel, Guid userId)
    {
        try
        {
            // Fetch bike details.
            var bike = await _unitOfWork.BikeRepository.Get(agreementModel.BikeId);

            // Check bike exists or not.
            if (bike == null)
            {
                return new ServiceResult<Guid>(
                    HttpStatusCode.NotFound,
                    new ValidationError(
                        code: "BikeNotFound",
                        description: $"Bike with id: {agreementModel.BikeId} not found."
                    )
                );
            }

            // Check bike is available for rent or not.
            if (bike.IsAvailableForRent == false)
            {
                return new ServiceResult<Guid>(
                    HttpStatusCode.BadRequest,
                    new ValidationError(
                        code: "NotAvailableForRent",
                        description: $"Bike with id: {agreementModel.BikeId} is not available for rent."
                    )
                );
            }

            var durationInDays = (int)(agreementModel.EndDate - agreementModel.StartDate).TotalDays;
            // Create new agreement.
            var agreement = new Agreement()
            {
                Id = Guid.NewGuid(),
                BikeId = bike.Id,
                BikeOwnerId = bike.Owner,
                UserId = userId,
                StartDate = agreementModel.StartDate,
                EndDate = agreementModel.EndDate,
                Duration = durationInDays,
                IsAcceptedByUser = false,
                TotalCost = durationInDays * bike.RentalPricePerDay,
                CreatedAt = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow,
            };

            // Update bike availability status
            bike.IsAvailableForRent = false;
            bike.CurrentBikeStatus = BikeStatus.Rented;
            bike.LastUpdated = DateTime.UtcNow;

            _unitOfWork.BikeRepository.Update(bike);
            _unitOfWork.AgreementRepository.Add(agreement);
            var result = await _unitOfWork.Complete();

            if (result == false)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, new Exception("Create agreement failed!"));
            }

            return new ServiceResult<Guid>(HttpStatusCode.Created, agreement.Id);
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
    /// Delete an existing agreement.
    /// </summary>
    /// <param name="agreementId">The agreement id.</param>
    /// <param name="userId">The user id.</param>
    /// <returns>Returns service result indication the result of delete operation.</returns>
    /// <exception cref="ApiException">The api exception.</exception>
    public async Task<ServiceResult> DeleteAgreement(Guid agreementId, Guid userId)
    {
        try
        {
            // Fetch agreement details
            var agreement = await _unitOfWork.AgreementRepository.Get(agreementId);

            // Check for agreement exists or not
            if (agreement == null)
            {
                return new ServiceResult(HttpStatusCode.NotFound, $"Agreement with id: {agreementId} not found.");
            }

            // Check for user accepted & user request for delete agreement
            if (agreement.IsAcceptedByUser == true && agreement.UserId == userId)
            {
                return new ServiceResult(HttpStatusCode.Forbidden, "You can't deleted this agreement as it's already accepted by you.");
            }

            // Check for user not accepted and user request for delete agreement or bike owner request for delete.
            if ((agreement.IsAcceptedByUser == false && agreement.UserId == userId) || (agreement.BikeOwnerId == userId))
            {
                var bike = await _unitOfWork.BikeRepository.Get(agreement.BikeId);

                // Check for bike existing or not.
                if (bike != null)
                {
                    bike.IsAvailableForRent = true;
                    bike.CurrentBikeStatus = BikeStatus.AvailableForRent;
                    bike.LastUpdated = DateTime.UtcNow;
                    _unitOfWork.BikeRepository.Update(bike);
                }

                // Update the database.
                _unitOfWork.AgreementRepository.Delete(agreement);
                var result = await _unitOfWork.Complete();

                // check for success result
                if (result == false)
                {
                    throw new ApiException(HttpStatusCode.InternalServerError, new Exception("Agreement deleted failed!"));
                }

                return new ServiceResult(HttpStatusCode.OK);
            }

            return new ServiceResult(HttpStatusCode.Forbidden, "You don't have permission for this operation.");
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

    public async Task<ServiceResult<Agreement>> UpdateExistingAgreement(Guid agreementId, AddAgreementModel agreementModel, Guid userId)
    {
        try
        {
            // Fetch agreement data
            var agreement = await _unitOfWork.AgreementRepository.Get(agreementId);

            // Check for agreement is exists or not
            if (agreement == null)
            {
                return new ServiceResult<Agreement>(
                    HttpStatusCode.NotFound,
                    new ValidationError(code: "NotFoundAgreement", description: $"Agreement with id: {agreementId} is not found!"));
            }

            // Check for valid bike id
            if (agreement.BikeId != agreementModel.BikeId)
            {
                return new ServiceResult<Agreement>(
                    HttpStatusCode.BadRequest,
                    new ValidationError(code: "InvalidBikeId", description: "Invalid Bike Id!"));
            }

            // Check for user accepted & user request for delete agreement
            if (agreement.IsAcceptedByUser == true && agreement.UserId == userId)
            {
                return new ServiceResult<Agreement>(
                    HttpStatusCode.Forbidden,
                    new ValidationError(
                        code: "PermissionDenied",
                        description: "You can't deleted this agreement as it's already accepted by you."
                    )
                );
            }

            // Check for user not accepted and user request for delete agreement or bike owner request for delete.
            if ((agreement.IsAcceptedByUser == false && agreement.UserId == userId) || (agreement.BikeOwnerId == userId))
            {
                // Fetch bike data.
                var bikeDetails = await _unitOfWork.BikeRepository.Get(agreementModel.BikeId);

                // Check for bike is exists or not
                if (bikeDetails == null)
                {
                    return new ServiceResult<Agreement>(
                        HttpStatusCode.NotFound,
                        new ValidationError(
                            code: "NotFoundBike",
                            description: $"Bike with id: {agreementModel.BikeId} is not found!"
                        )
                    );
                }

                // update the agreement
                var durationInDays = (int)(agreementModel.EndDate - agreementModel.StartDate).TotalDays;
                agreement.StartDate = agreementModel.StartDate;
                agreement.EndDate = agreementModel.EndDate;
                agreement.TotalCost = durationInDays * bikeDetails.RentalPricePerDay;
                agreement.LastUpdated = DateTime.UtcNow;

                // Update agreement details.
                _unitOfWork.AgreementRepository.Update(agreement);
                var result = await _unitOfWork.Complete();

                if (result == false)
                {
                    throw new ApiException(HttpStatusCode.InternalServerError, new Exception("Agreement update failed!"));
                }

                return new ServiceResult<Agreement>(HttpStatusCode.OK, agreement);
            }

            return new ServiceResult<Agreement>(
                HttpStatusCode.Forbidden,
                new ValidationError(
                        code: "PermissionDenied",
                        description: "You don't have permission for this operation."
                    )
                );
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

    public Task<ServiceResult<Agreement>> GetAgreementDetails(Guid agreementId, Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult<List<Agreement>>> GetAllAgreements()
    {
        throw new NotImplementedException();
    }

}
