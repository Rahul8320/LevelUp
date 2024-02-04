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

            var durationInDays =  (int)(agreementModel.EndDate - agreementModel.StartDate).TotalDays;
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

            if(result == false)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, new Exception("Create agreement and update bike failed!"));
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

    public Task<ServiceResult> DeleteAgreement(Guid agreementId, Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult<Agreement>> GetAgreementDetails(Guid agreementId, Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult<List<Agreement>>> GetAllAgreements()
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult<Agreement>> UpdateExistingAgreement(AddAgreementModel agreementModel, Guid userId)
    {
        throw new NotImplementedException();
    }
}
