using System.ComponentModel.DataAnnotations;

namespace BookBoowingApp.Service.Models;

public class AddAgreementModel
{
    /// <summary>
    /// Gets or sets bike id.
    /// </summary>
    [Required(ErrorMessage = "BikeId cannot be empty!")]
    public Guid BikeId { get; set; }
    /// <summary>
    /// Gets or sets start date.
    /// </summary>
    [Required(ErrorMessage = "StartDate cannot be empty!")]
    public DateTime StartDate { get; set; }
    /// <summary>
    /// Gets or sets end date.
    /// </summary>
    [Required(ErrorMessage = "EndDate cannot be empty!")]
    public DateTime EndDate { get; set; }
}
