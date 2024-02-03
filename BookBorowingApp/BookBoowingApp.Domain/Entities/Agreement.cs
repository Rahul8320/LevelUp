namespace BookBoowingApp.Domain.Entities;

/// <summary>
/// Represents agreement entity.
/// </summary>
public class Agreement
{
    /// <summary>
    /// Gets or sets agreement id.
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Gets or sets bike id.
    /// </summary>
    public Guid BikeId { get; set; }
    /// <summary>
    /// Get or sets user id.
    /// </summary>
    public Guid UserId { get; set; }
    /// <summary>
    /// Gets or sets bike owner id.
    /// </summary>
    public Guid BikeOwnerId { get; set; }
    /// <summary>
    /// Gets or sets start date.
    /// </summary>
    public DateTime StartDate { get; set; }
    /// <summary>
    /// Gets or sets end date.
    /// </summary>
    public DateTime EndDate { get; set; }
    /// <summary>
    /// Gets or sets duration in days.
    /// </summary>
    public int Duration { get; set; }
    /// <summary>
    /// Gets or sets total cost.
    /// </summary>
    public int TotalCost { get; set; }
    /// <summary>
    /// Gets or sets is accepted by user status.
    /// </summary>
    public bool IsAcceptedByUser { get; set; }
    /// <summary>
    /// Gets or sets created at.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Gets or sets last updated.
    /// </summary>
    public DateTime LastUpdated { get; set; }
}
