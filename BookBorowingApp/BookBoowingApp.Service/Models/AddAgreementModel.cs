namespace BookBoowingApp.Service.Models;

public class AddAgreementModel
{
    /// <summary>
    /// Gets or sets bike id.
    /// </summary>
    public Guid BikeId { get; set; }
    /// <summary>
    /// Gets or sets start date.
    /// </summary>
    public DateTime StartDate { get; set; }
    /// <summary>
    /// Gets or sets end date.
    /// </summary>
    public DateTime EndDate { get; set; }
}
