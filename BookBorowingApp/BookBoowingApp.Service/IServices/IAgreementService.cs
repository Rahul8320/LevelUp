using BookBoowingApp.Domain.Common;
using BookBoowingApp.Domain.Entities;
using BookBoowingApp.Service.Models;

namespace BookBoowingApp.Service.IServices;

/// <summary>
/// Represents agreement service interface.
/// </summary>
public interface IAgreementService
{
    /// <summary>
    /// Create new agreement.
    /// </summary>
    /// <param name="agreementModel">The agreement details.</param>
    /// <param name="userId">The user id.</param>
    /// <returns>Returns newly created agreement id.</returns>
    Task<ServiceResult<Guid>> CreateNewAgreement(AddAgreementModel agreementModel, Guid userId);

    /// <summary>
    /// Update existing agreement details.
    /// </summary>
    /// <param name="agreementModel">The updated agreement details.</param>
    /// <param name="userId">The user id.</param>
    /// <returns>Returns updated agreement details.</returns>
    Task<ServiceResult<Agreement>> UpdateExistingAgreement(AddAgreementModel agreementModel, Guid userId);

    /// <summary>
    /// Delete an agreement.
    /// </summary>
    /// <param name="agreementId">The agreement id.</param>
    /// <param name="userId">The user id.</param>
    /// <returns>Returns the service result indicating the delete operation result.</returns>
    Task<ServiceResult> DeleteAgreement(Guid agreementId, Guid userId);

    /// <summary>
    /// Get agreement details by it's id.
    /// </summary>
    /// <param name="agreementId">The agreement id.</param>
    /// <param name="userId">The user id.</param>
    /// <returns>Returns agreement details.</returns>
    Task<ServiceResult<Agreement>> GetAgreementDetails(Guid agreementId, Guid userId);

    /// <summary>
    /// Get all agreements.
    /// </summary>
    /// <returns>Returns list of all agreement.</returns>
    Task<ServiceResult<List<Agreement>>> GetAllAgreements();
}
